

using System;

namespace MixQuestionAnswer.BLL.DTOs
{
    public class UserRoleDTO
    {
        public Guid  UserId { get; set; }
        public UserDTO UserDto { get; set; }
        public Guid RoleId { get; set; }
        public RoleDTO RoleDto { get; set; }
    }
}
