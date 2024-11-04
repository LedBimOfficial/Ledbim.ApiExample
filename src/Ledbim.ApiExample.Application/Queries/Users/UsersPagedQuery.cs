using Ledbim.ApiExample.Domain.Entities;
using Ledbim.Core.Base.Responses;
using Ledbim.Core.Helpers.Pagination;
using MediatR;

namespace Ledbim.ApiExample.Application.Queries.Users
{
    public class UsersPagedQuery : IRequest<ApiResponse>
    {
        public PagedFilterRequest<User> FilterRequest { get; set; }
    }
}
