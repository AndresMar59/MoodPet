using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Tar;


namespace MoodPet.Domain.Entities
{
    public class Raza : GeneralEntity
    {

        public string Nombre { get; set; }

        public int EspecieId { get; set; }

        public Especie Especie { get; set; }



    }
}
