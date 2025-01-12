namespace eshop.Application.Users
{
    public class RegistrationResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        public static RegistrationResult Success() => new RegistrationResult { IsSuccess = true };
        public static RegistrationResult Failure(string message, int code) => new RegistrationResult { IsSuccess = false, ErrorMessage = message, ErrorCode = code };
    }
}
