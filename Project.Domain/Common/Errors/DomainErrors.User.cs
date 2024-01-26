namespace Project.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        public static Error DuplicatEmail => new Error(code: "User.DuplicateEmail", message: "Email already exist");
        public static Error UserNotFound => new Error(code: "User.NotFound", message: "User can not found");


    }
}