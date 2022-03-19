using Loteria.Data.Models;
using Loteria.Data.Services;

namespace Loteria.Data.Helper
{
    public class DataHelper
    {
        private static DataHelper? _instance;
        public static DataHelper Instance => _instance ??= new DataHelper();

        public List<Loto> Lotos { get; set; } = new ();
        public List<Tripleta> Tripletas { get; set; } = new ();

        public DataHelper()
        {
            LotoServices services = new();
            Lotos = services.GetAll<Loto>("loto");
            Tripletas = services.GetAll<Tripleta>("tripleta");
        }
    }
}
