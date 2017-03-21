using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    class ExcelExport
    {
        public void Run()
        {
            XSSFWorkbook book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet("sheet1");
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
            IRow row = sheet.CreateRow(0);
            row.Height = 580;
            row.CreateCell(0).SetCellValue("XXXXXX标题");

            string fileName = "test.xlsx";
            using (FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite)) 
            {
                book.Write(fs);
            }
            
            
        }
    }
}
