using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KwestKarz.Entities;
using KwestKarz.Services.CSV;

namespace KwestKarz.Services.CSV
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

            // If a map exists for the type, register it
            if (typeof(T) == typeof(TripEarnings))
            {
                csv.Context.RegisterClassMap<TripEarningsMap>();
            }

            return csv.GetRecords<T>().ToList();
        }
    }
}
