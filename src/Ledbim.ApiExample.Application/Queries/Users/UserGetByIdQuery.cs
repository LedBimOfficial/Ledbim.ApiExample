using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Queries.Users
{
    public class UserGetByIdQuery : IRequest<ApiResponse>
    {
        public Guid UserId { get; set; }
    }
}
