using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Ultils
{
    public static class CsvReaderExtensions
    {
        public static List<T> ReadCsvFile<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null // Bỏ qua dữ liệu xấu
            }))
            {
                return new List<T>(csv.GetRecords<T>());
            }
        }
    }
}
