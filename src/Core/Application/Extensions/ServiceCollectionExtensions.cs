using CleanApi.Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApi.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<IRequestValidator>();
            return services;
        }
    }
}