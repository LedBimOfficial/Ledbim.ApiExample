using AutoMapper;
using Ledbim.ApiExample.Application.Helpers;
using Ledbim.ApiExample.Application.Queries.Users;
using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Models.Users.ResponseModels;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Handlers.Users
{
    internal sealed class UserGetByIdSelectHandler : IRequestHandler<UserGetByIdQuery, ApiResponse>
    {
        private readonly IUnitOfWork _repo = null!;
        private readonly IMapper _mapper = null!;

        public UserGetByIdSelectHandler(IUnitOfWork repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.Users.GetAsync(new(I => I.Id == request.UserId));

            if (user == null)
                return ApiResponse.NotFound(ResultMessages.UserNotFound);

            var response = _mapper.Map<UserResponse>(user);

            return ApiResponse<UserResponse>.Success(response, ResultMessages.Success);
        }
    }
}