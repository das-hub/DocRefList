using AutoMapper;
using DocRefList.Models.DocumentsViewModels;
using DocRefList.Models.Entities;

namespace DocRefList.Models.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentInfo, DocumentViewModel>().ReverseMap();
        }
    }
}
