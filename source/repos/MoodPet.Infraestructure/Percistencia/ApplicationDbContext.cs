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

       public DbSet<Eventocalendario> EventosCalendario { get; set; }

       public DbSet<Mascota> Mascotas { get; set; } 

       public DbSet<TipoEvento> Evento { get; set; }

       public DbSet<TareaDiaria> TareasDiarias { get; set; }

        public DbSet<Recomendacion> Recomendaciones { get; set; }   

        public DbSet<Raza> Raza { get; set; }

        public DbSet<HistorialTarea> HistorialTarea { get; set; }

        public DbSet<Especie> Especies { get; set; }


        // 2. Configuramos las relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación Uno a Muchos (Una Mascota tiene muchos Eventos)
            modelBuilder.Entity<Eventocalendario>(entity =>
            {
                entity.HasKey(e => e.Id); // Llave primaria

                entity.HasOne(e => e.mascota)             // Un evento tiene una mascota
                    .WithMany()                           // Una mascota puede tener muchos eventos (puedes poner .WithMany(m => m.Eventos) si agregas la colección en Mascota)
                    .HasForeignKey(e => e.MascotaId)      // Clave foránea en Eventocalendario
                    .OnDelete(DeleteBehavior.Cascade); // Si se borra la mascota, se borran sus eventos

                entity.HasOne(e => e.TipoEvento).WithMany()                           
                    .HasForeignKey(e => e.TipoEventoId);
            });



            modelBuilder.Entity<Raza>(entity =>
            {
                entity.HasKey(e => e.Id); 

                entity.HasOne(e => e.Especie)             
                    .WithMany(g => g.Razas)                           
                    .HasForeignKey(e => e.EspecieId)      
                    .OnDelete(DeleteBehavior.Cascade);    
            });

            modelBuilder.Entity<HistorialTarea>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.TareaDiaria)
                    .WithMany()
                    .HasForeignKey(e => e.TareaId);
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioId).OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Raza)
                    .WithMany()
                    .HasForeignKey(e => e.RazaId);
            });


            modelBuilder.Entity<Recomendacion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Mascota)
                    .WithMany()
                    .HasForeignKey(e => e.MascotaId);
            });

            modelBuilder.Entity<TareaDiaria>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Mascota)
                    .WithMany()
                    .HasForeignKey(e => e.MascotaId);
            });


        }

    }


    }

