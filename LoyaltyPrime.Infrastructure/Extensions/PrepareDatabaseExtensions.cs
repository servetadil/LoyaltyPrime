using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyPrime.Infrastructure.Extensions
{
    public static class PrepareDatabaseExtensions
    {
        public static void PrepareDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                //Optional : Seed Data if need.

                context.SaveChanges();

            }
        }
    }
}
