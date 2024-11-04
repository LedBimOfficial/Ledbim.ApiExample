using Ledbim.ApiExample.Application.Commands.Users;
using Ledbim.ApiExample.Application.Helpers;
using Ledbim.ApiExample.Domain.Base;
using Ledbim.Core.Base.Responses;
using MediatR;

namespace Ledbim.ApiExample.Application.Handlers.Users
{
    internal sealed class UserDeleteHandler : IRequestHandler<UserDeleteRequest, ApiResponse>
    {
        private readonly IUnitOfWork _repo = null!;

        public UserDeleteHandler(IUnitOfWork repo)
            => _repo = repo;

        public async Task<ApiResponse> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            var user = await _repo.Users.GetAsync(new(I => I.Id == request.UserId));
            if (user == null)
                return ApiResponse.UnSuccessDeleted(ResultMessages.UserNotFound);

            var result = await _repo.Users.DeleteAsync(user);
            if (!result)
                return ApiResponse.Fail(ResultMessages.Fail);

            return ApiResponse.SuccessDeleted(ResultMessages.Success);
        }
    }
}
