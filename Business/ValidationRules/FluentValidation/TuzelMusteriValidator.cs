using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TuzelMusteriValidator : AbstractValidator<TuzelMusteriler>
    {
        public TuzelMusteriValidator()
        {
            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.Unvan).NotEmpty();

            RuleFor(p => p.VergiNo).NotEmpty();
            RuleFor(p => p.VergiNo).Length(2, 10);
        }
    }
}
