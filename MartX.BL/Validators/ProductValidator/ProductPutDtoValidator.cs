using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.ProductDtos;

namespace MartX.BL.Validators.ProductValidator;

public class ProductPutDtoValidator : AbstractValidator<ProductPutDto>
{
    public ProductPutDtoValidator()
    {
        RuleFor(x => x.Title)
              .NotEmpty().NotNull().WithMessage("Title is required")
              .Length(3, 30).WithMessage("min 3, max 30");

        RuleFor(x => x.Description)
        .NotEmpty().NotNull().WithMessage("Description is required")
        .Length(8, 100).WithMessage("min 8, max 100");

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
