using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.ProductDtos;

namespace MartX.BL.Validators.ProductValidator;

public class ProductGetDtoValidator : AbstractValidator<ProductGetDto>
{
    public ProductGetDtoValidator()
    {
        RuleFor(x => x.Title)
               .NotEmpty().NotNull().WithMessage("Title is required")
               .Length(8, 25).WithMessage("min 8, max 25");

        RuleFor(x => x.Description)
        .NotEmpty().NotNull().WithMessage("Description is required")
        .Length(8, 25).WithMessage("min 8, max 25");

        RuleFor(x => x.SalePrice)
        .NotEmpty().NotNull().WithMessage("SalePrice is required");

        RuleFor(x => x.SalePercent)
        .NotEmpty().NotNull().WithMessage("SalePercent is required");

        RuleFor(x => x.Size)
        .NotEmpty().NotNull().WithMessage("Size is required");

        RuleFor(x => x.StockQuantity)
        .NotEmpty().NotNull().WithMessage("StockQuantity is required");
    }
}
