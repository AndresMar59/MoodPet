using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;

namespace MoodPet.Domain.Interfaces
{
    public interface IJwtService
    {
 
            Task<string> GenerateJwtToken(Usuario usuario, IList<String> roles);

            Task<string> generateJwtToken(Usuario user, List<string> list);

        
    }
}
