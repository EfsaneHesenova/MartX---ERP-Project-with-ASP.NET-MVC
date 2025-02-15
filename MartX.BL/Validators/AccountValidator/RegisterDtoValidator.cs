using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.AccountDtos;

namespace MartX.BL.Validators.AccountValidator;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("incorrect Email adress");

        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password is required")
            .Length(8, 12).WithMessage("min 8, max 12");

        RuleFor(x => x.ConfirmPassword)
           .NotEmpty().NotNull().WithMessage("Password is required")
           .Equal(x => x.Password).WithMessage("incorrect password");

        RuleFor(x => x.UserName)
            .NotEmpty().NotNull().WithMessage("UserName is required")
            .Length(6, 12).WithMessage("min 8, mzax 12");
    }
}
