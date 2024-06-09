using EnovationApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace EnovationApp.DataAccess.Models
{
    public class AnimalDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Legs { get; set; }
        public AnimalEnum Type { get; set; }
    }
}
