namespace Tweeter.Domain.Dtos
{
    public class ViewProfileDto
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public UserProfileDto UserProfile { get; set; }

        public int Following { get; set; }
        
        public int Followers { get; set; }
    }
}