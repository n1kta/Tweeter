using System;

namespace Tweeter.Domain.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        
        public string Description { get; set; }

        public UserProfileDto UserProfile { get; set; }

        public int Likes { get; set; }
        
        public bool? IsLiked { get; set; }
        
        public DateTime AddedDate { get; set; }
    }
}