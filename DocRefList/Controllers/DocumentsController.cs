using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocRefList.Data.Repository.Abstraction;
using DocRefList.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DocRefList.Models;
using DocRefList.Models.DocumentsViewModels;
using DocRefList.Models.Entities;
using DocRefList.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DocumentInfo = DocRefList.Models.Entities.DocumentInfo;

namespace DocRefList.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<DocumentsController> _logger;
        private readonly IDataAccess _dataAccess;
        private readonly IMapper _mapper;
        private readonly FileStorage _storage;

        public DocumentsController(
            IDataAccess dataAccess,
            UserManager<Employee> userManager,
            IMapper mapper,
            FileStorage storage,
            ILogger<DocumentsController> logger)
        {
            _dataAccess = dataAccess;
            _userManager = userManager;
            _mapper = mapper;
            _storage = storage;
            _logger = logger;
        }

        public IActionResult List(int page = 1)
        {
            ListViewModel model = new ListViewModel
            {
                Documents = _dataAccess.Document.GetPagingList(page, 6)
            };
            
            return View(model);
        }

        [Authorize(Roles = RoleNames.ADMIN)]
        public IActionResult Create()
        {
            return View(new DocumentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> Create(DocumentViewModel document)
        {
            if (!ModelState.IsValid)
                return View(document);

            DocumentInfo info = _mapper.Map<DocumentInfo>(document);

            info.Employee = await GetCurrentUserAsync();
            info.CreatedAt = DateTime.Now;

            _dataAccess.Document.Create(info);
            _dataAccess.Save();

            _logger.LogInformation($"Создан {info}");

            _storage[info.Id].AddFormFiles(document.Files);

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = RoleNames.ADMIN)]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentInfo document = _dataAccess.Document.FirstOrDefault(id.Value);  

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ADMIN)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            DocumentInfo document = _dataAccess.Document.FirstOrDefault(id);

            _dataAccess.Document.Delete(document);
            _storage[id].Clear();

            _dataAccess.Save();

            Employee employee = await GetCurrentUserAsync();

            _logger.LogInformation($"Подтверждение на удаление {document}. Документ успешно удален [{employee.FullName}({employee.Email})]");

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentInfo document = _dataAccess.Document.FirstOrDefault(id.Value);

            if (document == null)
            {
                return NotFound();
            }

            Employee employee = await GetCurrentUserAsync();

            bool isFamiliarized = document.Familiarizations.Any(f => f.Employee.Id == employee.Id);

            return View(new DetailsViewModel{Document = document, СurrentUserIsFamiliarized = isFamiliarized || document.SeeBefore < DateTime.Now });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Attachment(int docId, Guid attId)
        {
            DocumentStorage attachments = _storage[docId];

            FileItem attachment = attachments.GetFile(attId);

            if (attachment == null) return NotFound();

            _logger.LogInformation($"Запрос на предоставление вложения для документа [ID={docId}]: {attachment.FileName}");

            return File(attachment.Stream, attachment.MimeType, attachment.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> Acquainted(int id)
        {
            DocumentInfo document = _dataAccess.Document.FirstOrDefault(id);
            
            Familiarization familiarization = new Familiarization
            {
                Employee = await GetCurrentUserAsync(),
                DateTime = DateTime.Now
            };

            document.Familiarizations.Add(familiarization);
            
            _dataAccess.Document.Update(document);

            _dataAccess.Save();

            _logger.LogInformation($"С {document} ознакомился [{familiarization.Employee}] ");

            return RedirectToAction(nameof(List));
        }

        private Task<Employee> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
