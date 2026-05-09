using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MoodPet.Domain.Interfaces;
using MoodPet.Infraestructure.Identity;
using MoodPet.Domain.Entities;
using MoodPet.Infraestructure.Mapping;


namespace MoodPet.Infraestructure.Percistencia.Repositorios
{
    public class UserRepository : IUserRepository
    {

        public readonly UserManager<AppIdentityUser> _IdentityUser;
        public UserRepository(UserManager<AppIdentityUser> IdentityUser)
        {

            _IdentityUser = IdentityUser;

        }

        public async Task<Usuario> AddToRoleAsync(Usuario usuario, string rolename)
        {
            var userDb = await _IdentityUser.FindByEmailAsync(usuario.Email);
            var result = await _IdentityUser.AddToRoleAsync(userDb, rolename);

            return usuario;
        }

        public async Task<bool> CheckPasswordAsync(string userid, string password)
        {
            var user = await _IdentityUser.FindByIdAsync(userid.ToString());

            if (user == null)
                return false;

            return await _IdentityUser.CheckPasswordAsync(user, password); ;
        }

        public async Task<Usuario> CreateUser(Usuario usuario)
        {
            var identityUser = usuario.ToIdentityUser();

            var result = await _IdentityUser.CreateAsync(identityUser, usuario.Password);

            if (result.Succeeded)
            {

                if (Guid.TryParse(identityUser.Id, out Guid generatedId))
                {
                    usuario.Id = generatedId;
                }

                return usuario;
            }

            return null;
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            var user = await _IdentityUser.FindByEmailAsync(email);

            return user?.ToDomainUser();
        }

        public async Task<List<string>> GetUserRoles(string email)
        {
            var user = await _IdentityUser.FindByEmailAsync(email);
            var roles = await _IdentityUser.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> UserExists(string email)
        {
            return await _IdentityUser.FindByEmailAsync(email) != null;
        }

        public async Task<AppIdentityUser?> Userr(string id)
        {
            return await _IdentityUser.FindByIdAsync(id);
        }


    }
}
