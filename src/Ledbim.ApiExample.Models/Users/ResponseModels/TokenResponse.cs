namespace Ledbim.ApiExample.Models.Users.ResponseModels
{
    public class TokenResponse
    {
        public UserResponse User { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public long AccessTokenExpires { get; set; } 

        //public string RefreshToken { get; set; }

        //public long RefreshTokenExpires { get; set; }
    }
}
