using AutoMapper;
using Ledbim.ApiExample.Application.Helpers;
using Ledbim.ApiExample.Application.Queries.Users;
using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Models.Users.ResponseModels;
using Ledbim.Core.Base.Responses;
using Ledbim.Core.Security;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace Ledbim.ApiExample.Application.Handlers.Users
{
    internal sealed class UserLoginSelectHandler : IRequestHandler<UserLoginQuery, ApiResponse<TokenResponse>>
    {
        private readonly IUnitOfWork _repo;
        private readonly ITokenSettings _settings;
        private readonly IMapper _mapper;

        public UserLoginSelectHandler(IUnitOfWork repo, ITokenSettings settings, IMapper mapper)
        {
            _repo = repo;
            _settings = settings;
            _mapper = mapper;
        }

        public async Task<ApiResponse<TokenResponse>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            // Kullanıcı Login için Veritabanında aranıyor. 
            var user = await _repo.Users.GetAsync(new(u => (u.Mail.Equals(request.Mail) && u.Password.Equals(request.Password))));

            if (user == null)
                return ApiResponse<TokenResponse>.UnAuthorized(ResultMessages.LoginFailed);

            // AccessToken ve RefreshToken üretiliyor. 
            JwtSecurityTokenHandler tokenHandler = new();
            var accessToken = JwtSecurity.GetAccessToken(_settings, user);
            var refreshToken = JwtSecurity.GetRefreshToken(_settings, user);
            //user.RefreshToken = tokenHandler.WriteToken(refreshToken);

            //// RefreshToken kullanıcı satırında güncelleniyor. 
            //await _repo.Users.UpdateAsync(user);

            // User bilgisi Response eklenmesi için mapleniyor.
            var userResponse = _mapper.Map<UserResponse>(user);

            TokenResponse tokenResponse = new()
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                AccessTokenExpires = TimeSpan.FromHours(_settings.AccessTokenExpiration).Ticks,

                //RefreshToken = tokenHandler.WriteToken(refreshToken),
                //RefreshTokenExpires = TimeSpan.FromHours(_settings.RefreshTokenExpiration).Ticks,

                User = userResponse
            };

            return ApiResponse<TokenResponse>.Authorized(tokenResponse, ResultMessages.Success);
        }
    }
}
