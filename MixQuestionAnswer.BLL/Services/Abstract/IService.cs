using MixQuestionAnswer.BLL.DTOs;
using MixQuestionAnswer.Common;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixQuestionAnswer.BLL.Services.Abstract
{
    public interface IService<TDto> where TDto: BaseDTO, new()
    {
        Task<ActionResult<IQueryable<TDto>>> GetAllAsync();
        Task<ActionResult<TDto>> GetByIdAsync(Guid Id);
        Task<ActionResult<TDto>> SaveAsync(TDto obj);
        Task<ActionResult> RemoveAsync(Guid Id);
    }
}
