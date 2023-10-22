using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class Maquina : EntityBaseWithName<int>
    {
        public byte Quantidade { get; private set; } 
    }
}