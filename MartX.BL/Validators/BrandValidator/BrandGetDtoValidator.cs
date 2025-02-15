using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.BrandDtos;

namespace MartX.BL.Validators.BrandValidator;

public class BrandGetDtoValidator : AbstractValidator<BrandGetDto>
{
    public BrandGetDtoValidator()
    {
        RuleFor(x => x.Title)
                .NotEmpty().NotNull().WithMessage("Title is required")
                .Length(8, 25).WithMessage("min 8, max 25");

        RuleFor(x => x.Description)
        .NotEmpty().NotNull().WithMessage("Description is required")
        .Length(8, 25).WithMessage("min 8, max 25");
    }
}
