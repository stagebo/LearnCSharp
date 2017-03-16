// PostgreSqlDatabase ��
// ʱ��: 2011-07-25 17:58:13
// ���ƣ�PostgreSqlDatabase 
// ��٣�postgresql���ݿ����������װ
//
// �����ˣ��̽���
// ��Ȩ��2011 ��������ʵ�����¼����ɷ����޹�˾ ��Ȩ����
// ��ע��
// ========================================================
//  ����		�޸���		����

using System;
using System.Diagnostics;
using System.Collections.Generic;
using Npgsql;
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace TDQS.DBHelper
{
    /// <summary>
    /// ����: PostgreSql���ݿ���
    /// ����: ��������PostgreSql���ݿ�
    /// </summary>
    [CLSCompliant(false)]
    public class PostgreSqlDatabase : Database
    {
        #region ���������

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="connctionString">���ݿ����Ӵ�</param>
        public PostgreSqlDatabase(String connctionString)
        {
            Debug.Assert(!string.IsNullOrEmpty(connctionString), "���ݿ������ַ���Ϊ�գ�");
            m_connString = connctionString;
            m_dbFactory = NpgsqlFactory.Instance;
            //�̽��� ��Ĭ�ϵ����ʱʱ�����Ϊ120�� 2013-03-20
            if (connctionString.IndexOf("COMMANDTIMEOUT") < 1)
            {
                if (connctionString.EndsWith(";"))
                {
                    connctionString += "COMMANDTIMEOUT=120";
                }
                else
                {
                    connctionString += ";COMMANDTIMEOUT=120";
                }


            }
            m_dbFactory.CreateConnectionStringBuilder().ConnectionString = m_connString;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="userId">�û���</param>
        /// <param name="password">����</param>
        /// <param name="hostName">���������ƻ�IP��ַ</param>
        /// <param name="database">���ݿ�����</param>
        /// <param name="port">���ݿ�������˿�</param>
        public PostgreSqlDatabase(string userId, string password, string hostName, string database, string port)
        {
            Debug.Assert(!string.IsNullOrEmpty(userId), "�û���Ϊ�գ�");
            Debug.Assert(!string.IsNullOrEmpty(password), "����Ϊ�գ�");
            Debug.Assert(!string.IsNullOrEmpty(hostName), "���������ƻ�IP��ַΪ�գ�");
            Debug.Assert(!string.IsNullOrEmpty(database), "���ݿ�����Ϊ�գ�");
            Debug.Assert(!string.IsNullOrEmpty(port), "���ݿ�������˿�Ϊ�գ�");

            m_dbFactory = NpgsqlFactory.Instance;
            DbConnectionStringBuilder dbStringBuilder = m_dbFactory.CreateConnectionStringBuilder();
            dbStringBuilder.ConnectionString = "Server=" + hostName;
            dbStringBuilder.Add("Port", port);
            dbStringBuilder.Add("User Id", userId);
            dbStringBuilder.Add("Password", password);
            dbStringBuilder.Add("Database", database);
            //�̽��� ��Ĭ�ϵ����ʱʱ�����Ϊ600�� 2013-03-20
            dbStringBuilder.Add("COMMANDTIMEOUT", 600);

            m_connString = dbStringBuilder.ToString();
        }

        #endregion // ���������

        #region ���з���

        #endregion // ���з���

        #region ��������

        /// <summary>
        /// ������͵�����������
        /// </summary>
        /// <param name="dc">������</param>
        /// <returns>����������</returns>
        protected override string GetDataTypeString(DataColumn dc)
        {
            Debug.Assert(dc != null, "�����Ϊ�գ�");
            // Ĭ���ַ���������
            String defStringType = "varchar(50)";
            // Ĭ����ֵ��������
            String defnumicType = "float8";
            switch (dc.DataType.ToString())
            {
                case "System.Boolean":
                    return "bool";
                //case "System.Byte":
                //    return "bool";
                case "System.Char":
                    return defStringType;
                case "System.DateTime":
                    return "timestamp with time zone";
                case "System.Decimal":
                    return defnumicType;
                case "System.Double":
                    return defnumicType;
                case "System.Int16":
                    return "smallint";
                case "System.Int32":
                    return "int";
                case "System.Int64":
                    return "bigint";
                case "System.SByte":
                    return defStringType;
                case "System.Single":
                    return defnumicType;
                case "System.String":
                    if (dc.MaxLength <= 10485760)
                    {
                        return "varchar(" + dc.MaxLength + ")";
                    }
                    else
                    {
                        return "text";
                    }
                case "System.TimeSpan":
                    return "timestamp with time zone";
                case "System.UInt16":
                    return defnumicType;
                case "System.UInt32":
                    return defnumicType;
                case "System.UInt64":
                    return defnumicType;
                default:
                    return defStringType;
            }
        }
        #endregion // ��������

        #region ˽�з���
        #endregion // ˽�з���

        #region ���Լ���˽�б���
        #endregion // ���Լ���˽�б���

    }
}