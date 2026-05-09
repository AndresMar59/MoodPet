using Microsoft.AspNetCore.Identity;
using MoodPet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace MoodPet.Infraestructure.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
        public ICollection<TareaDiaria> TareaDiarias { get; set; } = new List<TareaDiaria>();
        public ICollection<EventoCalendario> Eventos { get; set; } = new List<EventoCalendario>();
    }
}
