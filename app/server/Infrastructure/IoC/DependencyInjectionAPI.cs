using System.Data;
using Articly.Core.Application.Auth.Interfaces;
using Articly.Core.Application.Interfaces;
using Articly.Core.Application.Mappings;
using Articly.Core.Application.Services;
using Articly.Core.Domain.Account;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Interfaces;
using Articly.Infrastructure.Data.Base;
using Articly.Infrastructure.Data.Context;
using Articly.Infrastructure.Data.Jobs;
using Articly.Infrastructure.Data.MongoDB.Settings;
using Articly.Infrastructure.Data.Repositories;
using Articly.Infrastructure.Security.Auth;
using Articly.Infrastructure.Security.Hashs;
using Articly.Infrastructure.Security.Sanitizer;
using Articly.Infrastructure.Security.Sessions;
using Articly.Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Articly.Infrastructure.IoC;

public static class DependencyInjectionAPI
{
    #region AddInfrastructureAPI
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database

        #region Postgres
        var postgresConnection = configuration.GetConnectionString("Postgres");        

        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(postgresConnection, 
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddSingleton<IDbConnection>(provider => 
        {
            var connection = new NpgsqlConnection(postgresConnection);
            connection.Open();
            return connection;
        });

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        #endregion

        #region MongoDB
        var mongoConnection = configuration.GetSection("MongoDBSettings");
        services.Configure<MongoDBSettings>(mongoConnection);
        #endregion

        #endregion

        #region Unity Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IHtmlSanitizer, HtmlSanitizerService>();
        #endregion

        #region Repository

        #region Entity Framework
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        #endregion

        #region MongoDB Driver
        services.AddScoped<IStatRepository, StatRepository>();
        #endregion

        // Bootstrapper.AddDependencies(services);  
        // services.AddDependencies();

        #endregion

        #region Jobs
        services.AddSingleton<JobScheduler>();
        services.AddSingleton<StatsJobScheduler>();
        #endregion

        #region Auth
        services.AddScoped<IAuthenticateService, AuthenticateService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        #endregion

        #region Auto Mapper
        services.AddAutoMapper(typeof(DomainToDtoMapping));
        #endregion

        #region MediatoR
        var handlers = AppDomain.CurrentDomain.Load("Application");
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(handlers));
        #endregion

        return services;
    }
    #endregion

    #region ApplyMigrations
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {            
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();   
            context.Database.Migrate();
        }
    }
    #endregion

    #region UseJobSchedule
    public static void UseJobSchedule(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {            
            var jobScheduler = serviceScope.ServiceProvider.GetRequiredService<JobScheduler>();  
            jobScheduler.ScheduleJobs();
        } 
    }
    #endregion
}