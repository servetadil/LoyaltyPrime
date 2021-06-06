using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LoyaltyPrime.Web.Api.Extensions
{
    public static class SwaggerExtensions
    {
        private static readonly string ProductVersion = "v1";

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;
                options.SwaggerDoc(ProductVersion, new OpenApiInfo
                {
                    Title = "LoyaltyPrime Member Management System",
                    Version = ProductVersion
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ProductVersion}/swagger.json", ProductVersion);
                c.RoutePrefix = "api";
            });
        }
    }
}

