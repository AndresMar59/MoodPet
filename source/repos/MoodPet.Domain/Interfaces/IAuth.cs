using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;

namespace MoodPet.Domain.Interfaces
{
    public interface IAuth
    {
        Task<Usuario> RegisterUser(Usuario usuario);
        Task<string> Login(string email, string password);
    }
}
