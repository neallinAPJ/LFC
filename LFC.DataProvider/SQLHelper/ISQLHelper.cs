using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.DataProvider.SQLHelper
{
    public interface ISQLHelper
    {
        string GetSQLUpdateStatement<T>(T t, Attribute lookupAttribute);
        string GetSQLSelectStatement<T>(T t);
        string GetSQLCreateTempTableStatement<T>();
        string GetSQLInsertTempTableStatement<T>();
        string GetSQLTruncateTempTableStatement<T>();
        string GetSQLCopyTempTableBackStatement<T>();
    }
}
