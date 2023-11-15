namespace Application.Features.Auth.ResponseModels
{
    public class UserResponse
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
