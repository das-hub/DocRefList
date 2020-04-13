using System.Threading;
using System.Threading.Tasks;

namespace DocRefList.StartupTasks
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
