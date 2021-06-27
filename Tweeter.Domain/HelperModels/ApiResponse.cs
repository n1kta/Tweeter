namespace Tweeter.Domain.HelperModels
{
    public class ApiResponse<T>
    {
        private T Data { get; set; }

        private bool Succeeded { get; set; }

        private string Message { get; set; }

        public static ApiResponse<T> Fail(string errorMessage)
        {
            return new ApiResponse<T> {Succeeded = false, Message = errorMessage};
        }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> {Succeeded = true, Data = data};
        }
    }
}