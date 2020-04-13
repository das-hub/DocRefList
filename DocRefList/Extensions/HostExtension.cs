using System.Threading;
using System.Threading.Tasks;
using DocRefList.StartupTasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DocRefList.Extensions
{
    public static class HostExtension
    {
        public static async Task RunWithTasksAsync(this IHost host, CancellationToken cancellationToken = default)
        {
            using (var scope = host.Services.CreateScope())
            {
                var startupTasks = scope.ServiceProvider.GetServices<IStartupTask>();

                foreach (var startupTask in startupTasks)
                {
                    await startupTask.ExecuteAsync(cancellationToken);
                }
            }
            
            await host.RunAsync(cancellationToken);
        }
    }
}
