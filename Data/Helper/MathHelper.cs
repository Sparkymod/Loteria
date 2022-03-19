using Loteria.Data.Models;
using Loteria.Data.Services;

namespace Loteria.Data.Helper
{
    public abstract class MathHelper
    {
        protected const byte MASMIN = 1;
        protected const byte MASMAX = 10;

        protected Random Random { get; set; } = new();

        protected byte NewNumber(byte min = 1, byte max = 38) => (byte)Random.Next(min, max);

        #region Methods Helpers
        /// <summary>
        /// Method used for <seealso cref="Loto"/> to verify repeated numbers.
        /// </summary>
        protected bool CheckForRepeat(List<Loto> lotos, byte number)
        {
            foreach (var loto in lotos)
            {
                if (loto.Numero1 == number || loto.Numero2 == number || loto.Numero3 == number || loto.Numero4 == number || loto.Numero5 == number || loto.Numero6 == number)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method used for <seealso cref="Tripleta"/> to verify repeated numbers.
        /// </summary>
        protected bool CheckForRepeat(List<Tripleta> pools, byte number)
        {
            foreach (var loto in pools)
            {
                if (loto.Numero1 == number || loto.Numero2 == number || loto.Numero3 == number)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get Frequent number from all lotos.
        /// </summary>
        public int[] GetFrequentNumber(List<Loto> lotos)
        {
            int[] tempPercentage = new int[38];

            for (int i = 0; i < 38; i++)
            {
                foreach (var loto in lotos)
                {
                    int num = i + 1;
                    if (loto.Numero1 == num || loto.Numero2 == num || loto.Numero3 == num || loto.Numero4 == num || loto.Numero5 == num || loto.Numero6 == num)
                    {
                        tempPercentage[i]++;
                    }
                }
            }

            return tempPercentage;
        }

        /// <summary>
        /// Get Frequent number from all Tripletas.
        /// </summary>
        public int[] GetFrequentNumber(List<Tripleta> tripletas)
        {
            int[] tempPercentage = new int[100];

            for (int i = 0; i < 100; i++)
            {
                foreach (var loto in tripletas)
                {
                    if (loto.Numero1 == i || loto.Numero2 == i || loto.Numero3 == i)
                    {
                        tempPercentage[i]++;
                    }
                }
            }

            return tempPercentage;
        }

        /// <summary>
        /// Check if this Tripleta exists in Database
        /// </summary>
        public bool CheckForRepeatInDb(Tripleta ticket)
        {
            LotoServices services = new();

            var sorted = new byte[] { ticket.Numero1, ticket.Numero2, ticket.Numero3 };
            Array.Sort(sorted);

            return DataHelper.Instance.Tripletas.Any(x => x.SortedList().SequenceEqual(sorted));
        }

        /// <summary>
        /// Check if this Loto exists in Database and if is posible at least at 3 positions.
        /// </summary>
        public bool CheckForRepeatInDb(Loto ticket)
        {
            LotoServices services = new();

            var sorted = new byte[] { ticket.Numero1, ticket.Numero2, ticket.Numero3, ticket.Numero4, ticket.Numero5, ticket.Numero6 };

            Array.Sort(sorted);

            if (!DataHelper.Instance.Lotos.Any(x => x.SortedList().SequenceEqual(sorted)))
            {
                return false;
            }
            return true;
        }

        public List<byte[]> CheckSort(Loto ticket, byte[] list, byte Numero6)
        {
            List<byte[]> lista = new List<byte[]>();

            // 6
            lista.Add(new byte[] { ticket.Numero1, ticket.Numero2, ticket.Numero3, ticket.Numero4, ticket.Numero5, ticket.Numero6 });
            // 5
            lista.Add(new byte[] { ticket.Numero1, ticket.Numero2, ticket.Numero3, ticket.Numero4, ticket.Numero5 });
            lista.Add(new byte[] { ticket.Numero1, ticket.Numero2, ticket.Numero3, ticket.Numero4, ticket.Numero6 });
            lista.Add(new byte[] { ticket.Numero1, ticket.Numero2, ticket.Numero3, ticket.Numero6, ticket.Numero5 });
            lista.Add(new byte[] { ticket.Numero1, ticket.Numero2, ticket.Numero6, ticket.Numero4, ticket.Numero5 });
            lista.Add(new byte[] { ticket.Numero1, ticket.Numero6, ticket.Numero3, ticket.Numero4, ticket.Numero5 });
            lista.Add(new byte[] { ticket.Numero6, ticket.Numero2, ticket.Numero3, ticket.Numero4, ticket.Numero5 });

            lista.ForEach(numbers => Array.Sort(numbers));

            return lista;
        }

        public int Permutation(int number)
        {
            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }

        public int Combination(int objects, int sample) => Permutation(objects) / Permutation(sample) * Permutation(objects - sample);

        #endregion
    }
}
