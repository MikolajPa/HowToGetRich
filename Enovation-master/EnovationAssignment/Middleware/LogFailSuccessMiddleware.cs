using EnovationAssignment.Middleware;

public class LogSuccessFail 
{
    private readonly RequestDelegate _next;



    public LogSuccessFail(RequestDelegate next)
    {
        _next = next;

    }
    public async Task Invoke(HttpContext httpContext)
    {
        await _next.Invoke(httpContext);

        var status = httpContext.Response.StatusCode;
        if (status < 300)
            MetricClass.AddSuccess();
        else if (status >= 400)
            MetricClass.AddFail();


    }
}
public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseMetricsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogSuccessFail>();
    }
}






