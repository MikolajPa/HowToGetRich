using EnovationApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public int legs { get; set; }
        public AnimalEnum Type { get; set; }
        public bool IsDeleted { get; set; }
    }
}
