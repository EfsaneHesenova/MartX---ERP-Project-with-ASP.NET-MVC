using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.DocumentImageUrlDtos;

namespace MartX.BL.Validators.DocumentImageUrlValidator;

public class DocumentImageUrlPostDtoValidator : AbstractValidator<DocumentImageUrlPostDto>
{
    public DocumentImageUrlPostDtoValidator()
    {
        RuleFor(x => x.Title)
                .NotEmpty().NotNull().WithMessage("Title is required")
                .Length(8, 25).WithMessage("min 8, max 25");
    }
}
