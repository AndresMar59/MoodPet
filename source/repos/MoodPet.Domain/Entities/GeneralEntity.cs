using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPet.Domain.Entities
{
    public class GeneralEntity

    {
        public int Id { get; set; }

        public DateTime CreaAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

    }
}
