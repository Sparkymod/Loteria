namespace Loteria.Data.Models
{
    public class TripletaPool
    {
        public int PoolId = 1;
        public byte Numero1 { get; set; }
        public byte Numero2 { get; set; }
        public byte Numero3 { get; set; }

        public byte[] SortedList()
        {
            var result = new byte[] { Numero1, Numero2, Numero3 };
            Array.Sort(result);
            return result;
        }
    }
}
