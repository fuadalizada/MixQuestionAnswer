using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.BLL.DTOs
{
    public class UserDTO : BaseDTO
    {
        public UserDTO()
        {
            UserRoles = new HashSet<UserRoleDTO>();
        }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String PasswordHash { get; set; }
        public String PasswordSalt { get; set; }
       
        public virtual ICollection<UserRoleDTO> UserRoles { get; set; }
    }
}
