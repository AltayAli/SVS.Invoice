using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SVS.Invoice.Api.Extensions
{
    public static class HostBuilderExtension
    {
        public static IHost MigrateDatabase<TDBContext>(this IHost host) where TDBContext : DbContext
        {
            using (var scopedService = host.Services.CreateScope())
            {
                try
                {
                    TDBContext _dataContext = scopedService.ServiceProvider.GetRequiredService<TDBContext>();
                    _dataContext.Database.Migrate();
                    _dataContext.Dispose();
                }
                catch { throw; }
            }

            return host;
        }
    }
}