using System;
using System.Collections.Generic;
using System.Text;

namespace MixQuestionAnswer.BLL.DTOs
{
    public class RoleDTO : BaseDTO
    {
        public RoleDTO()
        {
            UserRoles = new List<UserRoleDTO>();
        }
        public String Name { get; set; }
        public virtual ICollection<UserRoleDTO> UserRoles { get; set; }
    }
}
