using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Customer;
using FluentValidation;

namespace Bussines.Configuration.Validator.FluentValidation
{
    public class CreateKDCustomerRequestValidator:AbstractValidator<CreateCustomerRequest>
    {
        public CreateKDCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
        }

    }
}
