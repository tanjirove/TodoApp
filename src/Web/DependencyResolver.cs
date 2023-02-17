using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Common.Contracts;
using Todo.Infrastructure.Persistence;

namespace API
{
    public static class DependencyResolver
    {
        public static IServiceCollection ResolveApiServices(
            this IServiceCollection services)
        {
            // Application
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = AppDomain.CurrentDomain.Load("Todo.Application");

            services.AddAutoMapper(assemblies);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assemblies);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Todo"));

            //In memory database. We also connect to ms sql server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Todo"));
 
            #region Infrastructure

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            
            #endregion

            return services;
        }
    }
}
