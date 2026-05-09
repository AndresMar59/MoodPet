using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using MoodPet.Domain.Interfaces;
using MoodPet.Infraestructure.Identity;
using MoodPet.Infraestructure.Percistencia;
using MoodPet.Infraestructure.Percistencia.Repositorios;
using MoodPet.Infraestructure.Security;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Db Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnection")));

builder.Services.AddIdentity<AppIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuración Json, enum y evitar ciclos de referencia
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    //Pasar el enum a string
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


//Autenticación JWT
builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // El challenge se encarga de validar el token y si no es valido, devuelve un error 401 Unauthorized. Al establecerlo como JwtBearerDefaults.AuthenticationScheme, le estamos diciendo a la aplicación que utilice el esquema de autenticación JWT Bearer para manejar los desafíos de autenticación.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),

        };

    });


builder.Services.AddAuthorization();

//Swagger
builder.Services.AddEndpointsApiExplorer(); // Esto es vital
builder.Services.AddSwaggerGen(c =>

{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sumativa Libro",
        Version = "v1"

    });

    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
    {
        Description = "Jwt Authorization header using the Bearer Schema. Example: ",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    });


    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []

    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Creando data por defecto
using (var scope = app.Services.CreateScope())
{
    var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
    // Crear roles por defecto
    var defaultRoles = new[] { "Admin", "User" };

    foreach (var role in defaultRoles)
    {
        if (!await roleRepository.RoleExistsAsync(role))
        {
            await roleRepository.CreateRoleAsync(role);
        }
    }

    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

    if (!await userRepository.UserExists("admin@admin.com"))
    {
        var result = userRepository.CreateUser(
            new MoodPet.Domain.Entities.Usuario
            {
                email = "admin@admin.com",
                password = "Admin123!",
                name = "Admin",
                lastname = "Admin",
            }).Result;

        var resultUsertoRole = userRepository.AddToRoleAsync(result, "Admin").Result;
    }
}

app.Run();
