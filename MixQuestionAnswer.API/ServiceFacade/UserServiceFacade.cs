using MixQuestionAnswer.BLL.Services.Abstract;
using MixQuestionAnswer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MixQuestionAnswer.API.ServiceFacade
{
    public class UserServiceFacade
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserServiceFacade(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<ActionResult> AddRole(Guid userId, Guid roleId)
        {

            var user = await _userService.GetByIdAsync(userId);
            var role = await _roleService.GetByIdAsync(roleId);
            if (user.IsSucceed && role.IsSucceed)
            {
                if (user.Data != null && role.Data != null)
                {
                    var result = await _userService.AddRole(user.Data, roleId);
                    if (result.IsSucceed)
                    {
                        return ActionResult.Succeed();
                    }
                }
            }
            return ActionResult.Failure(user.FailureResult+" " +role.FailureResult);
        }

        public async Task<ActionResult> ChangePassword(Guid Id, string password)
        {
          var result= await _userService.ChangePassword(Id, password);
            if (result.IsSucceed)
                return ActionResult.Succeed();
            return ActionResult.Failure(result.FailureResult);
        }
    }
}
