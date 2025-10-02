using Demo.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using FV = FluentValidation;
using System.Net.Mail;

namespace AP.MyGameStore.WebAPI.Middleware
{
    public class OurOwnMiddelware
    {

    }
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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
                var response = new ErrorResponseInfo();
                response.Message = ex.Message;
                switch (ex)
                {
                    case SmtpException:
                        response.StatusCode = StatusCodes.Status200OK;
                        response.Message = "Failed to send email";
                        break;
                    case Demo.Application.Exceptions.ValidationException:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case FV.ValidationException fvEx:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.Message = "Validation failed";
                        response.Errors = fvEx.Errors.Select(e => new
                        {
                            Field = e.PropertyName,
                            Error = e.ErrorMessage
                        });
                        break;
                    case KeyNotFoundException:
                    case RelationNotFoundException:
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }

    public class ErrorResponseInfo
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }
    }
}
