using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces;

namespace MoodPet.Application.Service
{
    public class Auth : IAuth
    {
        public readonly IJwtService _jwt;
        public readonly IUserRepository _user;

        public Auth(IJwtService jwt, IUserRepository user)
        {

            _jwt = jwt;
            _user = user;

        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _user.GetUserByEmail(email);

            if (user == null)
            {
                return "Credenciales invalidas, no existe usuario)";

            }

            var credencialesValidas = await _user.CheckPasswordAsync(user.id.ToString(), password);

            if (!credencialesValidas)
            {
                return "Credenciales invalidas";
            }
            var roles = await _user.GetUserRoles(email);

            return await _jwt.GenerateJwtToken(user, roles);

        }


        public async Task<string> RegisterUser(Usuario usuario)
        {
            var user = await _user.CreateUser(usuario);
            if (user == null) { return "Usuario no se pudo crear"; }
            else
            {
                var member = "member";
                await _user.AddToRoleAsync(usuario, member);
                return "Usuario creado satisfactoriamente";
            }
        }

    }
}
