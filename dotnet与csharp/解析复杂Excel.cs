using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet与csharp
{
    class 解析复杂Excel
    {
         //for (int ii = 0; ii < sheet.NumMergedRegions; ii++)
         //               {
         //                   var cellrange = sheet.GetMergedRegion(ii);
         //                   if (cell.ColumnIndex >= cellrange.FirstColumn && cell.ColumnIndex <= cellrange.LastColumn
         //                       && cell.RowIndex >= cellrange.FirstRow && cell.RowIndex <= cellrange.LastRow)
         //                   {
         //                       // 这里是cell所在的合并单元格，添加处理代码
         //                   }
         //               }
        public void Run()
        {
            string fileName = "../../复杂Excel解析.xlsx";
            string target = "C:\\Users\\1\\Desktop\\ExcelTest\\copy.xlsx";
            FileStream fs = null;
            FileStream fst = null;
            try
            {
                fst = new FileStream(target,System.IO.FileMode.Create,System.IO.FileAccess.Write);
                fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            }
            catch (Exception e)
            {
                return;
            }
            IWorkbook book = new XSSFWorkbook(fs);
            IWorkbook book1 = new XSSFWorkbook();
            ISheet sheet = book.GetSheetAt(0);
            ISheet sheet1 = book1.CreateSheet();
            /*遍历表，并复制数据*/
            int x = sheet.PhysicalNumberOfRows;
            for (int i = 0; i < x; i++)
            {
                IRow row = sheet.GetRow(i);
                IRow row1 = sheet1.CreateRow(i);
                List<ICell> cells = row.Cells;
                for (int j = 0; j < cells.Count; j++)
                {
                    row1.CreateCell(j).SetCellValue(cells[j].ToString());
                }
            }
            /*给合并单元格赋值*/
            for (int i = 0; i < sheet.NumMergedRegions; i++)
            {
                var cellRange = sheet.GetMergedRegion(i);
                int rowStart = cellRange.FirstRow;
                int colStart = cellRange.FirstColumn;
                int rowEnd = cellRange.LastRow;
                int colEnd = cellRange.LastColumn;
                string data = sheet.GetRow(rowStart).GetCell(colStart).ToString();
                for (int m = rowStart; m <= rowEnd; m++)
                {
                    IRow row = null;
                    for (int n = colStart; n <= colEnd; n++)
                    {
                        //空行待处理
                        try
                        {
                            sheet1.GetRow(m).GetCell(n).SetCellValue(data);
                        }
                        catch (Exception e)
                        {
                            if (row == null)
                            {
                                row = sheet1.CreateRow(m);
                            }
                            sheet1.GetRow(m).GetCell(n).SetCellValue(data);
                        }
                       
                    }
                }
            }
            book1.Write(fst);
            fst.Close();
            fs.Close();

        }
    }
}
