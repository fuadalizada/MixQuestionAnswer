using MixQuestionAnswer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixQuestionAnswer.DAL.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity: BaseEntity, new ()
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid Id);
        Task<TEntity> CreateAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task RemoveAsync(Guid Id);
    }
}
