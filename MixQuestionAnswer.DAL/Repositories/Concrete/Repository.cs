using Microsoft.EntityFrameworkCore;
using MixQuestionAnswer.DAL.Repositories.Abstract;
using MixQuestionAnswer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixQuestionAnswer.DAL.Repositories.Concrete
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected MainDbContext _ctx;
        private DbSet<TEntity> _dbSet;
        public Repository(MainDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }
        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result.AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task RemoveAsync(Guid Id)
        {
            var data = await _dbSet.FindAsync(Id);
            data.DeletedDate = DateTime.UtcNow;
            data.IsActive = true;
            await _ctx.SaveChangesAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity obj)
        {
            obj.CreatedDate = DateTime.UtcNow;
            var result = await _dbSet.AddAsync(obj);
            await _ctx.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            var entity = await _dbSet.FindAsync(obj.Id);
            obj.UpdatedDate = DateTime.UtcNow;
            var entry = _ctx.Entry(entity);
            entry.CurrentValues.SetValues(obj);
            entry.Property(x => x.CreatedDate).IsModified = false;
            await _ctx.SaveChangesAsync();
            return entity;
        }
    }
}
