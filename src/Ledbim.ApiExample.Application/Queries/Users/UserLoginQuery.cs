using FluentValidation;
using Ledbim.ApiExample.Models.Users.ResponseModels;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Queries.Users
{
    public class UserLoginQuery : IRequest<ApiResponse<TokenResponse>>
    {
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(I => I.Mail).NotNull();
            RuleFor(I => I.Password).NotNull();
        }
    }
}
