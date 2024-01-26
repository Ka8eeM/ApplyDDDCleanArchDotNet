namespace Project.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class Authentication
    {
        public static Error InvlaidCredentials => new Error(
            code: "Auth.InvalidCredential",
            message: "Invalid Credentials");
    }
}