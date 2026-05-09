using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;

namespace MoodPet.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<Usuario> GetUserByEmail(String email);

        Task<Usuario> CreateUser(Usuario Usario);

        Task<bool> CheckPasswordAsync(string userid, string password);

        Task<bool> UserExists(string email);

        Task<Usuario> AddToRoleAsync(Usuario usuario, string rolename);

        Task<List<string>> GetUserRoles(string email);

    }
}
