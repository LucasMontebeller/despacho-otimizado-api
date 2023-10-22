using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class VeiculoTipo : EntityBaseWithName<byte>
    {
        #region Navegation
        public virtual ICollection<Veiculo> Veiculos {get; protected set; } 
        #endregion
    }
}