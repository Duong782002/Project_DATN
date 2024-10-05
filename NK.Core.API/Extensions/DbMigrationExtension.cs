using Microsoft.EntityFrameworkCore;
using NK.Core.Model;

namespace SH.Core.API.Extensions
{
    public static class DbMigrationExtension
    {
        public static void UseDbMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
        }

        public static void UseDataSeeding(this IApplicationBuilder app, bool isDevelopment = false)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                var config = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();

                DataSeeding.DevelopmentSeed(context, config);
                if (isDevelopment)
                {
                }
                else
                {
                }
            }
        }
    }
}
