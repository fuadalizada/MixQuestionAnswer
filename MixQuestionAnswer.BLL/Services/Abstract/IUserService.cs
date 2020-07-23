using MixQuestionAnswer.BLL.DTOs;
using MixQuestionAnswer.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MixQuestionAnswer.BLL.Services.Abstract
{
    public interface IUserService : IService<UserDTO>
    {
        Task<ActionResult<UserReturnDTO>> Authenticate(string email, string password);
        Task<ActionResult> ChangePassword(Guid Id, string password);
        Task<ActionResult> AddRole(UserDTO userDTO, Guid roleId);
    }
}
