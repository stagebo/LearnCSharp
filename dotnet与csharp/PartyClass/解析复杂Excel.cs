using NPOI.HSSF.UserModel;
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
            string fileName = "C:\\Users\\Administrator\\Desktop\\Excel\\Resouse.xlsx";
            string target = "C:\\Users\\Administrator\\Desktop\\Excel\\Target.xlsx";
            FileStream fs = null;
            FileStream fsTarget = null;
            try
            {
                fsTarget = new FileStream(target, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                return;
            }
            IWorkbook book = new XSSFWorkbook(fs);
            IWorkbook book1 = AnalyseExcel(book);

            book1.Write(fsTarget);
            fsTarget.Close();
            fs.Close();

        }
        /// <summary>
        /// 复制电子表格，达到拆分单元格的目的
        /// </summary>
        /// <returns></returns>
        public IWorkbook AnalyseExcel(IWorkbook book)
        {
            /*用于存复制之后的电子表格*/
            IWorkbook result = null;
            /*判断传入的格式，返回同类的格式*/
            if (book == null)
            {
                return null;
            }
            else if (book is HSSFWorkbook)
            {
                result = new HSSFWorkbook();//.xls
            }
            else if (book is XSSFWorkbook)
            {
                result = new XSSFWorkbook();//.xlsx
            }
            else//其他文件类型，不支持
            {
                return null;
            }
            for (int index = 0; index < book.NumberOfSheets;index++ )//遍历所有sheet
            {
                ISheet sheet = book.GetSheetAt(index);
                ISheet sheet1 = result.CreateSheet(sheet.SheetName);
                int rows = sheet.PhysicalNumberOfRows;
                /*先复制所有数据*/
                for (int j = 0; j < rows; j++)
                {
                    IRow row = sheet.GetRow(j);
                    IRow row1 = sheet1.CreateRow(j);
                    List<ICell> cells = row.Cells;
                    for (int k = 0; k < cells.Count; k++)
                    {
                        row1.CreateCell(k).SetCellValue(cells[k].ToString());
                    }
                }

                /*拆分已合并单元格，并给余下单元格赋值*/
                for (int j = 0; j < sheet.NumMergedRegions; j++)
                {
                    var cellRange = sheet.GetMergedRegion(j);//获取第i个合并单元格
                    int rowStart = cellRange.FirstRow;//获取该合并单元格起始行
                    int colStart = cellRange.FirstColumn;//获取该合并单元格起始列
                    int rowEnd = cellRange.LastRow;//获取该合并单元格终止行
                    int colEnd = cellRange.LastColumn;//获取该合并单元格终止列
                    string data = sheet.GetRow(rowStart).GetCell(colStart).ToString();//获取该合并单元格值
                    for (int m = rowStart; m <= rowEnd; m++)//遍历该合并单元格所包含的所有单元格
                    {
                        IRow row = null;
                        for (int n = colStart; n <= colEnd; n++)
                        {
                            try
                            {
                                sheet1.GetRow(m).GetCell(n).SetCellValue(data);//中间部分可能存在空行，如果是空行则捕获异常，创建该行即可
                            }
                            catch (Exception e)
                            {
                                if (row == null)
                                {
                                    row = sheet1.CreateRow(m);
                                }
                                sheet1.GetRow(m).GetCell(n).SetCellValue(data);//创建行并设单元格的值
                            }

                        }
                    }
                }
            }
            return result;
        }
    }
}
