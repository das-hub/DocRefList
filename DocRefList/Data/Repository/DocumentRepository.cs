using System.Collections.Generic;
using System.Linq;
using DocRefList.Data.Repository.Abstraction;
using DocRefList.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace DocRefList.Data.Repository
{
    public class DocumentRepository : RepositoryBase<DocumentInfo>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<DocumentInfo> GetAll()
        {
            return FindAll().AsNoTracking().OrderByDescending(d => d.SeeBefore).ToList();
        }

        public IPagingList<DocumentInfo> GetPagingList(int current, int size)
        {
            PagingList<DocumentInfo> paging = PagingList.Create(GetAll(), size, current);
            
            paging.Action = "List";

            return paging;
        }

        public DocumentInfo FirstOrDefault(int id)
        {
            return FindByCondition(d => d.Id == id)
                   .Include(d => d.Familiarizations)
                   .ThenInclude(f => f.Employee)
                   .AsNoTracking()
                   .FirstOrDefault();
        }
    }
}
