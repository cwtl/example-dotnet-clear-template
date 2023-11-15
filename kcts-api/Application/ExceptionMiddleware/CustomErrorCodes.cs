namespace Application.ExceptionMiddleware
{
    public enum CustomErrorCodes
    {
        ENTITY_NOT_FOUND,
        ROLE_DELETE_NOT_ALLOWED_CONSTRAINT,
        NOT_AUTHORIZED,
        CONFLICTED_COMMAND
    }
}
