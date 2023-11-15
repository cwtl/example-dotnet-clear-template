using Application.Features.Common.ResponseModels;

namespace Application.ExceptionMiddleware.Exceptions
{
    public class NotAuthorizedCommandException : CustomException
    {
        public NotAuthorizedCommandException(ResultResponse resultResponse)
            : base(resultResponse) { }

        public NotAuthorizedCommandException(string message)
            : base(CustomErrorCodes.NOT_AUTHORIZED, message) { }

        public NotAuthorizedCommandException(string message, Exception innerException)
            : base(CustomErrorCodes.NOT_AUTHORIZED, message, innerException) { }

    }
}
