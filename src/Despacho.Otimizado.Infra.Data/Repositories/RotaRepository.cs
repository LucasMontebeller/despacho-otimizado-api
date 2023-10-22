using Despacho.Otimizado.Domain.Entities;
using Despacho.Otimizado.Domain.Interfaces;
using Despacho.Otimizado.Infra.Data.Context;

namespace Despacho.Otimizado.Infra.Data.Repositories
{
    public class RotaRepository : BaseRepository<Rota, int>, IRotaRepository
    {
        public RotaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}