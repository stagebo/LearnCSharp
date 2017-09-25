using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProgram
{ 
    /// <summary>
    /// sqllite操作类
    /// </summary>
    public class SqliteDatabase
    {
        #region 构造和析构

        /// <summary>
        /// 构造
        /// </summary>
        public SqliteDatabase(string dbname)
        {
            m_DbName = dbname;

        }

        #endregion // 构造和析构

        #region 公有方法

        #region  执行简单SQL语句
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>        
        public int ExecuteSql(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SQLiteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }



        /// <summary>
        /// 执行SQL语句，设置命令的执行等待时间
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="Times">命令的执行等待时间</param>
        /// <returns></returns>
        public int ExecuteSqlByTime(string SQLString, int Times)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {

                    try
                    {

                        connection.Open();

                        cmd.CommandTimeout = Times;

                        int rows = cmd.ExecuteNonQuery();

                        return rows;

                    }

                    catch (SQLiteException E)
                    {

                        connection.Close();

                        throw new Exception(E.Message);
                    }
                }
            }
        }



        /// <summary>

        /// 执行多条SQL语句，实现数据库事务。

        /// </summary>

        /// <param name="SQLStringList">多条SQL语句</param>        

        public void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectstring()))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                SQLiteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {

                    for (int n = 0; n < SQLStringList.Count; n++)
                    {

                        string strsql = SQLStringList[n].ToString();

                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();

                }

                catch (SQLiteException E)
                {

                    tx.Rollback();

                    throw new Exception(E.Message);
                }
            }
        }



        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>

        public int ExecuteSql(string SQLString, string content)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SQLiteException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }



        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public object ExecuteSqlGet(string SQLString, string content)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);

                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }

                catch (SQLiteException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }

            }

        }



        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@fs", DbType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SQLiteException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }



        /// <summary>        
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {

                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();

                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {

                            return obj;

                        }

                    }

                    catch (SQLiteException e)
                    {

                        connection.Close();

                        throw new Exception(e.Message);

                    }

                }

            }

        }



        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader(使用该方法切记要手工关闭SQLiteDataReader和连接)
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>

        public SQLiteDataReader ExecuteReader(string strSQL)
        {

            SQLiteConnection connection = new SQLiteConnection(GetConnectstring());

            SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);

            try
            {

                connection.Open();

                SQLiteDataReader myReader = cmd.ExecuteReader();

                return myReader;

            }

            catch (SQLiteException e)
            {

                throw new Exception(e.Message);

            }

            //finally //不能在此关闭，否则，返回的对象将无法使用

            //{

            //    cmd.Dispose();

            //    connection.Close();

            //}    

        }



        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataTable QueryTable(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {
                DataTable table = new DataTable();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(table);
                }
                catch (SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return table;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataSet Query(string SQLString, string TableName)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                DataSet ds = new DataSet();

                try
                {

                    connection.Open();

                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);

                    command.Fill(ds, TableName);

                }

                catch (SQLiteException ex)
                {

                    throw new Exception(ex.Message);

                }

                return ds;

            }

        }



        /// <summary>
        /// 执行查询语句，返回DataSet,设置命令的执行等待时间
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public DataSet Query(string SQLString, int Times)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                DataSet ds = new DataSet();

                try
                {

                    connection.Open();

                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);

                    command.SelectCommand.CommandTimeout = Times;

                    command.Fill(ds, "ds");

                }

                catch (SQLiteException ex)
                {

                    throw new Exception(ex.Message);

                }

                return ds;

            }

        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, params SQLiteParameter[] cmdParms)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                using (SQLiteCommand cmd = new SQLiteCommand())
                {

                    try
                    {

                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);

                        int rows = cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();

                        return rows;

                    }

                    catch (SQLiteException E)
                    {

                        throw new Exception(E.Message);

                    }

                }

            }

        }



        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>

        public void ExecuteSqlTran(Hashtable SQLStringList)
        {

            using (SQLiteConnection conn = new SQLiteConnection(GetConnectstring()))
            {

                conn.Open();

                using (SQLiteTransaction trans = conn.BeginTransaction())
                {

                    SQLiteCommand cmd = new SQLiteCommand();

                    try
                    {

                        //循环

                        foreach (DictionaryEntry myDE in SQLStringList)
                        {

                            string cmdText = myDE.Key.ToString();

                            SQLiteParameter[] cmdParms = (SQLiteParameter[])myDE.Value;

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            int val = cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();



                            trans.Commit();

                        }

                    }

                    catch
                    {

                        trans.Rollback();

                        throw;

                    }

                }

            }

        }



        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>

        public object GetSingle(string SQLString, params SQLiteParameter[] cmdParms)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                using (SQLiteCommand cmd = new SQLiteCommand())
                {

                    try
                    {

                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);

                        object obj = cmd.ExecuteScalar();

                        cmd.Parameters.Clear();

                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {

                            return null;

                        }

                        else
                        {

                            return obj;

                        }

                    }

                    catch (SQLiteException e)
                    {

                        throw new Exception(e.Message);

                    }

                }

            }

        }



        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader (使用该方法切记要手工关闭SQLiteDataReader和连接)
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        private SQLiteDataReader ExecuteReader(string SQLString, params SQLiteParameter[] cmdParms)
        {

            SQLiteConnection connection = new SQLiteConnection(GetConnectstring());

            SQLiteCommand cmd = new SQLiteCommand();

            try
            {

                PrepareCommand(cmd, connection, null, SQLString, cmdParms);

                SQLiteDataReader myReader = cmd.ExecuteReader();

                cmd.Parameters.Clear();

                return myReader;

            }

            catch (SQLiteException e)
            {

                throw new Exception(e.Message);

            }

            //finally //不能在此关闭，否则，返回的对象将无法使用

            //{

            //    cmd.Dispose();

            //    connection.Close();

            //}   



        }



        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, params SQLiteParameter[] cmdParms)
        {

            using (SQLiteConnection connection = new SQLiteConnection(GetConnectstring()))
            {

                SQLiteCommand cmd = new SQLiteCommand();

                PrepareCommand(cmd, connection, null, SQLString, cmdParms);

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {

                    DataSet ds = new DataSet();

                    try
                    {

                        da.Fill(ds, "ds");

                        cmd.Parameters.Clear();

                    }

                    catch (SQLiteException ex)
                    {

                        throw new Exception(ex.Message);

                    }

                    return ds;

                }

            }

        }


        /// <summary>
        /// 预处理命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        public void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn,
                         SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parameter in cmdParms)
                {

                    if ((parameter.Direction == ParameterDirection.InputOutput
                            || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }

            }

        }
        #endregion

        #region 其他
        /// <summary>
        /// 返回某个字段的最大ID
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetMaxID(string FieldName, string TableName)
        {

            string strsql = "select max(" + FieldName + ")+1 from " + TableName;

            object obj = GetSingle(strsql);

            if (obj == null)
            {

                return 1;

            }

            else
            {

                return int.Parse(obj.ToString());

            }

        }


        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public bool Exists(string strSql)
        {

            object obj = GetSingle(strSql);

            int cmdresult;

            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断结果是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public bool Exists(string strSql, params SQLiteParameter[] cmdParms)
        {

            object obj = GetSingle(strSql, cmdParms);

            int cmdresult;

            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {

                cmdresult = 0;

            }

            else
            {

                cmdresult = int.Parse(obj.ToString());

            }

            if (cmdresult == 0)
            {

                return false;

            }

            else
            {

                return true;

            }

        }

        #endregion

        #region 参数转换

        /// <summary>
        /// 放回一个SQLiteParameter
        /// </summary>
        /// <param name="name">参数名字</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns>SQLiteParameter的值</returns>

        public SQLiteParameter MakeSQLiteParameter(string name,

                   DbType type, int size, object value)
        {

            SQLiteParameter parm = new SQLiteParameter(name, type);

            parm.Value = value;

            return parm;

        }


        /// <summary>
        /// 生成参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        /// <returns>返回生成的sqllite的参数</returns>
        public SQLiteParameter MakeSQLiteParameter(string name, DbType type, object value)
        {

            SQLiteParameter parm = new SQLiteParameter(name, type);

            parm.Value = value;

            return parm;

        }

        #endregion // 公有方法

        #endregion

        #region 保护方法
        #endregion // 保护方法

        #region 私有方法

        /// <summary>
        ///获得连接串
        /// </summary>
        /// <returns></returns>
        private string GetConnectstring()
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.Version = 3;
            sb.DataSource = m_DbName;
            return sb.ToString();
            //return String.Format("Data Source={0};New=False;Version=3", m_DbName);
        }


        #endregion // 私有方法

        #region 属性及其私有变量

        string m_DbName;
        /// <summary>
        /// 
        /// </summary>
        public string DbName
        {
            get { return m_DbName; }
            set { m_DbName = value; }
        }

        #endregion // 属性及其私有变量


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
