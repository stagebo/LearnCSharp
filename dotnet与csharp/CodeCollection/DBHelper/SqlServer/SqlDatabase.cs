using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace BaseCSharp.CodeCollection.SqlServer
{
    /// <summary>
    /// SqlDatabase ��
    /// </summary>
    
    public class SqlDatabase : Database
    {
        #region ���������

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="connctionString">���ݿ������ַ���</param>
        public SqlDatabase(String connctionString)
        {
            Debug.Assert(!string.IsNullOrEmpty(connctionString), "���ݿ�·�� " + connctionString + " �����ڣ�");
            m_connString = connctionString;
            m_dbFactory = SqlClientFactory.Instance;
            m_dbFactory.CreateConnectionStringBuilder().ConnectionString = m_connString;
        }

        #endregion // ���������

        #region ���з���
        #endregion // ���з���

        #region ��������
        #endregion // ��������

        #region ˽�з���
        #endregion // ˽�з���

        #region ���Լ���˽�б���
        #endregion // ���Լ���˽�б���

    }
}