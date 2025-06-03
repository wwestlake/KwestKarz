using System.Collections.Generic;
using System.Threading.Tasks;
using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public interface IMemoService
    {
        Task<Memo> CreateMemoAsync(Memo memo);
        Task<List<Memo>> GetUpcomingAsync();
    }
}
