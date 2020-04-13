using System;
using System.Threading.Tasks;
using DocRefList.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DocRefList.Extensions
{
    public static class EmployeeUserManagerExtensions
    {
        public static async Task LockAsync(this UserManager<Employee> userManager, Employee employee)
        {
            await userManager.SetLockoutEnabledAsync(employee, true);
            await userManager.SetLockoutEndDateAsync(employee, DateTimeOffset.MaxValue);
        }
        
        public static async Task UnLockAsync(this UserManager<Employee> userManager, Employee employee)
        {
            await userManager.SetLockoutEnabledAsync(employee, false);
            await userManager.SetLockoutEndDateAsync(employee, DateTimeOffset.MinValue);
        }
    }
}
