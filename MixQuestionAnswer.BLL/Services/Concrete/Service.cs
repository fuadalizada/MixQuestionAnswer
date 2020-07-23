using AutoMapper;
using MixQuestionAnswer.BLL.DTOs;
using MixQuestionAnswer.BLL.Services.Abstract;
using MixQuestionAnswer.Common;
using MixQuestionAnswer.DAL.Repositories.Abstract;
using MixQuestionAnswer.Domains;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MixQuestionAnswer.BLL.Services.Concrete
{
    public abstract class Service<TDto, TEntity, TRepository> : IService<TDto>
        where TDto : BaseDTO, new()
        where TEntity : BaseEntity, new()
        where TRepository : IRepository<TEntity>
    {

        private readonly TRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly String _serviceName;

        public Service(TRepository repository, IMapper mapper, String serviceName)
        {
            _repository = repository;
            _logger = LogManager.GetCurrentClassLogger();
            _mapper = mapper;
            _serviceName = serviceName;
        }

        public async Task<ActionResult<IQueryable<TDto>>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var data = _mapper.ProjectTo<TDto>(entities);
                return ActionResult<IQueryable<TDto>>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return ActionResult<IQueryable<TDto>>.Failure(ex.ToString());
            }
            catch (Exception e)
            {
                _logger.LogEx($"{_serviceName}.GetAllAsync.Exception", e);
                return ActionResult<IQueryable<TDto>>.Failure(e.Message);
            }
        }

        public async Task<ActionResult<TDto>> GetByIdAsync(Guid Id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(Id);
                var data = _mapper.Map<TDto>(entity);
                return ActionResult<TDto>.Succeed(data);
            }
            catch (ApplicationException ex)
            {
                return ActionResult<TDto>.Failure(ex.ToString());
            }
            catch (Exception e)
            {
                _logger.LogEx($"{_serviceName}.GetByIdAsync.Exception", e);
                return ActionResult<TDto>.Failure(e.Message);
            }
        }

        public async Task<ActionResult> RemoveAsync(Guid Id)
        {
            try
            {
                await _repository.RemoveAsync(Id);
                return ActionResult.Succeed();
            }
            catch (ApplicationException ex)
            {
                return ActionResult.Failure(ex.ToString());
            }
            catch (Exception e)
            {
                _logger.LogEx($"{_serviceName}.RemoveAsync.Exception", e);
                return ActionResult.Failure(e.Message);
            }
        }

        public async Task<ActionResult<TDto>> SaveAsync(TDto obj)
        {
            if (obj.Id == default(Guid))
                return await CreateAsync(obj);
            return await UpdateAsync(obj);
        }

        #region CreateAndUpdate
        private async Task<ActionResult<TDto>> CreateAsync(TDto obj)
        {
            try
            {
                
                var entity = _mapper.Map<TEntity>(obj);
                var data = await _repository.CreateAsync(entity);
                var dto = _mapper.Map<TDto>(data);
                return ActionResult<TDto>.Succeed(dto);
            }
            catch (ApplicationException ex)
            {
                return ActionResult<TDto>.Failure(ex.ToString());
            }
            catch (Exception e)
            {
                _logger.LogEx($"{_serviceName}.CreateAsync.Exception", e);
                return ActionResult<TDto>.Failure(e.Message);
            }
        }
        private async Task<ActionResult<TDto>> UpdateAsync(TDto obj)
        {
            try
            {

                var entity = _mapper.Map<TEntity>(obj);
                var data = await _repository.UpdateAsync(entity);
                var dto = _mapper.Map<TDto>(data);
                return ActionResult<TDto>.Succeed(dto);
            }
            catch (ApplicationException ex)
            {
                return ActionResult<TDto>.Failure(ex.ToString());
            }
            catch (Exception e)
            {
                _logger.LogEx($"{_serviceName}.UpdateAsync.Exception", e);
                return ActionResult<TDto>.Failure(e.Message);
            }
        }
        #endregion
    }
}
