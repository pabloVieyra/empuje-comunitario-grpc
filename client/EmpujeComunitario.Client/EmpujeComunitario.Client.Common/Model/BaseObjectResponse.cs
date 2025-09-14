namespace EmpujeComunitario.Client.Common.Model
{
    public class BaseObjectResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ValidationErrorResponse> Errors { get; set; } = new();
        public BaseObjectResponse<T> OkWithData(T data, string message = null)
        {
            this.Data = data;
            this.StatusCode = 200;
            if (!string.IsNullOrEmpty(message))
                this.Message = message;
            return this;
        }

        public BaseObjectResponse<T> ExceptionWithData(string message = null)
        {
            this.StatusCode = 500;
            if (!string.IsNullOrEmpty(message))
                this.Message = message;
            return this;
        }
        public BaseObjectResponse<T> BadRequestWithoutData(string message = null)
        {
            this.StatusCode = 400;
            this.Errors = new List<ValidationErrorResponse>();
            if (!string.IsNullOrEmpty(message))
                this.Message = message;
            return this;
        }

    }
}
