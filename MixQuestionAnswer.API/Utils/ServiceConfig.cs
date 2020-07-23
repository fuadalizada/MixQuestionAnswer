using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MixQuestionAnswer.BLL.Services.Abstract;
using MixQuestionAnswer.BLL.Services.Concrete;
using MixQuestionAnswer.DAL;
using MixQuestionAnswer.DAL.Repositories.Abstract;
using MixQuestionAnswer.DAL.Repositories.Concrete;

namespace MixQuestionAnswer.API.Utils
{
    public class ServiceConfig : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options =>
                    options.UseSqlServer(AppSettings.ConnectionString));
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterMappers(services);
        }
        private  void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            
        }
        private  void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBlogService, BlogService>();
        }
        private void RegisterMappers(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BLL.DTOs.MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
