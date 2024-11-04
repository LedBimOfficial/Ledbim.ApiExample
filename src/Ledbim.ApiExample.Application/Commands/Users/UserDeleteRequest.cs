using FluentValidation;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Commands.Users
{
    public class UserDeleteRequest : IRequest<ApiResponse>
    {
        public Guid UserId { get; set; }
    }
    public class UserDeleteRequestValidator : AbstractValidator<UserDeleteRequest>
    {
        public UserDeleteRequestValidator()
        {
            RuleFor(I => I.UserId).NotNull();
        }
    };
}
