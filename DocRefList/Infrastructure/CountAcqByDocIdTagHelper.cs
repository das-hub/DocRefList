using DocRefList.Data.Repository.Abstraction;
using DocRefList.Models.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DocRefList.Infrastructure
{
    [HtmlTargetElement(Attributes = "count-acq-for")]
    public class CountAcqByDocIdTagHelper : TagHelper
    {
        private readonly IDataAccess _dataAccess;

        public CountAcqByDocIdTagHelper(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public int CountAcqFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            DocumentInfo document = _dataAccess.Document.FirstOrDefault(CountAcqFor);
            
            if (document == null) return;

            int count = document.Familiarizations.Count;

            output.Content.Append(count.ToString());
        }
    }
}
