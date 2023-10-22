namespace Despacho.Otimizado.Domain.Abstractions
{
    public abstract class EntityBaseWithName<TId> : EntityBase<TId>
    {
        public string Nome { get; protected set; }
    }
}