using DocRefList.Models.Entities;
using ReflectionIT.Mvc.Paging;

namespace DocRefList.Models.AdminViewModels
{
    public class ListViewModel
    {
        public IPagingList<Employee> Users { get; set; }
    }
}
