using LoyaltyPrime.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using LoyaltyPrime.Application.Member.Commands.CreateMember;

namespace LoyaltyPrime.Application.Configurations
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateMemberCommandValidator>());

            return services;
        }

        public static IServiceCollection AddServiceHandlers(this IServiceCollection services)
        {

            return services;
        }
    }
}
