using System.ComponentModel.DataAnnotations;

namespace Tweeter.Domain.Dtos
{
    public class UserProfileDto
    {
        public string FullName { get; set; }

        public string BIO { get; set; }

        public string Photo { get; set; }

        public string Phone { get; set; }
    }
}
