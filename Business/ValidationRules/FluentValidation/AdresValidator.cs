using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AdresValidator : AbstractValidator<Adres>
    {
        public AdresValidator()
        {
            RuleFor(p => p.MusteriId).NotEmpty();
            RuleFor(p => p.Mahalle).NotEmpty();
            RuleFor(p => p.Cadde).NotEmpty();
            RuleFor(p => p.Sokak).NotEmpty();

            RuleFor(p => p.BinaNo).NotEmpty();
            RuleFor(p => p.BinaNo).Length(1, 10);

            RuleFor(p => p.Kat).NotEmpty();
            RuleFor(p => p.Kat).Length(1,2);

            RuleFor(p => p.İlce).NotEmpty();
            RuleFor(p => p.İl).NotEmpty();
            RuleFor(p => p.PostaKodu).NotEmpty();
        }
    }
}
