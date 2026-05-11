using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces;
using MoodPet.Infraestructure.Percistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

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

        public async Task<string> GetUserRole(string email)
        {
            var roles = await _user.GetUserRoles(email);
            if (roles.Contains("Admin"))
            {
                return "Admin";
            }

            return "User";


            

        }


        public async Task<string> Login(string email, string password)
        {
            var user = await _user.GetUserByEmail(email);

            if (user == null)
            {
                return "Credenciales invalidas";

            }

            var credencialesValidas = await _user.CheckPasswordAsync(user.Id.ToString(), password);

            if (!credencialesValidas)
            {
                return "Credenciales invalidas";
            }
            var roles = await _user.GetUserRoles(email);

            return await _jwt.GenerateJwtToken(user, roles);

        }

        public async Task<bool> RegisterUser(Usuario usuario)
        {

            var result = await _user.UserExists(usuario.Email);
            if (result == false)
            {
                var create = await _user.CreateUser(usuario);
                if (create == null) return false;
                return true;
            }



            return false;
        }
    }
}
