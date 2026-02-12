using FluentValidation;
using OnlineShop.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Validators.Usres
{
    public class RefreshTokenRequestModelValidator:AbstractValidator<RefreshTokenRequestModel>
    {
        public RefreshTokenRequestModelValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(d => d.Token)
                .NotEmpty()
                .WithName("رفرش توکن");

            RuleFor(d => d.UserId)
                .NotEmpty()
                .WithName("شناسه کاربر");
        }
    }
}
