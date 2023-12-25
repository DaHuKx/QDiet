using FluentValidation;
using FluentValidation.Validators;
using QDiet.Domain.Models.Auth;
using QDiet.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDiet.Domain.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator() 
        {
            RuleFor(reg => reg.UserName).NotNull()
                                                            .NotEmpty()
                                                            .WithName("Логин")
                                                            .MinimumLength(2)
                                                            .MaximumLength(30);

            RuleFor(reg => reg.Email).NotNull()
                                                         .NotEmpty()
                                                         .WithName("Адрес электронной почты")
                                                         .EmailAddress(EmailValidationMode.Net4xRegex);

            RuleFor(reg => reg.Password).NotNull()
                                                            .NotEmpty()
                                                            .WithName("Пароль")
                                                            .MinimumLength(8);
        }
    }
}
