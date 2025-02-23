using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.EmployeeDtos;

namespace MartX.BL.Validators.EmployeeValidator;

public class EmployeePutDtoValidator : AbstractValidator<EmployeePutDto>
{
    public EmployeePutDtoValidator()
    {
        RuleFor(p => p.Image)
            .Cascade(CascadeMode.Stop)
            .Must(p => p.Length < 5 * 1024 * 1024).WithMessage("Image must be 5mb")
            .NotEmpty().NotNull().WithMessage("Image is required");

        RuleFor(x => x.Address)
                .NotEmpty().NotNull().WithMessage("Address is required")
                .Length(3, 50).WithMessage("Address min character is 3 and max character is 50 ");

        RuleFor(x => x.Email)
             .NotEmpty().NotNull().WithMessage("Email is required")
             .EmailAddress().WithMessage("incorrect Email adress");


        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number cannot be empty.")
            .Must(PhoneNumberRegex)
            .WithMessage("Please enter a valid Azerbaijani phone number.");

        RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("LastName is required")
                .Length(3, 30).WithMessage("LastName min character is 3 and max character is 30 ");

        RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("FirstName is required")
                .Length(3, 30).WithMessage("FirstName min character is 3 and max character is 30 ");

        RuleFor(x => x.Age)
                .NotEmpty().NotNull().WithMessage("StockQuantity is required");
    }

    public static bool PhoneNumberRegex(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        string pattern = @"^(?:\+994|0)(?:(?:10|12|18|21|22|24|25|26|29)|(?:50|51|55|70|77))\d{7}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
}


