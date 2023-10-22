namespace Despacho.Otimizado.Application.Results
{
    public class Result : IResult
    {
        protected Result(bool succeed)
        {
            Succeed = succeed;
        }
        public bool Succeed { get; private set; }
    }

    public class Result<T> : Result, IResult<T>
    {
        private Result(T data, bool Succeed) : base(Succeed)
        {
            Data = data;
        }

        public T Data { get; private set; }

        public static IResult<T> Fail(T data) => new Result<T>(data, false);
        public static IResult<T> Success(T data) => new Result<T>(data, true);

    }
}