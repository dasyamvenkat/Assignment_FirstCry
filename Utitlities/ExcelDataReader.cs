using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.Utitlities
{
    public class ExcelDataReader
    {
        public static Dictionary<string, string> readXLS(string FilePath)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            FileInfo existingFile = new FileInfo(FilePath);
            //for license issue resolution
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the required worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets["TestExcelData"];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                //moving excel data into dictonary
                for (int row = 1; row <= rowCount; row++)
                {
                    string key = worksheet.Cells[row, 1].Value.ToString();
                    dict.Add(key, "");
                    for (int col = 2; col <= colCount; col++)
                    {
                        string val = worksheet.Cells[row, col].Value.ToString();
                        if (dict.ContainsKey(key))
                        {
                            dict[key] = val;
                        }

                    }
                }

            }
            return dict;
        }
    }
}
