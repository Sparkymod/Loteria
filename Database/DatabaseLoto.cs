using Loteria.Data.Models;
using SqlKata;
using SqlKata.Execution;

namespace Loteria.Database
{
    public class DatabaseLoto : DatabaseFactory
    {
        public DatabaseLoto() : base("loto")
        {

        }

        public IEnumerable<T> GetAll<T>(string table) => QueryFactory.Query(table).Select("*").Get<T>();

        public void InsertPool<T>(string table, T pool) => QueryFactory.Query(table).Insert(pool);

        internal T? GetPool<T>(string table, int poolId) => QueryFactory.Query(table).Select("*").Where("PoolId", poolId).Get<T>().FirstOrDefault();

        internal T? GetTicket<T>(string table, DateTime fecha) => QueryFactory.Query(table).Select("*").Where("Fecha", fecha).Get<T>().FirstOrDefault();

        internal void Add<T>(string table, T loto) => QueryFactory.Query(table).Insert(loto);
    }
}
