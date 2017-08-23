//using System;
//using System.Diagnostics;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using System.Data.Common;

//namespace BaseCSharp.CodeCollection
//{
//    /// <summary>
//    /// ����: ���룬���£�ɾ�����󣬲��Ҹ��µ����ݿ�   
//    /// </summary>
//    [CLSCompliant(false)]
//    public class ObjectOperable : IObjectOperable
//    {
//        #region ���������

//        /// <summary>
//        /// ���캯��    
//        /// </summary>
//        /// <param name="db">���ݿ����</param>
//        /// <param name="table">˵�������</param>
//        public ObjectOperable(IDatabase db, IExplanationTable table)
//        {
//            Debug.Assert(db!=null, "���ݿ����Ϊ�գ�");
//            Debug.Assert(table!=null, "˵�������Ϊ�գ�");
//            m_db = db;
//            m_table = table;
//            m_transaction = null;
//        }

//        #endregion // ���������

//        #region ���з���

//        /// <summary>
//        /// ����
//        /// </summary>
//        /// <param name="o">����</param>
//        /// <returns>�Ƿ������ɹ�</returns>
//        public bool Insert(object o)
//        {
//            if (!(o is DataRow))
//            {
//                return false;
//            }
//            DbCommand command = m_db.DbProviderFactory.CreateCommand();
//            try
//            {
//                DataRow row = o as DataRow;
//                StringBuilder keyGroup = new StringBuilder();
//                StringBuilder valueGroup = new StringBuilder();
//                foreach (DataColumn column in row.Table.Columns)
//                {
//                    //ֻ���ֶβ����б���
//                    if (column.ReadOnly == true)
//                    {
//                        continue;
//                    }
//                    keyGroup.Append(column.ColumnName);
//                    keyGroup.Append(",");
//                    valueGroup.Append(":" + column.ColumnName);
//                    valueGroup.Append(",");
//                }
//                string cmd = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", m_table.Name.Trim(),
//                    keyGroup.ToString().TrimEnd(new char[] { ' ', ',' }),
//                    valueGroup.ToString().TrimEnd(new char[] { ' ', ',' }));
//                //Ĭ�����ʱʱ��Ϊ3���� �̽��� ����� 2013-03-29
//                command.CommandTimeout = 180;
//                command.CommandType = CommandType.Text;
//                command.CommandText = cmd;
//                command.Parameters.Clear();
//                foreach (DataColumn column in row.Table.Columns)
//                {
//                    DbParameter param = m_db.DbProviderFactory.CreateParameter();
//                    param.ParameterName = ":" + column.ColumnName;
//                    // ������, 2013-04-10 11:08:23, �޸ģ���Ӱٷ����Ĵ���
//                    if (m_table.Contains(column.ColumnName) && m_table[column.ColumnName].FieldType != XFieldType.String)
//                    {
//                        switch (m_table[column.ColumnName].FieldType)
//                        {
//                            case XFieldType.Bool:
//                                if (row[column.ColumnName].ToString() == "��")
//                                {
//                                    param.Value = "0";
//                                }
//                                else if (row[column.ColumnName].ToString() == "��")
//                                {
//                                    param.Value = "1";
//                                }
//                                else
//                                {
//                                    param.Value = row[column.ColumnName];
//                                }
//                                break;
//                            case XFieldType.Double:
//                                double dv = 0;
//                                double.TryParse(row[column.ColumnName].ToString(), out dv);
//                                param.Value = dv;
//                                break;
//                            case XFieldType.Float:
//                                float fv = 0;
//                                float.TryParse(row[column.ColumnName].ToString(), out fv);
//                                param.Value = fv;
//                                break;
//                            case XFieldType.Integer:
//                                int iv = 0;
//                                int.TryParse(row[column.ColumnName].ToString(), out iv);
//                                param.Value = iv;
//                                break;
//                            case XFieldType.Long:
//                                long lv = 0;
//                                long.TryParse(row[column.ColumnName].ToString(), out lv);
//                                param.Value = lv;
//                                break;
//                            default:
//                                param.Value = row[column.ColumnName];
//                                break;

