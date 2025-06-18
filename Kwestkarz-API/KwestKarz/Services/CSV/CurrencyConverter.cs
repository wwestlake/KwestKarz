using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace KwestKarz.Services.CSV
{
    public class CurrencyConverter : DecimalConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
                return base.ConvertFromString(text, row, memberMapData);

            // Strip dollar signs and commas
            text = text.Replace("$", "").Replace(",", "").Trim();

            if (decimal.TryParse(text, NumberStyles.Number | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var value))
                return value;

            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
