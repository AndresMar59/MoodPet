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
            return new AppIdentityUser
            {
                //sin id, ya que se genera automáticamente al crear el usuario en la base de datos
                Firstname = usuario.FirstName,
                Lastname = usuario.LastName,
                UserName = usuario.Email,
                Email = usuario.Email,
                PhoneNumber = usuario.Tel
            };
        }

        public static Usuario ToDomainUser(this AppIdentityUser identityUser)
        {
            if (identityUser == null)
                return null;
            return new Usuario
            {
                Id = Guid.Parse(identityUser.Id),
                Email = identityUser.Email,
                Tel = identityUser.PhoneNumber,
                FirstName = identityUser.Firstname,
                LastName = identityUser.Lastname
            };
        }
    }
}
