namespace Library.Middlewares;

public class RequestTrackingId(RequestDelegate _next)
{

    public async Task InvokeAsync(HttpContext context)
    {
        Guid requestId = Guid.NewGuid();
        
        context.Request.Headers["x-request-id"] = requestId.ToString();
        
        await _next(context);
        
    }
    
    
    
    
    
}