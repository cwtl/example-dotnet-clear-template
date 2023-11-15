using Application.Features.Common.ResponseModels;

namespace Application.ExceptionMiddleware.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : CustomException
    {
        public EntityNotFoundException(ResultResponse resultResponse)
            : base(resultResponse) { }

        public EntityNotFoundException(string message)
            : base(CustomErrorCodes.ENTITY_NOT_FOUND, message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(CustomErrorCodes.ENTITY_NOT_FOUND, message, innerException) { }

        public EntityNotFoundException(CustomErrorCodes code, string message)
            : base(code, message)
        {
        }

        public EntityNotFoundException(CustomErrorCodes code, string message, Exception innerException)
            : base(code, message, innerException) { }

    }
}
