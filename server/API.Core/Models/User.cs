using System;

namespace API.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public bool IsVerifier { get; set; }
    }
}
