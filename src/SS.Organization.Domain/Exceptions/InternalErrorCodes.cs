namespace SS.Organizations.Domain.Exceptions
{
    public enum InternalErrorCodes
    {
        UserIsNotMemberOfOrganization = 1001,
        Userhasthesamerole = 1002,
        OrganizationExists = 1003,
        AtLeastOneOwner = 1004,
        ApplicationExists = 1005,
        CannotFindUser = 1006,
        UserDoesntExists = 1007,
        OrganizationDoesntExits = 1008,
    }
    public enum HttpErrorCodes
    {
        ResourceNotFound = 404,
        ResourceExists = 409,
        NotAllowed = 405
    }
}
