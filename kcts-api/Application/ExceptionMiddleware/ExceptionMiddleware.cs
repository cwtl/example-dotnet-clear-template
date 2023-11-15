using Application.ExceptionMiddleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.ExceptionMiddleware
{
    public class ExceptionMiddleware : AbstractExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) : base(next)
        {
            _logger = logger;
        }

        public override (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            HttpStatusCode code;
            switch (exception)
            {
                //if we know it is a custom exception we throw the bad request status code 400
                case CustomException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            _logger.LogError(exception, exception.Message);
            return (code, exception.Message);
        }
    }
}
