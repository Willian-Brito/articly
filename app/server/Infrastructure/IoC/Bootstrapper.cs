using System.Reflection;
using Articly.Core.Application.Base;
using Articly.Core.Domain.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Articly.Infrastructure.IoC;

public static class Bootstrapper
{    
    public static void AddDependencies(this IServiceCollection services)
    {
        RegisterServices(typeof(IBaseService<,>).GetTypeInfo().Assembly, services);
        RegisterRepositories(typeof(IBaseRepository<,>).GetTypeInfo().Assembly, services);
    }    

    public static void RegisterRepositories(Assembly assembly, IServiceCollection services)
    {
        foreach (var type in assembly.GetTypes().Where(p => IsRepository(p)))
        {
            Type[] contracts = type.GetInterfaces();

            foreach (var contract in contracts)
                services.AddTransient(contract, type);

            services.AddTransient(type);
        }
    }

    public static void RegisterServices(Assembly assembly, IServiceCollection services)
    {
        foreach (var type in assembly.GetTypes().Where(p => IsService(p)))
        {
            Type[] contracts = type.GetInterfaces();

            foreach (Type contract in contracts)
                services.AddTransient(contract, type);
            
            services.AddTransient(type);
        }
    }

    private static bool IsRepository(Type type)
        {
            return !string.IsNullOrEmpty(type.Namespace)
                && type.Namespace.StartsWith("Articly.")
                && type.Name.EndsWith("Repository")
                && !type.GetTypeInfo().IsAbstract
                && !type.IsGenericParameter;
        }

        private static bool IsService(Type type)
        {
            return !string.IsNullOrEmpty(type.Namespace)
                && type.Namespace.StartsWith("Articly.")
                && type.Name.EndsWith("Service")
                && !type.GetTypeInfo().IsAbstract
                && !type.IsGenericParameter;
        }

}