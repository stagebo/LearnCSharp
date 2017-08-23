using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace BaseCSharp.CodeCollection.SqlServer
{
    /// <summary>
    /// SqlDatabase 类
    /// </summary>
    
    public class SqlDatabase : Database
    {
        #region 构造和析构

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="connctionString">数据库连接字符串</param>
        public SqlDatabase(String connctionString)
        {
            Debug.Assert(!string.IsNullOrEmpty(connctionString), "数据库路径 " + connctionString + " 不存在！");
            m_connString = connctionString;
            m_dbFactory = SqlClientFactory.Instance;
            m_dbFactory.CreateConnectionStringBuilder().ConnectionString = m_connString;
        }

        #endregion // 构造和析构

        #region 公有方法
        #endregion // 公有方法

        #region 保护方法
        #endregion // 保护方法

        #region 私有方法
        #endregion // 私有方法

        #region 属性及其私有变量
        #endregion // 属性及其私有变量

    }
}