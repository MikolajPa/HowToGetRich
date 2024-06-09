using EnovationApp.DataAccess.Models;
using EnovationApp.Domain.Enums;
using EnovationAssignment.Models;
using FluentValidation;

namespace EnovationAssignment.Validators
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleFor(x => x.WalletId).NotNull().Length(27, 40).WithMessage("Missing walletId");
            RuleFor(x => x.Name).NotNull().Length(1,100).WithMessage("Name missing");
            RuleFor(x => x.Email).NotNull().Length(1, 100).WithMessage("Mail missing");
            RuleFor(x => x.AccountId).NotNull().Length(1, 100).WithMessage("AccountId missing");
            RuleFor(x => x.Password).NotNull().Length(1, 100).WithMessage("Password missing");

        }
    }
}
