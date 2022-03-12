using Loteria.Data.Models;
using SqlKata.Execution;

namespace Loteria.Database
{
    public class DatabaseLoto : DatabaseFactory
    {
        public DatabaseLoto() : base("loto")
        {

        }

        public IEnumerable<Loto> GetAllLotos() => QueryFactory.Query(TableName).Select("*").Get<Loto>();

        public IEnumerable<Pool> GetAllPool() => QueryFactory.Query("pool").Select("*").Get<Pool>();

        public void InsertPool(Pool pool) => QueryFactory.Query("pool").Insert(pool);

        internal Pool? GetPool(int poolId) => QueryFactory.Query("pool").Select("*").Where("PoolId", poolId).Get<Pool>().FirstOrDefault();

        internal void Add(Loto loto) => QueryFactory.Query(TableName).Insert(loto);
    }
}
