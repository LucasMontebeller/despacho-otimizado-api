using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class Rota : EntityBase<int>
    {
        public int OrigemId { get; private set; }
        public int DestinoId { get; private set; }
        public int? RotaId { get; private set; }
        public Decimal Distancia { get; private set; }
        public Decimal Tempo { get; private set; }
        public byte[]? Detalhe { get; private set; }

        #region Navegation
        public virtual Localizacao Origem { get; private set; }
        public virtual Localizacao Destino { get; private set; }
        public virtual Rota? RotaNavegation { get; private set; }
        public virtual ICollection<Rota> Rotas { get; private set; }
        #endregion
    }
}