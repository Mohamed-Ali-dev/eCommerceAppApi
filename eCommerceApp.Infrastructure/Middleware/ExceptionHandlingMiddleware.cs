using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();

            try
            {
                await _next(context);
            }
            //This block specifically catches database-related exceptions that occur during database update operations.

            catch (DbUpdateException ex)
            {
                context.Response.ContentType = "application/json";
                if(ex.InnerException is SqlException innerException )
                {
                    logger.LogError(innerException, "Sql Exception");
                    switch (innerException.Number)
                    {
                        case 2627: //unique constraint violation (e.g., duplicate primary key
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                           // await context.Response.WriteAsync("Unique constraint violation");
                            break;
                        case 515: //cannot insert null into a required field
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            //await context.Response.WriteAsync("Cannot insert null");
                            break;
                        case 547://Foreign key constraint violation(e.g., deleting a referenced record). 
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            //await context.Response.WriteAsync("Foreign key constraint violation");
                            break;
                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync(CreateErrorResponse(context.Response.StatusCode, "An error occurred while the entity changes."));
                            break;
                    }
                    var errorMessage = innerException.Number switch
                    {
                        2627 => "Unique constraint violation",
                        515 => "Cannot insert null",
                        547 => "Foreign key constraint violation",
                        _ => "An error occurred while updating the entity."
                    };
                    await context.Response.WriteAsync(CreateErrorResponse(context.Response.StatusCode, errorMessage));
                }
                else
                {
                    logger.LogError(ex, "Related EFCore Exception");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(CreateErrorResponse(context.Response.StatusCode, "An error occurred while the entity changes."));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "UnKown Exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(CreateErrorResponse(context.Response.StatusCode, "An unexpected error occurred."));
            }
        }

        private string CreateErrorResponse(int statusCode, string errorMessage)
        {
            return System.Text.Json.JsonSerializer.Serialize(new
            {
                statusCode = statusCode,
                Message = errorMessage
            });
        }
    }
}
