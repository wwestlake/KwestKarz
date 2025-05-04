using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using KwestKarz.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KwestKarz.Services
{
    public class CSVParserService : ICSVParserService
    {
        public List<T> ParseCsv<T>(Stream csvStream) where T : class, new()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
                PrepareHeaderForMatch = args => args.Header
            };

            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }
}
