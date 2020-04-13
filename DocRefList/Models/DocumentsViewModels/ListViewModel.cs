using DocRefList.Models.Entities;
using ReflectionIT.Mvc.Paging;

namespace DocRefList.Models.DocumentsViewModels
{
    public class ListViewModel
    {
        public IPagingList<DocumentInfo> Documents { get; set; }
    }
}
