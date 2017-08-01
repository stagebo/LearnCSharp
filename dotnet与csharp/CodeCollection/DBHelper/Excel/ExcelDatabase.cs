#region CLR�汾 4.0.30319.269
// Excel ��
// ʱ�䣺6/29/2012 1:30:12 PM
// ���ƣ�Excel���ݿ����
// ��٣�
//
// �����ˣ�������
// ��Ȩ��2012 ��������ʵ�����¼����ɷ����޹�˾ ��Ȩ����
// ��ע��
// ========================================================
//  ����		�޸���		����
#endregion
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace TDQS.DBHelper
{
    /// <summary>
    /// Excel���ݿ����
    /// </summary>
    [CLSCompliant(false)]
    public class ExcelDatabase : Database
    {
        #region ���������

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="FilePath">�����ļ�λ��</param>
        public ExcelDatabase(string FilePath)
        {
            Debug.Assert(!string.IsNullOrEmpty(FilePath), "���ݿ�·�� " + FilePath + " �����ڣ�");
            m_connString = "Provider=Microsoft.Ace.OleDb.12.0; data source=" + FilePath + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
            m_olecon = new OleDbConnection(m_connString);
        }
        #endregion // ���������

        #region ���з���

        /// <summary>
        /// ��ȡ���ݱ�
        /// </summary>
        /// <param name="tableName">������</param>
        /// <returns>���ݱ�</returns>
        public DataTable GetDataByTableName(string tableName)
        {
            DataSet myds = new DataSet();
            try
            {
                if (m_olecon.State == ConnectionState.Closed)
                {
                    m_olecon.Open();
                }
                //ץ����Sheet1
                System.Data.DataTable dt = m_olecon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (dt.Rows[0]["TABLE_Name"].ToString().IndexOf("$") < 0)
                {
                    dt.Rows[0]["TABLE_Name"] += "$";
                }
                string dtname = "";
                foreach(DataRow row in dt.Rows)
                {
                    string tempTableName = row["TABLE_Name"].ToString();
                    if (tempTableName.IndexOf(tableName) < 0)
                    {
                        continue;
                    }
                    dtname = tempTableName;
                    break;
                }
                
                string strSelect = string.Format("Select * From [{0}] ", dtname);
                OleDbDataAdapter da = new OleDbDataAdapter(strSelect, m_olecon);

                da.Fill(myds, dtname);
                da.Dispose();
                m_olecon.Close();
                m_olecon.Dispose();
                m_olecon = null;
                return myds.Tables[dtname];
            }
            catch(Exception ex)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
#endif

                throw new Exception("��ȡ���ݱ����");
            }
            finally
            {
                if (m_olecon != null)
                {
                    m_olecon.Close();
                    m_olecon.Dispose();
                    m_olecon = null;
                }
            }
        }

        #endregion // ���з���

        #region ��������
        #endregion // ��������

        #region ˽�з���
        #endregion // ˽�з���

        #region ���Լ���˽�б���

        /// <summary>
        /// ����Դ���Ӷ���
        /// </summary>
        private OleDbConnection m_olecon;

        #endregion // ���Լ���˽�б���

    }
}