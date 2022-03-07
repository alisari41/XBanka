using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TicariFaliyetValidator : AbstractValidator<TicariFaaliyetler>
    {
        public TicariFaliyetValidator()
        {
            RuleFor(p => p.GercekMusteriId).NotEmpty();
            RuleFor(p => p.BaslangicTarihi).NotEmpty();
        }
    }
}
