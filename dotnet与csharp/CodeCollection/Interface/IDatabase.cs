using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.CodeCollection
{

    //
    // 摘要:
    //     实现数据操作
    public interface IDatabase : IDisposable
    {
        //
        // 摘要:
        //     数据库连接字符串
        string ConnectionString { get; }
        //
        // 摘要:
        //     数据源工厂
        DbProviderFactory DbProviderFactory { get; }

        //
        // 摘要:
        //     创建数据连接对象
        //
        // 返回结果:
        //     数据连接对象
        DbConnection CreateConnection();
        //
        // 摘要:
        //     创建事务对象
        ITrans CreateTrans();
        //
        // 摘要:
        //     执行单条SQL语句
        //
        // 参数:
        //   sql:
        //     数据操作语句
        //
        // 返回结果:
        //     语句执行影响记录数量
        int Execute(string sql);
        //
        // 摘要:
        //     执行多条SQL语句（事务执行）
        //
        // 参数:
        //   sqls:
        //     数据操作语句集合
        //
        // 返回结果:
        //     语句执行影响记录数量
        int Execute(ICollection<string> sqls);
        //
        // 摘要:
        //     执行单条SQL语句
        //
        // 参数:
        //   trans:
        //     事务对象
        //
        //   sql:
        //     数据操作语句
        //
        // 返回结果:
        //     语句执行影响记录数量
        int Execute(ITrans trans, string sql);
        //
        // 摘要:
        //     执行多条SQL语句（事务执行）
        //
        // 参数:
        //   trans:
        //     事务对象
        //
        //   sqls:
        //     数据操作语句集合
        //
        // 返回结果:
        //     语句执行影响记录数量
        int Execute(ITrans trans, ICollection<string> sqls);
        //
        // 摘要:
        //     执行单条查询语句得到数据集
        //
        // 参数:
        //   sql:
        //     查询语句
        //
        // 返回结果:
        //     数据集
        DataSet QueryDataSet(string sql);
        //
        // 摘要:
        //     执行多条查询语句得到数据集（事务执行）
        //
        // 参数:
        //   sqls:
        //     查询语句集合
        //
        // 返回结果:
        //     数据集
        DataSet QueryDataSet(ICollection<string> sqls);
        //
        // 摘要:
        //     执行单条查询语句得到数据只进流
        //
        // 参数:
        //   sql:
        //     查询语句
        //
        // 返回结果:
        //     只进数据流
        DbDataReader QueryReader(string sql);
        //
        // 摘要:
        //     执行单条查询语句得到数据只进流
        //
        // 参数:
        //   conn:
        //     数据库连接对象
        //
        //   sql:
        //     查询语句
        //
        // 返回结果:
        //     只进数据流
        DbDataReader QueryReader(DbConnection conn, string sql);
        //
        // 摘要:
        //     返回结果集中的第一行第一列
        //
        // 参数:
        //   sql:
        //     数据查询语句
        //
        // 返回结果:
        //     结果集中的第一行第一列
        object QueryScalar(string sql);
        //
        // 摘要:
        //     执行单条查询语句得到数据表
        //
        // 参数:
        //   sql:
        //     查询语句
        //
        // 返回结果:
        //     数据表
        DataTable QueryTable(string sql);
        //
        // 摘要:
        //     执行单条查询语句得到数据表
        //
        // 参数:
        //   trans:
        //     事务对象
        //
        //   sql:
        //     查询语句
        //
        // 返回结果:
        //     数据表
        DataTable QueryTable(ITrans trans, string sql);
        //
        // 摘要:
        //     有效值（消除特殊字符）
        //
        // 参数:
        //   value:
        //     值
        //
        // 返回结果:
        //     有效值
        string ValidValue(string value);
    }
}
