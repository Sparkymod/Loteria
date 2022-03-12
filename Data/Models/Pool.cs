namespace Loteria.Data.Models
{
    public class Pool
    {
        public int PoolId = 1;
        public byte Numero1 { get; set; }
        public byte Numero2 { get; set; }
        public byte Numero3 { get; set; }
        public byte Numero4 { get; set; }
        public byte Numero5 { get; set; }
        public byte Numero6 { get; set; }

        public byte[] SortedList()
        {
            var result = new byte[] { Numero1, Numero2, Numero3, Numero4, Numero5, Numero6 };
            Array.Sort(result);
            return result;
        }
    }
}
