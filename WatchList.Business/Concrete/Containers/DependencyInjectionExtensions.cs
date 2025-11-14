using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchList.Business.Abstract.Generic;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Abstract.JwtTool;
using WatchList.Core.Tools.Concrete.JwtTool;
using WatchList.DataAccess.Abstract.UnitOfWorkApp;
using WatchList.DataAccess.Concrete.AppContext;
using WatchList.DataAccess.Concrete.UnitOfWorkApp;

namespace WatchList.Business.Concrete.Containers;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<WatchListDbContext>(x =>
        {
            x.UseNpgsql("Server=localhost:5432;Username=hivda;Database=watchlist;Password=1234", conf =>
            {
                conf.MigrationsAssembly("WatchList.Api");
            });
        });
        
        services.AddScoped<IUnitOfWorkApp, UnitOfWorkApp>();
        services.AddScoped<IGenericServiceApp, GenericManagerApp>();
        services.AddScoped<IJwtService, JwtManager>();
    }
}