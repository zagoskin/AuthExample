using AuthExample.Domain.Exceptions;
using AuthExample.Domain.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace AuthExample.API.Configuration;

public static class ExceptionConfiguration
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder)
    {
        // UseExceptionHandler automaticamente faz catch das exceptions que acontecem. 
        applicationBuilder.UseExceptionHandler(app =>
        {
            app.Run(async context =>
            {
                var handler = context.Features.Get<IExceptionHandlerFeature>();
                if (handler is null)
                    return;

                var ex = handler.Error;
                var errorResponse = CreateErrorResponse(ex);
                await WriteErrorResponse(context, errorResponse);
            });
        });
    }
    private static async Task WriteErrorResponse(HttpContext context, ApiErrorResponse errorResponse)
    {        
        context.Response.StatusCode = errorResponse.StatusCode;        
        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private static ApiErrorResponse CreateErrorResponse(Exception ex)
    {
        // Nesse exemplo a gente faz pattern matching para entender
        // qual é o tipo de exception e criar a resposta certa
        ApiErrorResponse errorResponse = new();
        switch (ex)
        {
            case UnauthorizedException uEx:
            {
                errorResponse.StatusCode = 401;
                errorResponse.Errors.Add($"{uEx.Message}");
                break;
            }
            case BusinessRuleException bEx:
            {
                errorResponse.StatusCode = 400;
                errorResponse.Errors.AddRange(bEx.MessageErrors);
                break;
            }
            case NotFoundException userEx:
            {
                errorResponse.StatusCode = 404;
                errorResponse.Errors.Add($"{userEx.Message}");
                break;
            }
            default:
            {
                errorResponse.Errors.Add($"An error ocurred when process the request.");
                errorResponse.StatusCode = 500;
                break;
            }
        }
        return errorResponse;
    }
}