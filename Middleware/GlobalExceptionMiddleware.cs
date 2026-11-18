using System.Text.Json;
using TodoApi.Exceptions;

namespace TodoApi.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Terjadi kesalahan pada aplikasi.");

            var statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            var message = statusCode == 500
            ?"Terjadi kesalahan pada server."
            : ex.Message;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

        var response =new
        {
            statusCode = statusCode,
            message = message
        };
        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
        }
    }
}
