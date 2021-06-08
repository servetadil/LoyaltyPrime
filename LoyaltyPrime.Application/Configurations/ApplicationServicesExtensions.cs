using LoyaltyPrime.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using LoyaltyPrime.Application.Members.Commands.CreateMember;
using static LoyaltyPrime.Application.Members.Commands.CreateMember.CreateMemberCommand;
using LoyaltyPrime.Application.Accounts.Commands.CreateAccount;
using static LoyaltyPrime.Application.Accounts.Commands.CreateAccount.CreateAccountCommand;
using LoyaltyPrime.Application.Members.Commands.ImportMembers;
using static LoyaltyPrime.Application.Members.Commands.ImportMembers.ImportMembersCommand;

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
            services.AddScoped<IRequestHandler<CreateMemberCommand, CreateMemberResultModel>, CreateMemberCommandHandler>();
            services.AddScoped<IRequestHandler<CreateAccountCommand, CreateAccountResultModel>, CreateAccountCommandHandler>();
            services.AddScoped<IRequestHandler<ImportMembersCommand, ImportMembersResultModel>, ImportMembersCommandHandler>();
            return services;
        }

        public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection services)
        {
            services.AddSingleton(AutoMapperProfileConfigurations.Initialize());
            return services;
        }
    }
}
