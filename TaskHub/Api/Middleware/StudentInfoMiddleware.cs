namespace Api.Middleware;

public class StudentInfoMiddleware
{
    private readonly RequestDelegate _next;

    public StudentInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-Student-Group"] = "PU-240930";
            context.Response.Headers["X-Student-Name"] = "Baskov Andrei Alekseevich";
            return Task.CompletedTask;
        });
        await _next(context);
    }
}