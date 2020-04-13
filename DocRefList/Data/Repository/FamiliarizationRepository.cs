using DocRefList.Data.Repository.Abstraction;
using DocRefList.Models.Entities;

namespace DocRefList.Data.Repository
{
    public class FamiliarizationRepository : RepositoryBase<Familiarization>, IFamiliarizationRepository
    {
        public FamiliarizationRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
