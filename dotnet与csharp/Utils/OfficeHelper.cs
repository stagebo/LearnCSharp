using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections;

namespace BaseCSharp
{
    /// <summary>
    /// Excel处理类
    /// </summary>
    public class OfficeHelper
    {
        #region 构造和析构

        /// <summary>
        /// 构造
        /// </summary>
        public OfficeHelper()
        {
           
        }

        #endregion // 构造和析构

        #region 公有方法

        /// <summary>
        /// 从excle导入到数据集，excle中的工作表对应dataset中的table，工作表名和列名分别对应table中的表名和列名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataTable ExcelToDataSet(string path, string sheetName)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                //var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook xssfworkbook = WorkbookFactory.Create(fs, ImportOption.SheetContentOnly);
                //NPOI.XSSF.UserModel.XSSFWorkbook xssfworkbook = new NPOI.XSSF.UserModel.XSSFWorkbook(fs);
                ISheet sheet = xssfworkbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    return null;
                }
                List<MergeRegion> mergeRegions = GetMergeRegions(sheet);
                DataTable dt = new DataTable(sheet.SheetName);

                //添加列
                int columnCount = sheet.GetRow(0).LastCellNum;
                for (int i = 0; i < columnCount; i++)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                }

                int rowsCount = sheet.LastRowNum + 1;
                for (int i = 0; i < rowsCount; i++)
                {
                    DataRow dr = dt.NewRow();
                    int iemptyClmCount = 0;
                    int mergedClmCount = 0;
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < columnCount; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null && cell.CellType == CellType.Blank && cell.IsMergedCell)
                        {
                            mergedClmCount++;
                        }
                        object tempValue = GetValue(cell, mergeRegions);
                        dr[j] = tempValue;
                        if (tempValue == null || tempValue.ToString() == "")
                        {
                            iemptyClmCount++;
                        }
                    }
                    if (iemptyClmCount == columnCount || mergedClmCount == columnCount)
                    {
                        continue;
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        /// <summary>
        ///  datatable导出到excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="wb"></param>
        public void ImportToWorkbook(DataTable dt, ref IWorkbook wb)
        {
            string sheetName = dt.TableName ?? "Sheet1";
            //创建工作表
            ISheet sheet = wb.CreateSheet(sheetName);
            //添加标题
            IRow titleRow = sheet.CreateRow(0);
            SetRow(titleRow,
                GetCloumnNames(dt),
                GetCellStyle(sheet.Workbook, FontBoldWeight.Bold));

            //添加数据行
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow dataRow = sheet.CreateRow(i + 1);
                SetRow(
                    dataRow,
                    GetRowValues(dt.Rows[i]),
                    GetCellStyle(sheet.Workbook));
            }

            //设置表格自适应宽度--注释掉以防Excel凌乱
            //AutoSizeColumn(sheet);
        }

        #endregion // 公有方法

        #region 保护方法

        protected static List<MergeRegion> GetMergeRegions(ISheet sheet)
        {
            var result = new List<MergeRegion>();
            for (var i = 0; i < sheet.NumMergedRegions; i++)
            {
                var rgn = new MergeRegion();
                rgn.NPOIRegion = sheet.GetMergedRegion(i);
                var row = sheet.GetRow(rgn.NPOIRegion.FirstRow);
                rgn.LeftTopCell = row.GetCell(rgn.NPOIRegion.FirstColumn);
                result.Add(rgn);
            }
            return result;
        }

        protected static ICell GetValCelForMergeRegion(ICell cell, List<MergeRegion> mergeRegions)
        {
            foreach (var mergeRgn in mergeRegions)
            {
                if (mergeRgn.NPOIRegion.IsInRange(cell.RowIndex, cell.ColumnIndex))
                {
                    return mergeRgn.LeftTopCell;
                }
            }
            return cell;
        }

        protected class MergeRegion
        {
            public NPOI.SS.Util.CellRangeAddress NPOIRegion;
            public ICell LeftTopCell;
        }

        #endregion // 保护方法

        #region 私有方法

        private static object GetValue(ICell cell, List<MergeRegion> mergeRegions)
        {
            if (cell == null)
                return null;
            if (cell.IsMergedCell || cell.CellType == CellType.Blank)
            {
                cell = GetValCelForMergeRegion(cell, mergeRegions);
            }
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String: //STRING:   
                    return cell.StringCellValue;
                default:
                    cell.SetCellType(CellType.String);
                    return cell.StringCellValue;
            }
        }


      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        private byte[] ToByte(IWorkbook wb)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //XSSFWorkbook即读取.xlsx文件返回的MemoryStream是关闭
                //但是可以ToArray(),这是NPOI的bug
                wb.Write(ms);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IWorkbook CreateSheet(string path)
        {
            IWorkbook wb = new NPOI.HSSF.UserModel.HSSFWorkbook(); ;
            string extension = System.IO.Path.GetExtension(path).ToLower();
            if (extension == ".xls")
                wb = new NPOI.HSSF.UserModel.HSSFWorkbook();
            else if (extension == ".xlsx")
                wb = new NPOI.XSSF.UserModel.XSSFWorkbook();

            return wb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private int GetWidth(DataTable dt, int columnIndex)
        {
            IList<int> lengths = new List<int>();
            foreach (DataRow dr in dt.Rows)
                lengths.Add(Convert.ToString(dr[columnIndex]).Length * 256);
            return lengths.Max();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private IList<string> GetRowValues(DataRow dr)
        {
            List<string> rowValues = new List<string>();

            for (int i = 0; i < dr.Table.Columns.Count; i++)
                rowValues.Add(Convert.ToString(dr[i]));

            return rowValues;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private IList<string> GetCloumnNames(DataTable dt)
        {
            List<string> columnNames = new List<string>();

            foreach (DataColumn dc in dt.Columns)
                columnNames.Add(dc.ColumnName);

            return columnNames;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="values"></param>
        private void SetRow(IRow row, IList<string> values)
        {
            SetRow(row, values, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="values"></param>
        /// <param name="cellStyle"></param>
        private void SetRow(IRow row, IList<string> values, ICellStyle cellStyle)
        {
            for (int i = 0; i < values.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(values[i]);
                if (cellStyle != null)
                    cell.CellStyle = cellStyle;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        private ICellStyle GetCellStyle(IWorkbook wb)
        {
            return GetCellStyle(wb, FontBoldWeight.None);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="boldweight"></param>
        /// <returns></returns>
        private ICellStyle GetCellStyle(IWorkbook wb, FontBoldWeight boldweight)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();

            //字体样式
            IFont font = wb.CreateFont();
            font.FontHeightInPoints = 10;
            font.FontName = "微软雅黑";
            font.Color = (short)FontColor.Normal;
            font.Boldweight = (short)boldweight;

            cellStyle.SetFont(font);

            //对齐方式
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            //边框样式
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            //设置背景色
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;


            //是否自动换行
            cellStyle.WrapText = false;

            //缩进
            cellStyle.Indention = 0;

            return cellStyle;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        private void AutoSizeColumn(ISheet sheet)
        {
            //获取当前列的宽度，然后对比本列的长度，取最大值
            for (int columnNum = 0; columnNum <= sheet.PhysicalNumberOfRows; columnNum++)
                AutoSizeColumn(sheet, columnNum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="columnNum"></param>
        private void AutoSizeColumn(ISheet sheet, int columnNum)
        {
            int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
            for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
            {
                IRow currentRow = sheet.GetRow(rowNum) == null ?
                    sheet.CreateRow(rowNum) : sheet.GetRow(rowNum);
                if (currentRow.GetCell(columnNum) != null)
                {
                    ICell currentCell = currentRow.GetCell(columnNum);
                    int length = System.Text.Encoding.Default.GetBytes(currentCell.ToString()).Length;
                    if (columnWidth < length)
                        columnWidth = length;
                }
            }
            sheet.SetColumnWidth(columnNum, columnWidth * 256);
        }
        #endregion // 私有方法

        #region 属性及其私有变量

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
        public class EntityMappingAttribute : Attribute
        {
            public string Name { get; set; }
        }
        #endregion // 属性及其私有变量

    }

}