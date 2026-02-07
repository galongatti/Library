namespace Library.Middlewares;

public class RequestTrackingId(RequestDelegate _next)
{

    public async Task InvokeAsync(HttpContext context)
    {
        String requestId = Guid.NewGuid().ToString();
        
        if(context.Request.Headers["x-request-id"].Count == 0)
        {
            context.Request.Headers["x-request-id"] = requestId;
        }
        await _next(context);
        
    }
    
    
    
    
    
}