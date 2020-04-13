using System.Threading.Tasks;
using AutoMapper;
using DocRefList.Extensions;
using DocRefList.Models.AdminViewModels;
using DocRefList.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Filters.Controllers
{
    [Authorize(Roles = RoleNames.ADMIN)]
    public class AdminController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly IPasswordHasher<Employee> _passwordHasher;
        //private IPasswordValidator<AppUser> passwordValidator;
        //private IUserValidator<AppUser> userValidator;

        public AdminController(UserManager<Employee> userManager, IMapper mapper, ILogger<AdminController> logger, IPasswordHasher<Employee> passwordHasher)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            //passwordHasher = passwordHash;
            //passwordValidator = passwordVal;
            //userValidator = userValid;
        }

        public IActionResult List(int page = 1)
        {
            ListViewModel model = new ListViewModel
            {
                Users = _userManager.GetUsersPaging("List", page, 6)
            };

            return View(model);
        }

        public ViewResult Create() => View(new RegisterViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _mapper.Map<Employee>(model);

                IdentityResult result = await _userManager.CreateAsync(employee, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(employee, RoleNames.USER);

                    _logger.LogInformation($"Создан {employee}");
                    
                    return RedirectToAction("List");
                }
                else
                {
                    AddErrors(result);
                }
            }
            
            return View(model);
        }

        public async Task<IActionResult> Update(string id)
        {
            Employee employee = await _userManager.FindByIdAsync(id);
            
            if (employee != null)
            {
                UpdateViewModel model = _mapper.Map<UpdateViewModel>(employee);
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _userManager.FindByIdAsync(model.Id);

                if (employee != null)
                {
                    _mapper.Map(model, employee);

                    IdentityResult result = await _userManager.UpdateAsync(employee);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Внесены изменения в {employee}");
                        return RedirectToAction("List");
                    }
                    else
                        AddErrors(result);
                }
                else
                    return NotFound();
            }
           
            return View(model);
        }

        public async Task<IActionResult> LockUnlock(string id)
        {
            Employee employee = await _userManager.FindByIdAsync(id);

            if (await _userManager.IsLockedOutAsync(employee))
            {
                await _userManager.UnLockAsync(employee);
                _logger.LogInformation($"{employee} разблокирован");
            }
            else
            {
                await _userManager.LockAsync(employee);
                _logger.LogInformation($"{employee} заблокирован");
            }

            return RedirectToAction("List");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            Employee employee = await _userManager.FindByIdAsync(id);

            string token = await _userManager.GeneratePasswordResetTokenAsync(employee);

            _logger.LogInformation($"Поступил запрос на изменение пароля для {employee}");

            return View(new ChangePasswordViewModel{EmployeeId = id, Token = token});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            Employee employee = await _userManager.FindByIdAsync(model.EmployeeId);
            
            if (employee != null)
            {
                var result = await _userManager.ResetPasswordAsync(employee, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation(3, $"Для {employee} успешно изменен пароль");
                    return RedirectToAction(nameof(List));
                }
                
                AddErrors(result);
                
                return View(model);
            }
            return NotFound();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}