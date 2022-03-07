using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SendikaValidator : AbstractValidator<Sendika>
    {
        public SendikaValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Ad).NotEmpty();
        }
    }
}
