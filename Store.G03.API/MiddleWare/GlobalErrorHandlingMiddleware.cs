using Domain.Exceptions;
using Store.G03.API.ErrorModels;

namespace Store.G03.API.MiddleWare
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context) {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound) {
                    await HandlingNotFoundEndPointAsync(context);
                }
            }
            catch (Exception ex) {
            _logger.LogError(ex,ex.Message);

                //1- set status code for Response
                //2- set Content Type Code For Response
                // 3- response object (body)
                // 4- Return reponse 

                await HandlingErrorAsync(context, ex);
            
            }
        
        }

        private async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message

            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException => HandlingValidationExceptionAsync((ValidationException)ex, response),
                _ => StatusCodes.Status500InternalServerError,


            };

            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End Point {context.Request.Path} is not found"

            };
            await context.Response.WriteAsJsonAsync(response);
        }

        private static int  HandlingValidationExceptionAsync(ValidationException ex, ErrorDetails response)
        {
            response.Errors = ex.Errors; 
            return StatusCodes.Status400BadRequest;
        }

    }
}
