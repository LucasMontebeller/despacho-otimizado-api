using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class Veiculo : EntityBaseWithName<int>
    {
        public byte VeiculoTipoId { get; private set; }
        public byte Quantidade { get; private set; }
        public Decimal Capacidade { get; private set; }

        #region Navegation
        public virtual VeiculoTipo VeiculoTipo { get; private set; }
        #endregion
    }
}