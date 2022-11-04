using Dfe.Academies.External.Web.Models.Responses;

namespace Dfe.Academies.External.Web.Middleware;

public class BespokeExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;
    private readonly ILogger _logger;

    public BespokeExceptionHandlingMiddleware(RequestDelegate next, 
											IHostEnvironment environment, 
											ILogger<BespokeExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _environment = environment;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var (statusCode, responseBodyObject) = HandleException(ex);
            
            // re-direct user to error page after exception
			context.Response.Redirect("../Error");
		}
    }

    private (int, BaseErrorResponse) HandleException(Exception exception)
    {
        int statusCode;
        BaseErrorResponse response;

        var devOnlyInfo = new Dictionary<string, object>
        {
            { "ExceptionDetails", UnwrapException(exception) }
        };

        switch (exception)
        {
            case NotImplementedException ex:
                statusCode = StatusCodes.Status501NotImplemented;
                response = new Default501NotImplementedResponse(ex);
                _logger.LogWarning("Non Implemented endpoint accessed.");
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                response = new Default500InternalServerErrorResponse("The server has encountered an unexpected exception. If this issue persists, please contact the support.");
                _logger.LogError(exception, "Unexpected Exception during request execution.");
                break;
        }

        response.DevOnlyInfo = devOnlyInfo;
        response.SerializeDevOnlyInfo = _environment.IsDevelopment();

        return (statusCode, response);
    }

    private static IDictionary<string, object> UnwrapException(Exception ex)
    {
        var response = new Dictionary<string, object>
        {
            { "ExceptionType", ex.GetType().Name },
            { "Message", ex.Message },
            { "StackTrace", ex.StackTrace }
        };

        if (ex.InnerException != null)
            response.Add("InnerException", UnwrapException(ex.InnerException));

        return response;
    }
}
