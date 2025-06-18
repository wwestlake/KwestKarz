using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace KwestKarz.Services.CSV
{
    public class UtcDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (DateTime.TryParse(text, out var dt))
            {
                return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
