using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Common.Interfaces;
using Weblog.Application.Common.Models;
using Weblog.Application.CustomExceptions;

namespace Weblog.API.Middleware
{
        public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IErrorService _errorService;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IErrorService errorService)
        {
            _next = next;
            _logger = logger;
            _errorService = errorService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                List<string>? details = [];
                if (ex.Details?.Count > 0)
                {
                    foreach (string item in ex.Details)
                    {
                        details.Add(_errorService.GetMessage(item));
                    }
                }

                var response = new ApiErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = _errorService.GetMessage(ex.Message),  // <-- Localize here
                    ErrorCode = ex.ErrorCode,
                    Details = details
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = new ApiErrorResponse
                {
                    StatusCode = 500,
                    Message = _errorService.GetMessage("INTERNAL_SERVER_ERROR"), // Localize internal error
                    ErrorCode = "INTERNAL_SERVER_ERROR"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}