using AutoMapper;
using Ledbim.ApiExample.Application.Queries.Users;
using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Models.Users.ResponseModels;
using Ledbim.Core.Base.Responses;
using Ledbim.Core.Helpers.Pagination;
using MediatR;

namespace Ledbim.ApiExample.Application.Handlers.Users
{
    internal sealed class UsersPagedSelectHandler : IRequestHandler<UsersPagedQuery, ApiResponse>
    {
        private readonly IUnitOfWork _repo = null!;
        private readonly IMapper _mapper = null!;

        public UsersPagedSelectHandler(IUnitOfWork repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UsersPagedQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.Users.GetAllPagedAsync(request.FilterRequest);

            var response = _mapper.Map<IEnumerable<UserResponse>>(users.PagedData);

            return PagedHelper.CreatePagedResponse(response, request.FilterRequest.FilterQuery, users.TotalRecords);
        }
    }
}
