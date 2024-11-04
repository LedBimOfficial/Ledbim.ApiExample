using AutoMapper;
using Ledbim.ApiExample.Application.Commands.Users;
using Ledbim.ApiExample.Application.Helpers;
using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Models.Users.ResponseModels;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Handlers.Users
{
    internal sealed class UserUpdateHandler : IRequestHandler<UserUpdateRequest, ApiResponse>
    {
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;

        public UserUpdateHandler(IUnitOfWork repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var model = await _repo.Users.GetAsync(new(a => a.Id == request.UserId));

            if (model == null)
                return ApiResponse.UnSuccessUpdated(ResultMessages.UserNotFound);

            var mapped = _mapper.Map(request, model);

            var response = await _repo.Users.UpdateAsync(mapped);

            if (response == null)
                return ApiResponse.Fail(ResultMessages.Fail);

            var userResponse = _mapper.Map<UserResponse>(response);

            return ApiResponse.Success(ResultMessages.Success);
        }
    }
}
