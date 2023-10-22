namespace Despacho.Otimizado.Domain.Abstractions
{
    public abstract class EntityBaseWithName<TId>
    {
        public string Nome { get; protected set; }
    }
}