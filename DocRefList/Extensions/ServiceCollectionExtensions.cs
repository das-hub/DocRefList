using System.IO;
using DocRefList.Data;
using DocRefList.Data.Repository;
using DocRefList.Data.Repository.Abstraction;
using DocRefList.Models.Entities;
using DocRefList.StartupTasks;
using DocRefList.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DocRefList.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
            where T : class, IStartupTask
            => services.AddTransient<IStartupTask, T>();

        public static void AddDatabaseAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<Employee, IdentityRole>(opt => { opt.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IDataAccess, DataAccess>();
        }

        public static void AddFileStorage(this IServiceCollection services, string storage)
        {
            services.AddScoped(provider =>
            {
                IHostEnvironment env = provider.GetRequiredService<IHostEnvironment>();
                return new FileStorage(Path.Combine(env.ContentRootPath, storage));
            });
        }
    }
}
