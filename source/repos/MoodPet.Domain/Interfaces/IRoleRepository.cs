using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPet.Domain.Interfaces
{
    public interface IRoleRepository
    {

        Task<bool> RoleExistsAsync(string roleName);

        Task<bool> CreateRoleAsync(string roleName);
    }
}
