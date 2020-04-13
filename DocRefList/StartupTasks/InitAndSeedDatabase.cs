using System.Threading;
using System.Threading.Tasks;
using DocRefList.Data;
using DocRefList.Models.Entities;
using DocRefList.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DocRefList.StartupTasks
{
    public class InitAndSeedDatabase : IStartupTask
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly InitDBSetting _setting;

        public InitAndSeedDatabase(IOptions<InitDBSetting> options, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _setting = options.Value;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _db.Database.Migrate();

                if (await _roleManager.FindByNameAsync(RoleNames.ADMIN) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(RoleNames.ADMIN));
                }
                if (await _roleManager.FindByNameAsync(RoleNames.USER) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(RoleNames.USER));
                }
                if (await _userManager.FindByNameAsync(_setting.Email) == null)
                {
                    Employee admin = new Employee { Email = _setting.Email, UserName = _setting.Email, FullName = "Администратор" };
                    
                    IdentityResult result = await _userManager.CreateAsync(admin, _setting.Password);
                    
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, RoleNames.ADMIN);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
