using ELibrary.Business.Email;
using ELibrary.Business.Helpers.GenerateJWT;
using ELibrary.Business.MappingProfiles;
using ELibrary.Business.Models.User;
using ELibrary.Business.Services.Implement;
using ELibrary.Business.Services.Interface;
using ELibrary.Business.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Business
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IWebHostEnvironment env, IConfiguration configuration)
        {
            services.AddServices(env);

            services.AddEmailConfiguration(configuration);

            services.AddJwtConfiguration(configuration);

            services.RegisterAutoMapper();

            services.AddValidatorsFromAssemblyContaining<IMappingProfilesMarker>();

            return services;
        }

        private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ITransactionService, TransactionService>();
            //services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
            services.AddScoped<IValidator<CreateUserModel>, RegisterValidator>();
            services.AddScoped<IValidator<LoginUserModel>, LoginValidator>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            //services.AddScoped<IValidator<CreateUserModel>, CreateUserValidator>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IMappingProfilesMarker));
        }

        public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        }

        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOption>(configuration.GetSection("JwtSettings"));
        }
    }
}
