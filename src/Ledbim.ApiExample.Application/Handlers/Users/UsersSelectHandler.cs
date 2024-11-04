using AutoMapper;
using Ledbim.ApiExample.Application.Helpers;
using Ledbim.ApiExample.Application.Queries.Users;
using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Models.Users.ResponseModels;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Handlers.Users
{
    internal sealed class UsersSelectHandler : IRequestHandler<UsersQuery, ApiResponse>
    {
        private readonly IUnitOfWork _repo = null!;
        private readonly IMapper _mapper = null!;
        public UsersSelectHandler(IUnitOfWork repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.Users.GetAllAsync();

            var response = _mapper.Map<IEnumerable<UserResponse>>(users);

            return ApiResponse<IEnumerable<UserResponse>>.Success(response, ResultMessages.Success);
        }
    }
}
