namespace ReportEvcn.Domain.Enum
{
    public enum ErrorCodes
    {
        ReportsNotFound = 0,
        ReportNotFound = 1,
        ReportAlreadyExists = 2,
        UserNotFound = 11,
        UserAlreadyExists = 12,
        UnauthorizedAccess = 13,
        UserAlreadyHaveThisRole = 14,

        PasswordNotEqualsPasswordConfirm = 21,
        PasswordIsWrong = 22,
        InvalidClientRequest = 23,
        RoleAlreadyExists = 31,
        RoleNotFound = 32,
        

        InternarServerError = 44,

    
    }
}
