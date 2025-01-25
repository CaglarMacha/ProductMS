using FluentValidation;
using PSM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application.Contracts.Products
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            When(a => !string.IsNullOrWhiteSpace(a.Title), () =>
            {
                RuleFor(x => x.Title)
                    .MaximumLength(ProductConsts.MaxTitleLength)
                    .WithMessage("Title cannot exceed 200 characters.")
                    .WithMessage("Required Field");
            });
        }
     
    }
}
