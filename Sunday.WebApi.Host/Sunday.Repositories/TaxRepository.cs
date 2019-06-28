using Sunday.Repositories.Entities;

namespace Sunday.Repositories
{
    public class TaxRepository : RepositoryBase<Tax>
    {
        public TaxRepository(SundayContext context) : base(context)
        {
        }
    }
}
