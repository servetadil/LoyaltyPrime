using LoyaltyPrime.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using LoyaltyPrime.Application.Member.Commands.CreateMember;
using static LoyaltyPrime.Application.Member.Commands.CreateMember.CreateMemberCommand;

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

        public static IServiceCollection AddCommandQueryHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateMemberCommand, Unit>, CreateMemberCommandHandler>();
            return services;
        }
    }
}
