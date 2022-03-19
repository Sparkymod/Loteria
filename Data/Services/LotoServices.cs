using Loteria.Data.Models;
using Loteria.Database;

namespace Loteria.Data.Services
{
    public class LotoServices
    {
        public List<T> GetAll<T>(string table)
        {
            using DatabaseManager context = new();
            return context.Loto.GetAll<T>(table).ToList();
        }

        public void AddPool<T>(string table, T pool)
        {
            using DatabaseManager context = new();
            context.Loto.InsertPool(table, pool);
        }

        internal T? GetPool<T>(string table, int poolId)
        {
            using DatabaseManager context = new();
            return context.Loto.GetPool<T>(table, poolId);
        }

        internal void Add<T>(string table, T loto)
        {
            using DatabaseManager context = new();
            context.Loto.Add(table, loto);
        }
    }
}
