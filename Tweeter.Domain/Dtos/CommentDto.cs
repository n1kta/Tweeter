using System;

namespace Tweeter.Domain.Dtos
{
    public class CommentDto
    {
        public string Description { get; set; }

        public UserProfileDto UserProfile { get; set; }

        public int Likes { get; set; }
        
        public DateTime AddedDate { get; set; }
    }
}