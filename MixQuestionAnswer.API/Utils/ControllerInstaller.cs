using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MixQuestionAnswer.DAL;
using Microsoft.Extensions.Configuration;
using NSwag;
using System;

namespace MixQuestionAnswer.API.Utils
{
    public class ControllerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddControllers();

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = doc =>
                {
                    doc.Info.Title = AppSettings.Title;
                    doc.Info.Version = "0.0.0.1";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "Urfan Ibrahimli",
                        Email = "urfani@list.ru",
                        Url = "https://github.com/UrfanIbrahimli"
                    };
                };
                config.AddSecurity("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In=OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });
        }
    }
}
