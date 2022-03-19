namespace Loteria.Data.Models
{
    public class Tripleta
    {
        public DateTime Fecha { get; set; }
        public byte Numero1 { get; set; }
        public byte Numero2 { get; set; }
        public byte Numero3 { get; set; }

        public Tripleta()
        {
            Fecha = DateTime.Now;
        }

        public byte[] SortedList()
        {
            var result = new byte[] { Numero1, Numero2, Numero3 };
            Array.Sort(result);
            return result;
        }

        public override string ToString() => $"{Numero1} {Numero2} {Numero3}";
    }
}
