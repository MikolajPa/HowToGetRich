using EnovationApp.DataAccess.Models;
using EnovationApp.Domain.Enums;
using FluentValidation;

namespace EnovationAssignment.Validators
{
    public class AnimalValidator : AbstractValidator<AnimalDto>
    {
        public AnimalValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(1, 100).WithMessage("Who would remember that name?");
            RuleFor(x => x.Age).NotNull().GreaterThan(0).WithMessage("How is it possible?");
            RuleFor(x => x.Legs).NotNull().InclusiveBetween(0,16).WithMessage("What kind of monster is this?");
            RuleFor(x => x.Type).NotNull().Must(BeValidAnimalType).WithMessage("It has to be some kind of Animal does it?");
            
        }


        private bool BeValidAnimalType(AnimalEnum AnimalType)
        {
            return AnimalType != AnimalEnum.None;

        }



    }
}
