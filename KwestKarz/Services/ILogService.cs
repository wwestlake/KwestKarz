using KwestKarz.Entities;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KwestKarz.Services
{
    public interface ILogService
    {
        Task LogAsync(
            TechLogLevel level,
            string message,
            string? detail = null,
            string? overrideSource = null,
            [CallerFilePath] string callerFile = ""
        );
    }
}
