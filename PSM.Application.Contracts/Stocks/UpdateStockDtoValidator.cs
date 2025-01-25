using FluentValidation;
using PSM.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Application.Contracts.Stocks
{
    public class UpdateStockDtoValidator : AbstractValidator<RemoveStockDto>
    {
        public UpdateStockDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            RuleFor(x => x.ActionTypes)
                    .Must(actionType => Enum.IsDefined(typeof(StockActionTypes), actionType) && actionType != StockActionTypes.Add)
                    .WithMessage("Action type must be a valid value and cannot be 'Add' (1).");

        }
    }
}
