namespace SS.Users.Domain.Exceptions
{
    public enum ErrorCodes
    {
        EmailIsTaken = 1101,
        UserNotFound = 1102,
        PasswordMatch = 1103,
        ResourceExists = 409,
        TokenCannotBeRefreshed = 1104
    }

    public enum HttpCodes
    {
        EmailIsTaken = 422,
        ResourceNotFound = 404,
        PasswordMatch = 401,
        ResourceExists = 409,
        NotAllowed = 405
    }
}
