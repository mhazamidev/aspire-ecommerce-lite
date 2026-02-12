using FluentValidation;
using OnlineShop.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Validators.Usres
{
    public class TokenRequestModelValidator:AbstractValidator<TokenRequestModel>
    {
        public TokenRequestModelValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.UserName)
                .NotEmpty()
                .WithName("نام کاربری");

            RuleFor(d => d.Password)
                .NotEmpty()
                .WithName("رمز عبور");
        }
    }
}
