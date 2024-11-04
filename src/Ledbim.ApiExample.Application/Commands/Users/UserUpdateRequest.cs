using FluentValidation;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Commands.Users
{
    public class UserUpdateRequest : IRequest<ApiResponse>
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Mail { get; set; } = null!;
    }

    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            RuleFor(I => I.UserId).NotNull();
            RuleFor(I => I.FullName).NotNull().MaximumLength(50);
            RuleFor(I => I.Mail).NotNull().EmailAddress().MaximumLength(64);
        }
    };
}
