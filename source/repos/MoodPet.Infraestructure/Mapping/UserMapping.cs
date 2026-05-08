using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MoodPet.Infraestructure.Identity;
using MoodPet.Domain.Entities;

namespace MoodPet.Infraestructure.Mapping
{
    public static class UserMapping
    {

        public static AppIdentityUser ToIdentityUser(this Usuario usuario)
        {
            if (usuario == null) return null;

            return new AppIdentityUser
            {

                UserName = usuario.name,
                Email = usuario.email,
                PhoneNumber = usuario.tel
            };
        }

        public static Usuario ToDomainUser(this AppIdentityUser identityUser)
        {
            if (identityUser == null) return null;

            return new Usuario
            {
                id = Guid.TryParse(identityUser.Id, out var guidId) ? guidId : Guid.Empty,
                email = identityUser.Email,
                tel = identityUser.PhoneNumber,
                name = identityUser.UserName ?? "Sin nombre",
                lastname = ""
            };
        }
    }
}
