using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MixQuestionAnswer.API.Models
{
    public class UserModel : BaseModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PasswordHash { get; set; }
        public String Email { get; set; }
        public Guid RoleId { get; set; }

        public RoleModel Role { get; set; }
    }
}
