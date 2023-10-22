using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class Localizacao : EntityBaseWithName<int>
    {
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set;}
        public bool Garagem { get; private set; }
    }
}