//                        }
//                        if (m_table.Contains(column.ColumnName) && m_table[column.ColumnName].DisplayStyle == XDisplayStyle.Percent)
//                        {
//                            param.Value = double.Parse(param.Value.ToString()) / 100;
//                        }
//                    }
//                    else
//                    {
//                        if (m_table[column.ColumnName].DisplayStyle == XDisplayStyle.DropDownList
//                            && string.IsNullOrEmpty(m_table[column.ColumnName].ForeignKey)
//                            && m_table[column.ColumnName].DataEnums != null
//                            && m_table[column.ColumnName].DataEnums.Count >0
//                            && m_table[column.ColumnName].DataEnums.ContainsKey(row[column.ColumnName].ToString())
//                            )
//                        {
//                            param.Value = m_table[column.ColumnName].DataEnums[row[column.ColumnName].ToString()];
//                        }
//                        else
//                        {
//                            param.Value = row[column.ColumnName];
//                        }
//                    }
//                    command.Parameters.Add(param);
//                }
//                command.Connection = (m_transaction == null || m_transaction.Connection == null) ?
//                    m_db.CreateConnection() : m_transaction.Connection;
//                if (command.Connection.State != ConnectionState.Open)
//                {
//                    command.Connection.Open();
//                }
//                command.ExecuteNonQuery();
//                if (m_table.Contains("f_wysykey") && row.Table.Columns.Contains("f_wysykey"))
//                {
//                    string[] keys = m_table.GetPrimaryKeys;
//                    List<string> list = new List<string>(keys);
//                    keyGroup.Clear();
//                    foreach (DataColumn column in row.Table.Columns)
//                    {
//                        if (list.Contains(column.ColumnName) && !column.ReadOnly)
//                        {
//                            keyGroup.Append(column.ColumnName + " = ((E'" + row[column.ColumnName].ToString() + "'))");
//                            keyGroup.Append("  AND ");
//                            continue;
//                        }
//                    }
//                    string getSql = string.Format("SELECT f_wysykey FROM {0} where {1}",
//                        m_table.Name.Trim(), keyGroup.Remove(keyGroup.Length - 4, 4));
//                    DataTable dt = m_db.QueryTable(getSql);
//                    row.Table.Columns["f_wysykey"].ReadOnly = false;
//                    // ������, 2012-12-6 14:54:49, �޸ģ�û������ʱ������
//                    if (dt.Rows.Count != 0)
//                    {
//                        row["f_wysykey"] = dt.Rows[0]["f_wysykey"].ToString();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                if (ex.Message.IndexOf("23505") >= 0)
//                {
//                    throw new Exception("�˼�¼�Ѵ��ڣ�");
//                }
//                else
//                {
//                    throw ex;
//                }
//            }
//            finally
//            {
//                if (command.Connection.State != ConnectionState.Closed)
//                {
//                    command.Connection.Close();
//                }
//            }
//            return true;
//        }

//        /// <summary>
//        /// ����
//        /// </summary>
//        /// <param name="o">����</param>
//        /// <returns>�Ƿ���³ɹ�</returns>
//        public bool Update(object o)
//        {
//            if (!(o is DataRow))
//            {
//                return false;
//            }
//            DbCommand command = m_db.DbProviderFactory.CreateCommand();
//            try
//            {
//                DataRow row = o as DataRow;
//                string[] keys = m_table.GetPrimaryKeys;
//                List<string> list = new List<string>(keys);
//                StringBuilder keyGroup = new StringBuilder();
//                StringBuilder fieldGroup = new StringBuilder();
//                foreach (DataColumn column in row.Table.Columns)
//                {

//                    if (list.Contains(column.ColumnName) && (this.m_table[column.ColumnName].Order < 0))
//                    {
//                        keyGroup.Append(column.ColumnName);
//                        keyGroup.Append(" =:");
//                        keyGroup.Append(column.ColumnName);
//                        keyGroup.Append(" AND ");
//                    }
//                    else if (this.m_table[column.ColumnName].Order >= 0)
//                    {
//                        fieldGroup.Append(column.ColumnName);
//                        fieldGroup.Append(" =:");
//                        fieldGroup.Append(column.ColumnName);
//                        fieldGroup.Append(", ");
//                    }
//                }
//                string conditions = keyGroup.ToString().TrimEnd();
//                conditions = conditions.Substring(0, conditions.Length - 4);
//                string cmd = string.Format("UPDATE {0} SET {1} {2}", m_table.Name.Trim(),
//                    fieldGroup.ToString().TrimEnd(new char[] { ' ', ',' }),
//                    (conditions != null ? "WHERE " + conditions : string.Empty));
//                //Ĭ�����ʱʱ��Ϊ3���� �̽��� ����� 2013-03-29
//                command.CommandTimeout = 180;
//                command.CommandType = CommandType.Text;
//                command.CommandText = cmd;
//                command.Parameters.Clear();
//                foreach (DataColumn column in row.Table.Columns)
//                {
//                    DbParameter param = m_db.DbProviderFactory.CreateParameter();
//                    param.ParameterName = ":" + column.ColumnName;
//                    //if (m_table.Contains(column.ColumnName) && m_table[column.ColumnName].Unit.IndexOf("%") >= 0)
//                    //{
//                    //    param.Value = double.Parse(row[column.ColumnName].ToString()) / 100;
//                    //}
//                    //else
//                    {
//                        param.Value = (row[column.ColumnName] == null ? row[column.ColumnName] : row[column.ColumnName].ToString().Trim());
//                    }
//                    command.Parameters.Add(param);
//                }
//                command.Connection = (m_transaction == null || m_transaction.Connection == null) ?
//                    m_db.CreateConnection() : m_transaction.Connection;
//                if (command.Connection.State != ConnectionState.Open)
//                {
//                    command.Connection.Open();
//                }
//                command.ExecuteNonQuery();
               
