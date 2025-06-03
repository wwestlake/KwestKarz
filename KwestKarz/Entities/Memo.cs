using System;
using System.Collections.Generic;

namespace KwestKarz.Entities
{
    public class Memo
    {
        public int Id { get; set; }
        public MemoType Type { get; set; } = MemoType.Thought;
        public string Text { get; set; }
        public DateTime? Timestamp { get; set; }
        public List<string> Tags { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
