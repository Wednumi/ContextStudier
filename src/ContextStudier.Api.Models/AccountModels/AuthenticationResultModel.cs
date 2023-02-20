namespace ContextStudier.Presentation.Core.AccountModels
{
    public class AuthenticationResultModel
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public AuthenticationResultModel(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public AuthenticationResultModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
