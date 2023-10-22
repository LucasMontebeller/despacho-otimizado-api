using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class MaquinaVeiculoRota : EntityBaseWithName<int>
    {
        public int MaquinaVeiculoId { get; private set; } 
        public int RotaId { get; private set; }

        #region Navegation
        public virtual MaquinaVeiculo MaquinaVeiculo { get; private set; }
        public virtual Rota Rota { get; private set; }
        #endregion
    }
}