namespace Ledbim.ApiExample.Models.Users.ResponseModels
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Mail { get; set; } = null!;
    }
}
