namespace Tweeter.Domain.Dtos
{
    public class FollowerDto
    {
        public int Followers { get; set; }

        public int Followings { get; set; }

        public bool IsCurrentUserProfileFollowed { get; set; }
    }
}
