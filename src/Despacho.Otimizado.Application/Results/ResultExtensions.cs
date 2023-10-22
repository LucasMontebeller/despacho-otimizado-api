namespace Despacho.Otimizado.Application.Results
{
    public static class ResultExtensions
    {
        public static IResult<T> Success<T>(this T data) => Result<T>.Success(data);

        public static IResult<T> Fail<T>(this T data) => Result<T>.Fail(data);
    }
}