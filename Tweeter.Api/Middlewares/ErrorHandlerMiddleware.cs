using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
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
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = ApiResponse<string>.Fail(ex.Message);

                response.StatusCode = ex switch
                {
                    ApiException e => (int) HttpStatusCode.BadRequest,
                    KeyNotFoundException e => (int) HttpStatusCode.NotFound,
                    ArgumentNullException e => (int) HttpStatusCode.NotFound,
                    UnauthorizedAccessException e => (int) HttpStatusCode.Unauthorized,
                    _ => (int) HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(responseModel);
                
                await response.WriteAsync(result);
            }
        }
    }
}