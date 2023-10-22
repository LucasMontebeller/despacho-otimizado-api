using Despacho.Otimizado.Domain.Abstractions;

namespace Despacho.Otimizado.Domain.Entities
{
    public class Estrada : EntityBaseWithName<int>
    {
        public decimal VelocidadeMedia { get; private set; }
        public byte EstradaTipoId { get; private set; }
        public byte EstradaSubTipoId { get; private set; }

        #region Navegation
        public virtual EstradaTipo EstradaTipo { get; private set; }
        public virtual EstradaSubTipo EstradaSubTipo { get; private set; }
        #endregion
    }
}