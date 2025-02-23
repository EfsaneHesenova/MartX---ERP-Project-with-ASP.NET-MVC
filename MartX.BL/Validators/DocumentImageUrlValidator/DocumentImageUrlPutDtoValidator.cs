using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MartX.BL.DTOs.DocumentImageUrlDtos;

namespace MartX.BL.Validators.DocumentImageUrlValidator;

public class DocumentImageUrlPutDtoValidator : AbstractValidator<DocumentImageUrlPutDto>
{
    public DocumentImageUrlPutDtoValidator()
    {
        RuleFor(x => x.Title)
                .NotEmpty().NotNull().WithMessage("Title is required")
                .Length(3, 30).WithMessage("min 3, max 30");
    }
}
