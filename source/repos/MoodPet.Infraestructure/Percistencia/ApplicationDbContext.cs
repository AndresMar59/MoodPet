using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moodpet.Domain.Entities;
using MoodPet.Domain.Entities;
using MoodPet.Infraestructure.Identity;

namespace MoodPet.Infraestructure.Percistencia
{
    public class ApplicationDbContext : IdentityDbContext<AppIdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

       public DbSet<EventoCalendario> EventosCalendario { get; set; }

       public DbSet<Mascota> Mascotas { get; set; } 

       public DbSet<TipoEvento> TiposEvento { get; set; }

       public DbSet<TareaDiaria> TareasDiarias { get; set; }

        public DbSet<Recomendacion> Recomendaciones { get; set; }   

        public DbSet<Raza> Razas { get; set; }

        public DbSet<HistorialTarea> HistorialTarea { get; set; }

        public DbSet<Especie> Especies { get; set; }
        public DbSet<HistorialRecomendaciones> HistorialRecomendaciones { get; set; }


        // 2. Configuramos las relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mascota 1 - N Evento_Calendario
            // TipoEvento 1 - N Evento_Calendario
            // User 1 - N Evento_Calendario
            modelBuilder.Entity<EventoCalendario>(entity =>
            {
                entity.HasKey(e => e.Id); // Llave primaria

                entity.HasOne(e => e.Mascota)             // Un evento tiene una mascota
                    .WithMany(e => e.Eventos)             // Una mascota puede tener muchos eventos (puedes poner .WithMany(m => m.Eventos) si agregas la colección en Mascota)
                    .HasForeignKey(e => e.MascotaId)      // Clave foránea en Eventocalendario
                    .OnDelete(DeleteBehavior.Cascade); // Si se borra la mascota, se borran sus eventos

                entity.HasOne(e => e.TipoEvento)
                    .WithMany()                           
                    .HasForeignKey(e => e.TipoEventoId);
                    
                entity.HasOne<AppIdentityUser>() // Relación con AppIdentityUser
                    .WithMany(e => e.Eventos) // Un usuario puede tener muchos eventos
                    .HasForeignKey(e => e.UserId) // Clave foránea en Eventocalendario
                    .OnDelete(DeleteBehavior.NoAction); // Si se borra el usuario, se borran sus eventos
            });

            // especie 1 - N raza
            modelBuilder.Entity<Raza>(entity =>
            {
                entity.HasKey(e => e.Id); 

                entity.HasOne(e => e.Especie)             
                    .WithMany(g => g.Razas)                           
                    .HasForeignKey(e => e.EspecieId)      
                    .OnDelete(DeleteBehavior.Cascade);    
            });

            // tarea diaria 1 - N historial tarea
            modelBuilder.Entity<HistorialTarea>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.TareaDiaria)
                    .WithMany()
                    .HasForeignKey(e => e.TareaId);
            });

            // user 1 - N mascota
            // raza 1 - N mascota
            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne<AppIdentityUser>()
                    .WithMany(e => e.Mascotas)
                    .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Raza)
                    .WithMany()
                    .HasForeignKey(e => e.RazaId);
            });

            // mascota 1 - N recomendacion
            modelBuilder.Entity<Recomendacion>()
                .HasOne(p => p.Raza)
                .WithMany()
                .HasForeignKey(p => p.RazaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recomendacion>()
                .HasOne(p => p.Especie)
                .WithMany()
                .HasForeignKey(p => p.EspecieId)
                .OnDelete(DeleteBehavior.Restrict);

            // mascota 1 - N tarea diaria
            modelBuilder.Entity<TareaDiaria>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Mascota)
                    .WithMany(e => e.Tareas)
                    .HasForeignKey(e => e.MascotaId);
            });


            modelBuilder.Entity<HistorialRecomendaciones>()
                .HasOne(r => r.Mascota)
                .WithMany()
                .HasForeignKey(r => r.MascotaId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }


    }

