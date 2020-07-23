using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MixQuestionAnswer.API.Utils
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
