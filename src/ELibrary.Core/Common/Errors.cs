namespace ELibrary.Core.Common
{
    public record Errors(string code, string message)
    {
        public static Errors None = new Errors(string.Empty, string.Empty);
        public static Errors NotFound = new Errors("Error.NotFound", "Not found");
        public static Errors Unauthorized = new Errors("Error.Unauthorized", "Unauthorized");
        public static Errors Forbidden = new Errors("Error.Forbidden", "Forbidden");
        public static Errors Conflict = new Errors("Error.Conflict", "Conflict");
        public static Errors InternalServerError = new Errors("Error.InternalServerError", "Internal Server Error");
        public static Errors LoginFailed = new Errors("Error.LoginFailed", "Username or Password is incorrect");
        public static Errors DatabaseError = new Errors("Error.DatabaseError", "Database error");
        public static Errors InvalidOperation = new Errors("Error.InvalidOperation", "Invalid operation");
        public static Errors InsufficientBalance = new Errors("Error.InsufficientBalance", "Insufficient balance"); // ← yangi
        public static Errors NoCopiesAvailable = new Errors("Error.NoCopiesAvailable", "No copies available");     // ← yangi
    }
}
