using System.Collections.Generic;
using DocRefList.Models.Entities;
using ReflectionIT.Mvc.Paging;

namespace DocRefList.Data.Repository.Abstraction
{
    public interface IDocumentRepository : IRepositoryBase<DocumentInfo>
    {
        IEnumerable<DocumentInfo> GetAll();

        IPagingList<DocumentInfo> GetPagingList(int current, int size);

        DocumentInfo FirstOrDefault(int id);
    }
}
