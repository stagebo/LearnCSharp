using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BaseCSharp
{
    /// <summary>
    /// 数据库工具类
    /// </summary>
    class DBUtils
    {
        #region 静态代码块
        static DBUtils()
        {
            m_tran = m_conn.BeginTransaction();
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 私有化构造函数，单例模式。
        /// </summary>
        private DBUtils() { }

        #endregion

        #region 公共方法
        /// <summary>
        /// 获得sql连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            return m_conn;
        }

        public static SqlTransaction GetTransaction()
        {
            return m_tran;
        }

        public static void DBDispose()
        {
            if (m_tran != null) m_tran.Dispose(); ;
            if (m_conn != null) m_conn.Close();
        }
        #endregion

        #region 私有方法

        #endregion

        #region    私有字段
        /// <summary>
        /// 事务对象
        /// </summary>
        private static SqlTransaction m_tran;
        /// <summary>
        /// 连接对象
        /// </summary>
        private static SqlConnection m_conn;


        #endregion

    }
}
