using AutoMapper;
using Ledbim.ApiExample.Application.Commands.Users;
using Ledbim.ApiExample.Application.Helpers;
using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Domain.Entities;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Handlers.Users
{
    internal sealed class UserCreateHandler : IRequestHandler<UserCreateRequest, ApiResponse>
    {
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;

        public UserCreateHandler(IUnitOfWork repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<User>(request);

            var model = await _repo.Users.AddAsync(mapped);

            if (model == null)
                return ApiResponse.UnSuccessCreated(ResultMessages.Fail);

            return ApiResponse.SuccessCreated(ResultMessages.Success);
        }
    }
}