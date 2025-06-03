using System;
using System.Linq;

namespace KwestKarz.Services
{
    public interface ICSVParserService
    {
        List<T> ParseCsv<T>(Stream csvStream) where T : class, new();
    }
}
