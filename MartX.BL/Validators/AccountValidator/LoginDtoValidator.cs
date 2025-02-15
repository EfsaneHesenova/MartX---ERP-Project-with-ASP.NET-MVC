using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.AccountDtos;

namespace MartX.BL.Validators.AccountValidator;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(p => p.UsernameOrEmail)
            .NotEmpty().NotNull().WithMessage("UserName or Email is required");

        RuleFor(p => p.Password)
            .NotEmpty().NotNull().WithMessage("Password is required");
    }
}
