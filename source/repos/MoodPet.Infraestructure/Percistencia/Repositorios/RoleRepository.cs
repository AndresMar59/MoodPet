using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MoodPet.Domain.Interfaces;

namespace MoodPet.Infraestructure.Percistencia.Repositorios
{
    public class RoleRepository : IRoleRepository
    {
        public readonly RoleManager<IdentityRole> _manager;
        public RoleRepository(RoleManager<IdentityRole> IdentityRole)
        {
            _manager = IdentityRole;

        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (!await RoleExistsAsync(roleName))
            {

                var result = await _manager.CreateAsync(new IdentityRole(roleName));


                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _manager.RoleExistsAsync(roleName);
        }
    }
}
