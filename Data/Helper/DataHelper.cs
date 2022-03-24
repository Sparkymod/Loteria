using Loteria.Data.Core;
using Loteria.Data.Models;
using Loteria.Data.Services;

namespace Loteria.Data.Helper
{
    public class DataHelper
    {
        // Singleton
        private static DataHelper? _instance;
        public static DataHelper Instance => _instance ??= new DataHelper();

        private readonly Generator _generator = new();

        public List<Loto> Lotos { get; set; }
        public List<Tripleta> Tripletas { get; set; }
        
        public DataHelper()
        {
            if (Lotos is null || Tripletas is null)
            {
                LotoServices services = new();
                Lotos = services.GetAll<Loto>("loto");
                Tripletas = services.GetAll<Tripleta>("tripleta");
            }
        }

        public Task<List<NumberData>> GetTripletaData()
        {
            List<NumberData> data = new();
            float count = Tripletas.Count;

            var frequencyOfNumbers = _generator.GetFrequentNumber(Tripletas);

            for (int i = 0; i <= 99; i++)
            {
                int num = frequencyOfNumbers[i];
                var item = new NumberData() { Frequency = num, Number = i, Percentage = (num / count) * 100f };
                data.Add(item);
            }

            return Task.FromResult(data);
        }

        public Task<List<NumberData>> GetLotoData()
        {
            List<NumberData> data = new();
            float count = Lotos.Count;

            var frequencyOfNumbers = _generator.GetFrequentNumber(Lotos);

            for (int i = 0; i < 38; i++)
            {
                int num = frequencyOfNumbers[i];
                var item = new NumberData() { Frequency = num, Number = i + 1, Percentage = (num / count) * 100f };
                data.Add(item);
            }

            return Task.FromResult(data);
        }
    }
}
