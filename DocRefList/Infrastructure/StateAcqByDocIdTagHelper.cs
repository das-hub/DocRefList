using System;
using System.Linq;
using System.Threading.Tasks;
using DocRefList.Data.Repository.Abstraction;
using DocRefList.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DocRefList.Infrastructure
{
    [HtmlTargetElement(Attributes = "state-acq-for")]
    public class StateAcqByDocIdTagHelper : TagHelper
    {
        private readonly IDataAccess _dataAccess;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<Employee> _userManager;

        public StateAcqByDocIdTagHelper(IDataAccess dataAccess, IHttpContextAccessor accessor, UserManager<Employee> userManager)
        {
            _dataAccess = dataAccess;
            _accessor = accessor;
            _userManager = userManager;
        }

        public int StateAcqFor { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            DocumentInfo document = _dataAccess.Document.FirstOrDefault(StateAcqFor);

            if (document == null) return;

            Employee employee = await _userManager.GetUserAsync(_accessor.HttpContext.User);

            bool isAcq = document.Familiarizations.Any(f => f.Employee.Id == employee.Id);

            if (!isAcq)
            {
                output.Content.Append(document.SeeBefore > DateTime.Now ? "Ознакомиться" : "Просрочено!");
            }
        }
    }
}
