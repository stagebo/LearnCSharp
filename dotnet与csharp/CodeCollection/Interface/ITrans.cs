using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp.CodeCollection
{

    //
    // 摘要:
    //     实现事务操作
    public interface ITrans : IDisposable
    {
        //
        // 摘要:
        //     数据库对象
        IDatabase Database { get; }
        //
        // 摘要:
        //     数据库连接对象
        DbConnection DbConnection { get; }
        //
        // 摘要:
        //     事务对象
        DbTransaction DbTrans { get; }

        //
        // 摘要:
        //     关闭连接
        void Close();
        //
        // 摘要:
        //     提交事务
        void Commit();
        //
        // 摘要:
        //     回滚事务
        void Rollback();
    }
}
