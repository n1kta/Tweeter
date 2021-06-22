using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tweeter.Application.Services;
using Tweeter.DataAccess.MSSQL.Context;
using Tweeter.DataAccess.MSSQL.Repositories;
using Tweeter.Domain.Contracts;

namespace Tweeter.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<ITweeterRepository, TweeterRepository>();
            services.AddScoped<DbContext, TweeterContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IFileUploader, FileUploader>();

            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<ITweetService, TweetService>();
            services.AddTransient<ILikeService, TweetService>();

            return services;
        }
    }
}
