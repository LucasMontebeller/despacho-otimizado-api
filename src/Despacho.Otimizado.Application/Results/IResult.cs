namespace Despacho.Otimizado.Application.Results
{
    public interface IResult
    {
        bool Succeed { get; }
    }

    public interface IResult<out T> : IResult
    {
        public T Data { get; }
    }
}