namespace API.Core.Models
{
    public enum ResultEnum
    {
        Success,
        Unauthorized,
        Error
    }

    public class OrchestratorResult
    {
        public ResultEnum Result { get; set; }
        public string Message { get; set; }

        public OrchestratorResult()
        {
            Result = ResultEnum.Success;
        }

        public OrchestratorResult Success()
        {
            Result = ResultEnum.Success;
            return this;
        }

        public OrchestratorResult Unauthorized()
        {
            Result = ResultEnum.Unauthorized;
            return this;
        }

        public OrchestratorResult Error(string message = null)
        {
            Result = ResultEnum.Error;
            Message = message;
            return this;
        }
    }

    public class OrchestratorResult<T> : OrchestratorResult
    {
        public T Model { get; set; }

        public OrchestratorResult()
        {
        }

        public OrchestratorResult(T model)
        {
            Model = model;
        }

        public OrchestratorResult<T> Success()
        {
            base.Success();
            return this;
        }

        public OrchestratorResult<T> Unauthorized()
        {
            base.Unauthorized();
            return this;
        }

        public OrchestratorResult<T> Error(string message = null)
        {
            base.Error(message);
            return this;
        }
    }
}