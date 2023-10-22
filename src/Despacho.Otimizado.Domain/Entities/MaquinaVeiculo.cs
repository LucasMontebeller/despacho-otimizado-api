using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class MaquinaVeiculo : EntityBase<int>
    {
        public int MaquinaId { get; private set; }
        public int VeiculoId { get; private set; }

        #region Navegation
        public virtual Maquina Maquina { get; private set; }
        public virtual Veiculo Veiculo { get; private set; }
        #endregion
    }
}