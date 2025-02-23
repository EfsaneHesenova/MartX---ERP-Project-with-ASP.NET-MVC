using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.SupplierDtos;

namespace MartX.BL.Validators.SupplierValidator;

public class SupplierGetDtoValidator : AbstractValidator<SupplierGetDto>
{
    public SupplierGetDtoValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .Length(3, 30).WithMessage("Name min character is 3 and max character is 30 ");

        RuleFor(x => x.Address)
                .NotEmpty().NotNull().WithMessage("Address is required")
                .Length(3, 50).WithMessage("Address min character is 3 and max character is 50 ");

        RuleFor(x => x.Email)
             .NotEmpty().NotNull().WithMessage("Email is required")
             .EmailAddress().WithMessage("incorrect Email adress");

        RuleFor(x => x.ContactPerson)
               .NotEmpty().NotNull().WithMessage("ContactPerson is required")
               .Length(3, 30).WithMessage("ContactPerson min character is 3 and max character is 30 ");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number cannot be empty.")
            .Must(PhoneNumberRegex)
            .WithMessage("Please enter a valid Azerbaijani phone number.");

    }

    public static bool PhoneNumberRegex(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        string pattern = @"^(?:\+994|0)(?:(?:10|12|18|21|22|24|25|26|29)|(?:50|51|55|70|77))\d{7}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
}
