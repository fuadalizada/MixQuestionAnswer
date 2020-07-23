using System;
using System.Collections.Generic;
namespace MixQuestionAnswer.Domains
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
