using GlobalExceptionHandling.Exceptions;
using System.Net;
using System.Text.Json;

using BadRequestException = GlobalExceptionHandling.Exceptions.BadRequestException;
using NotFoundException = GlobalExceptionHandling.Exceptions.NotFoundException;
using KeyNotFoundException = GlobalExceptionHandling.Exceptions.KeyNotFoundException;
using NotImplementedException = GlobalExceptionHandling.Exceptions.NotImplementedException;
using UnauthorizedAccessException = GlobalExceptionHandling.Exceptions.UnauthorizedAccessException;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GlobalExceptionHandling.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
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
        private static Task HandleExceptionAsync (HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message = "";
            var exceptionType = ex.GetType();

            if (exceptionType == typeof(NotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(BadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotImplemented;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = ex.Message;
                status = HttpStatusCode.Unauthorized;
                stackTrace = ex.StackTrace;
            }
            else 
            {
                message = ex.Message;
                status = HttpStatusCode.InternalServerError;
                stackTrace = ex.StackTrace;
            }

            var response = new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "One or more validation errors occurred.",
                status = (int)status,
                stackTrace = stackTrace
            };

            var exceptionResult = JsonSerializer.Serialize(new {error = message, stackTrace = stackTrace});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;


            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
