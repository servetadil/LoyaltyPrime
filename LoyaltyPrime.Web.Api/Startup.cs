using LoyaltyPrime.Application.Configurations;
using LoyaltyPrime.Infrastructure.Extensions;
using LoyaltyPrime.Web.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoyaltyPrime.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence();

            services.AddControllers();

            services.AddHttpContextAccessor();

            // Initialize Dbcontext
            services.AddPersistence();

            // Initialize Application Services
            services.AddApplicationServices();

            // Initialize Swagger
            services.AddSwagger();

            // Initialize Command & Query Handlers
            services.AddCommandQueryHandlers();

            // Initialize Automapper mapping profiles
            services.AddAutoMapperConfigurations();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Add SwaggerUI
            app.UseSwaggerUI();

            // Migrate Db
            PrepareDatabaseExtensions.PrepareDatabase(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
