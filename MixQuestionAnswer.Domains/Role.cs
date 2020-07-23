using System;
using System.Collections.Generic;

namespace MixQuestionAnswer.Domains
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public String Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
