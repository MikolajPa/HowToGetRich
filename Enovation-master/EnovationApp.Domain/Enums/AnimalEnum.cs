using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.Domain.Enums
{
    public enum AnimalEnum
    {
        [Display(Name = "What?")]
        None,
        [Display(Name = "Hau")]
        Dog,
        [Display(Name = "Meow")]
        Cat,
        [Display(Name = "Muu")]
        Cow

    }
}
