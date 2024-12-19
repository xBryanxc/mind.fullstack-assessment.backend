using System.Net;
using System.Text;
using System.Text.Json;
using mind.Core.Models;
using Serilog;

namespace mind.API.Middlewares;

public class LogginMiddleware
{
    private readonly RequestDelegate _next;

    public LogginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
	{
        try
        {
            var requestBody = context.Request.Method == "POST" ? await ReadRequestBodyAsync(context.Request) : "{}";

            Log.Information($"{context.Request.Host}{context.Request.Path}{(context.Request.QueryString.HasValue ? context.Request.QueryString.Value : string.Empty)} - {context.Request.Method} - {requestBody}");
            await _next(context);
        }
        catch (Exception ex)
        {
            var requestBody = context.Request.Method == "POST" ? await ReadRequestBodyAsync(context.Request) : "{}";
            Log.Error("======START ERROR======");
            Log.Error(ex, $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm")} - {context.Request.Host}{context.Request.Path}{(context.Request.QueryString.HasValue ? context.Request.QueryString.Value : string.Empty)} - {context.Request.Method} - {requestBody} - {ex.Message}");
            Log.Error("======END ERROR======");

            var apiResponse = new BaseApiResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessages = new List<string>()
                    {
                        ex.ToString()
                    },
                Result = ex.Message
            };

            var response = JsonSerializer.Serialize(apiResponse);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response);
        }
    }

    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();

        request.Body.Position = 0;

        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);

        var body = await reader.ReadToEndAsync();

        using var jsonDocument = JsonDocument.Parse(body);

        var minifiedJsonString = JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        request.Body.Position = 0;

        return minifiedJsonString;
    }

}
