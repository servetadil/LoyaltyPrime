using LoyaltyPrime.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using System;
using System.Threading.Tasks;

namespace LoyaltyPrime.Web.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ErrorHandlingMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = StatusCodes.Status500InternalServerError;
            var message = ex.Message;

            _logger.Error(ex.ToString());

            switch (ex)
            {
                case BusinessRuleException e:
                    code = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;
                case NotFoundException e:
                    code = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = message });
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}
