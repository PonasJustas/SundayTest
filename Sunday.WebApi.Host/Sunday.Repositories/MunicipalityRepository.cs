using Microsoft.EntityFrameworkCore;
using Sunday.Repositories.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Sunday.Repositories
{
    public class MunicipalityRepository : RepositoryBase<Municipality>
    {
        public MunicipalityRepository(SundayContext context) : base(context)
        {
        }

        public override async Task<Municipality> Get(params object[] keys)
        {
            try
            {
                var result = await base.Get(keys);
                return result ?? await DbSet.Include(x => x.Taxes).AsNoTracking()
                           .SingleOrDefaultAsync(x => keys.Any(k => k.Equals(x.Name)));
            }
            catch
            {
            }

            return null;
        }
    }
}
