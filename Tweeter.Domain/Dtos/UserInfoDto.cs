namespace Tweeter.Domain.Dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public UserProfileDto UserProfile { get; set; }
    }
}
