using Application.Features.Common.ResponseModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.ExceptionMiddleware.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(ResultResponse resultResponse)
            : base(TransformMessage(resultResponse)) { }

        public CustomException(CustomErrorCodes code, string message)
            : base(TransformMessage(code, message)) { }

        public CustomException(CustomErrorCodes code, string message, Exception innerException)
            : base(TransformMessage(code, message), innerException) { }

        protected static string TransformMessage(CustomErrorCodes code, string message)
        {
            var resultResponse = new ResultResponse()
            {
                Succeeded = false,
                Errors = new List<ErrorInfo>()
                    {
                        new ErrorInfo()
                        {
                            Code = Enum.GetName(typeof(CustomErrorCodes), code) ?? nameof(StatusCodes.Status500InternalServerError),
                            Description = message
                        }
                    }
            };
            return JsonConvert.SerializeObject(resultResponse);
        }

        protected static string TransformMessage(ResultResponse resultResponse)
        {
            return JsonConvert.SerializeObject(resultResponse);
        }

    }
}
