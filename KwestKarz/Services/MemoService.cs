using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Services
{
    public class MemoService : IMemoService
    {
        private readonly KwestKarzDbContext _db;

        public MemoService(KwestKarzDbContext db)
        {
            _db = db;
        }

        public async Task<Memo> CreateMemoAsync(Memo memo)
        {
            _db.Memos.Add(memo);
            await _db.SaveChangesAsync();
            return memo;
        }

        public async Task<List<Memo>> GetUpcomingAsync()
        {
            var now = DateTime.UtcNow;
            return await _db.Memos
                .Where(m => m.Timestamp != null && m.Timestamp > now)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}
