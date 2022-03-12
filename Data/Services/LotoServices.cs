using Loteria.Data.Models;
using Loteria.Database;

namespace Loteria.Data.Services
{
    public class LotoServices
    {
        public List<Loto> GetAllTickets()
        {
            using DatabaseManager context = new();
            return context.Loto.GetAllLotos().ToList();
        }

        public void AddPool(Pool pool)
        {
            using DatabaseManager context = new();
            context.Loto.InsertPool(pool);
        }

        internal Pool? GetPool(int poolId)
        {
            using DatabaseManager context = new();
            return context.Loto.GetPool(poolId);
        }

        internal List<Pool> GetAllPool()
        {
            using DatabaseManager context = new();
            return context.Loto.GetAllPool().ToList();
        }

        internal void AddLoto(Loto loto)
        {
            using DatabaseManager context = new();
            context.Loto.Add(loto);
        }
    }
}
