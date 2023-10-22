namespace Despacho.Otimizado.Application.Results
{
    public class Result : IResult
    {
        public Result()
        { }
        protected Result(bool succeed, string? error = null)
        {
            Succeed = succeed;
            Error = error;
        }
        public bool Succeed { get; private set; }
        public string? Error { get; private set; }

        public static IResult Fail(string? error = null) => new Result(false, error);
    }

    public class Result<T> : Result, IResult<T>
    {
        private Result(T data, bool succeed, string? error = null) : base(succeed, error)
        {
            Data = data;
        }

        public T Data { get; private set; }

        public static IResult<T> Fail(T data, string? error = null) => new Result<T>(data, false, error);
        public static IResult<T> Success(T data) => new Result<T>(data, true);

    }
}