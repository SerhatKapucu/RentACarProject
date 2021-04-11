using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
   public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {

            RuleFor(c => c.CarName).NotEmpty().WithMessage("Car Name boş geçilemez.");
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage("DailyPrice 0'dan büyük olmalı");
        }
    }
}
