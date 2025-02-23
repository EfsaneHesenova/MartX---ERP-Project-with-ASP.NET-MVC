using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.BrandDtos;

namespace MartX.BL.Validators.BrandValidator;

public class BrandPostDtoValidator : AbstractValidator<BrandPostDto>
{
    public BrandPostDtoValidator()
    {
        RuleFor(x => x.Title)
                .NotEmpty().NotNull().WithMessage("Title is required")
                .Length(3, 30).WithMessage("min 3, max 30");

        RuleFor(x => x.Description)
        .NotEmpty().NotNull().WithMessage("Description is required")
        .Length(8, 100).WithMessage("min 8, max 100");
    }
}
