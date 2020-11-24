
using ControleFluxoAPI.Persistence.Contexts;

namespace ControleFluxoAPI.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly ControleFluxoDBContext _context;
        
        public BaseRepository(ControleFluxoDBContext context)
        {
            _context = context;
        }
    }
}
