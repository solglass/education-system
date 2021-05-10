using EducationSystem.Core.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace EducationSystem.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private const string GlobalErrorMessage = "An error occured while processing the request.";
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (SqlException ex)
            {
                await HandleSqlExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            ModifyContextResponse(context, exception.StatusCode);

            return ConstructResponse(context, exception.StatusCode, exception.ErrorMessage);
        }

        private Task HandleSqlExceptionAsync(HttpContext context, SqlException exception)
        {
            var keys = new string[] { "UC_CourseTheme_CourseId_ThemeId", "UC_CourseTheme_CourseId_Order"};
            var result = keys.FirstOrDefault<string>(s => exception.Message.Contains(s));
            switch (result)
            {
                case "UC_CourseTheme_CourseId_ThemeId":
                    ModifyContextResponse(context, (int)HttpStatusCode.Conflict);
                    return ConstructResponse(context, 409, "The pair of course and theme is already exists.");

                case "UC_CourseTheme_CourseId_Order":
                    ModifyContextResponse(context, (int)HttpStatusCode.Conflict);
                    return ConstructResponse(context, 409, "Order of every theme in the course have to be unique.");

                default:
                    return HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ModifyContextResponse(context, (int)HttpStatusCode.BadRequest);

            return ConstructResponse(context, (int)HttpStatusCode.BadRequest, exception.Message);
        }

        private void ModifyContextResponse(HttpContext context, int statusCode)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = statusCode;
        }

        private Task ConstructResponse(HttpContext context, int statusCode, string message)
        {
            var errorResponse = JsonSerializer.Serialize(
                new
                {
                    Code = statusCode,
                    Message = message
                });

            return context.Response.WriteAsync(errorResponse);
        }
    }
}
