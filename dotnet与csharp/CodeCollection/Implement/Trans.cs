using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;


namespace BaseCSharp.CodeCollection
{
    /// <summary>
    /// 事务 类
    /// </summary>
    /// <summary>
    /// 名称: 事务 类
    /// 功能: 实现事务操作
    /// </summary>   
    [CLSCompliant(false)]
    public class Trans : ITrans
    {

        #region 构造和析构

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="database">数据库接口</param>
        public Trans(IDatabase database)
        {
            m_database = database;
            m_conn = m_database.CreateConnection();
            try
            {
                m_conn.Open();
                m_dbTrans = m_conn.BeginTransaction();
            }
            catch
            {
                Debug.Assert(false, "数据库连接打开失败");
            }
        }

        #endregion // 构造和析构

        #region 公有方法

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            m_dbTrans.Commit();
            this.Close();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            try
            {
                m_dbTrans.Rollback();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (m_conn.State == System.Data.ConnectionState.Open)
            {
                m_conn.Close();
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

        #endregion // 公有方法

        #region 保护方法
        #endregion // 保护方法

        #region 属性及其私有变量

        /// <summary>
        /// 数据库对象
        /// </summary>
        [CLSCompliant(false)]
        public IDatabase Database
        {
            get
            {
                return m_database;
            }
        }
        private IDatabase m_database;


        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public DbConnection DbConnection
        {
            get
            {
                return this.m_conn;
            }
        }
        private DbConnection m_conn;

        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction DbTrans
        {
            get
            {
                return this.m_dbTrans;
            }
        }
        private DbTransaction m_dbTrans;

        #endregion // 属性及其私有变量

    }
}