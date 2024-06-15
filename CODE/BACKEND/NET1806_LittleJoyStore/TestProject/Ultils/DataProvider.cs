using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DemoTest;

namespace TestProject.Ultils
{
    public class DataProvider
    {
        public static IEnumerable<T> GetTestData<T>(string filePath)
        {
            return CsvReaderExtensions.ReadCsvFile<T>(filePath);
        }
    }
}