//            }
//            catch (Exception ex)
//            {
//                if (ex.Message.IndexOf("23505") >= 0)
//                {
//                    // Start Bug No.[Z34#-b-403]  (��� 2013/5/24 16:17:20)
//                    // ��������:  �ظ������޷���������¼�¼����ʾ��Ϣ��һ������
//                    //            
//                    // �޸�˵��:  
//                    //            
//                    // ======================================================= 


//                    //throw new Exception("ϵͳ������ͬ�ļ�¼");
//                    throw new Exception("�˼�¼�Ѵ��ڣ�");

//                    // End Bug No.[Z34#-b-403] (��� 2013/5/24 16:17:23)
//                }
//                else
//                {
//                    throw ex;
//                }
//            }
//            finally
//            {
//                if (command.Connection.State != ConnectionState.Closed)
//                {
//                    command.Connection.Close();
//                }
//            }
//            return true;
//        }

//        /// <summary>
//        /// ɾ��
//        /// </summary>
//        /// <param name="o">��ɾ������</param>
//        /// <returns>ɾ���Ƿ�ɹ�</returns>
//        public bool Delete(object o)
//        {
//            if (!(o is DataRow))
//            {
//                return false;
//            }
//            DbCommand command = m_db.DbProviderFactory.CreateCommand();
//            try
//            {
//                DataRow row = o as DataRow;
//                string[] keys = m_table.GetPrimaryKeys;
//                StringBuilder builder = new StringBuilder();
//                foreach (string key in keys)
//                {
//                    builder.Append(key);
//                    builder.Append(" = ");
//                    builder.Append(":" + key);
//                    builder.Append(" AND ");
//                }
//                string conditions = builder.ToString().TrimEnd();
//                conditions = conditions.Substring(0, conditions.Length - 4);
//                string cmd = string.Format("DELETE FROM {0} {1}", m_table.Name,
//                (keys.Length > 0 ? "WHERE " + conditions : string.Empty));
                
//                //Ĭ�����ʱʱ��Ϊ3���� �̽��� ����� 2013-03-29
//                command.CommandTimeout = 180;
//                command.CommandType = CommandType.Text;
//                command.CommandText = cmd;
//                command.Parameters.Clear();
//                foreach (DataColumn column in row.Table.Columns)
//                {
//                    DbParameter param = m_db.DbProviderFactory.CreateParameter();
//                    param.ParameterName = ":" + column.ColumnName;
//                    param.Value = row[column.ColumnName, DataRowVersion.Original];
//                    command.Parameters.Add(param);
//                }
//                command.Connection = (m_transaction == null || m_transaction.Connection == null) ?
//                    m_db.CreateConnection() : m_transaction.Connection;
//                if (command.Connection.State != ConnectionState.Open)
//                {
//                    command.Connection.Open();
//                }
//                command.ExecuteNonQuery();
              
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                if (command.Connection.State != ConnectionState.Closed)
//                {
//                    command.Connection.Close();
//                }
//            }
//            return true;
//        }

//        #endregion // ���з���

//        #region ��������
//        #endregion // ��������

//        #region ˽�з���
//        #endregion // ˽�з���

//        #region ���Լ���˽�б���

//        /// <summary>
//        /// ���ݿ����
//        /// </summary>
//        private IDatabase m_db;

//        /// <summary>
//        /// ˵�������
//        /// </summary>
//        private IExplanationTable m_table;

//        /// <summary>
//        /// �������
//        /// </summary>
//        private DbTransaction m_transaction;

//        /// <summary>
//        /// ��������
//        /// </summary>
//        public DbTransaction Transaction
//        {
//            get
//            {
//                return m_transaction;
//            }
//            set
//            {
//                if (m_transaction == value)
//                {
//                    return;
//                }
//                m_transaction = value;
//            }
//        }

//        #endregion // ���Լ���˽�б���

//    }
//}