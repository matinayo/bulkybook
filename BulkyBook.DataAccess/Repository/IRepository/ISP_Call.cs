using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        // returns single value
        T Single<T> (string procedureName, DynamicParameters param = null);

        // execute to a database and not retrieving anything
        void Execute(string procedureName, DynamicParameters param = null);

        // returns one complete record
        T OneRecord<T>(string procedureName, DynamicParameters param = null);

        // returns all rows
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        // returns two tables
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);
    }
}
