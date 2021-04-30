using System;
using API.Core.Platform.Enums;

namespace API.Core.Models
{
    public class UserProfileModel
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsVerifier { get; set; }

        public DateTime UpdatedOn { get; set; }

        public UserStatus Status { get; set; }
    }
}
