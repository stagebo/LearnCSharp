using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace dotnet与csharp
{
    class 读入Excel
    {
        static string filePath = "停电计划.xlsx";
        public void Run()
        {
            string fileName = "../../停电计划.xlsx";
            List<string> columnTitleNameList = new List<string>();
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            }
            catch
            {
                return;
            }
            IWorkbook workbook = new XSSFWorkbook(fs);
            /* 验证sheet表 */
            if (workbook.NumberOfSheets == 0)
            {
                return;
            }
            ISheet deviceSheet = workbook.GetSheet("Sheet1");
            if (deviceSheet == null)
            {
                return;
            }
            /* 验证总列数、标题行 */
            int deviceNumberOfRows = deviceSheet.PhysicalNumberOfRows;
            //IRow deviceTitleRow = deviceSheet.GetRow(0);
            //if (deviceTitleRow == null)
            //{
            //    return;
            //}
            //List<ICell> deviceTitleCellList = deviceTitleRow.Cells;
            //if (deviceTitleCellList.Count != columnTitleNameList.Count)
            //{
            //    return;
            //}

            ///* 验证列标题 */
            //for (int i = 0; i < deviceTitleCellList.Count; i++)
            //{
            //    ICell deviceTitleCell = deviceTitleCellList[i];
            //    string strTitleName = columnTitleNameList[i];
            //    if (!strTitleName.Equals(deviceTitleCell.ToString()))
            //    {
            //        return;
            //    }
            //}

            /* 读取数据内容前准备 */
            List<TDJH> tdjhList = new List<TDJH>();

            /* 读取数据内容行 */
            IRow row;
            for (int i = 1; i < deviceNumberOfRows; i++)
            {
                TDJH t = new TDJH();
                row = deviceSheet.GetRow(i);

                t.f_id = Guid.NewGuid().ToString();
                t.f_subgchid = Guid.NewGuid().ToString();
                t.f_mc = row.GetCell(0).StringCellValue;
                t.f_gznr = row.GetCell(1).StringCellValue;
                t.f_jhxz = row.GetCell(2).StringCellValue;
                t.f_gzlb = row.GetCell(3).StringCellValue;
                t.f_zy = row.GetCell(4).StringCellValue;
                t.f_gzbz = row.GetCell(5).StringCellValue;
                t.f_gzdd = row.GetCell(6).StringCellValue; ;
                // t.f_jhksrq = row.GetCell(0).StringCellValue;
                //  t.f_jhjsrq = row.GetCell(0).StringCellValue; 
                tdjhList.Add(t);

            }
            /* 关闭流，删除上传文件 */
            try
            {
                fs.Close();
                 File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return;
            }

            foreach (var tt in tdjhList)
            {
                Console.Write(tt.f_mc + "\t");
                Console.Write(tt.f_gznr + "\t");
                Console.Write(tt.f_jhxz + "\t");
                Console.Write(tt.f_gzlb + "\t");
                Console.Write(tt.f_zy + "\t");
                Console.Write(tt.f_gzdd + "\t");
                Console.WriteLine();
            }


        }

        public class TDJH
        {
            #region Model

            /// <summary>
            /// f_id
            /// </summary>
            public virtual string f_id
            {
                get;
                set;
            }

            /// <summary>
            /// f_subgchid
            /// </summary>
            public virtual string f_subgchid
            {
                get;
                set;
            }

            /// <summary>
            /// f_mc
            /// </summary>
            public virtual string f_mc
            {
                get;
                set;
            }

            /// <summary>
            /// f_gznr
            /// </summary>
            public virtual string f_gznr
            {
                get;
                set;
            }

            /// <summary>
            /// f_jhxz
            /// </summary>
            public virtual string f_jhxz
            {
                get;
                set;
            }

            /// <summary>
            /// f_gzlb
            /// </summary>
            public virtual string f_gzlb
            {
                get;
                set;
            }

            /// <summary>
            /// f_zy
            /// </summary>
            public virtual string f_zy
            {
                get;
                set;
            }

            /// <summary>
            /// f_gzbz
            /// </summary>
            public virtual string f_gzbz
            {
                get;
                set;
            }

            /// <summary>
            /// f_gzdd
            /// </summary>
            public virtual string f_gzdd
            {
                get;
                set;
            }

            /// <summary>
            /// f_jhksrq
            /// </summary>
            public virtual DateTime? f_jhksrq
            {
                get;
                set;
            }

            /// <summary>
            /// f_jhjsrq
            /// </summary>
            public virtual DateTime? f_jhjsrq
            {
                get;
                set;
            }

            /// <summary>
            /// f_gzfs
            /// </summary>
            public virtual string f_gzfs
            {
                get;
                set;
            }

            /// <summary>
            /// f_xsxl
            /// </summary>
            public virtual string f_xsxl
            {
                get;
                set;
            }

            /// <summary>
            /// f_djxl
            /// </summary>
            public virtual string f_djxl
            {
                get;
                set;
            }

            /// <summary>
            /// f_dydj
            /// </summary>
            public virtual string f_dydj
            {
                get;
                set;
            }

            /// <summary>
            /// f_zyzrdw
            /// </summary>
            public virtual string f_zyzrdw
            {
                get;
                set;
            }

            /// <summary>
            /// f_zydw
            /// </summary>
            public virtual string f_zydw
            {
                get;
                set;
            }

            /// <summary>
            /// f_fxdj
            /// </summary>
            public virtual string f_fxdj
            {
                get;
                set;
            }

            /// <summary>
            /// f_gkcj
            /// </summary>
            public virtual string f_gkcj
            {
                get;
                set;
            }

            /// <summary>
            /// f_gkfs
            /// </summary>
            public virtual string f_gkfs
            {
                get;
                set;
            }

            /// <summary>
            /// f_yscj
            /// </summary>
            public virtual string f_yscj
            {
                get;
                set;
            }

            /// <summary>
            /// f_sgfa
            /// </summary>
            public virtual int? f_sgfa
            {
                get;
                set;
            }

            /// <summary>
            /// f_zyzds
            /// </summary>
            public virtual int? f_zyzds
            {
                get;
                set;
            }

            /// <summary>
            /// f_gzp
            /// </summary>
            public virtual int? f_gzp
            {
                get;
                set;
            }

            /// <summary>
            /// f_czp
            /// </summary>
            public virtual int? f_czp
            {
                get;
                set;
            }

            /// <summary>
            /// f_jlbd
            /// </summary>
            public virtual int? f_jlbd
            {
                get;
                set;
            }

            /// <summary>
            /// f_bz
            /// </summary>
            public virtual string f_bz
            {
                get;
                set;
            }
            #endregion
        }

    }
}
