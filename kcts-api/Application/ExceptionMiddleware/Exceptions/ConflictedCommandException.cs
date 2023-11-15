using Application.Features.Common.ResponseModels;

namespace Application.ExceptionMiddleware.Exceptions
{
    [Serializable]
    public sealed class ConflictedCommandException : CustomException
    {
        public ConflictedCommandException(ResultResponse resultResponse)
            : base(resultResponse) { }

        public ConflictedCommandException(string message)
            : base(CustomErrorCodes.CONFLICTED_COMMAND, message)
        {
        }

        public ConflictedCommandException(string message, Exception innerException)
            : base(CustomErrorCodes.CONFLICTED_COMMAND, message, innerException) { }

        public ConflictedCommandException(CustomErrorCodes code, string message)
            : base(code, message)
        {
        }

        public ConflictedCommandException(CustomErrorCodes code, string message, Exception innerException)
            : base(code, message, innerException) { }
    }
}
