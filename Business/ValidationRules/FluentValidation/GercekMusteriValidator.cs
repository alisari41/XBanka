using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class GercekMusteriValidator : AbstractValidator<GercekMusteriler>
    {
        public GercekMusteriValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Ad).NotEmpty();
            RuleFor(p => p.Soyad).NotEmpty();
            
            RuleFor(p => p.TcKimlikNo).NotEmpty();
            RuleFor(p => p.TcKimlikNo).Length(11);
        }
    }
}
