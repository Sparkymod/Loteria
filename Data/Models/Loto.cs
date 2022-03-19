namespace Loteria.Data.Models
{
    public class Loto
    {
        public DateTime Fecha { get; set; }
        public byte Numero1 { get; set; }
        public byte Numero2 { get; set; }
        public byte Numero3 { get; set; }
        public byte Numero4 { get; set; }
        public byte Numero5 { get; set; }
        public byte Numero6 { get; set; }
        public byte Mas { get; set; }
        public byte SuperMas { get; set; }

        public Loto()
        {
            Fecha = DateTime.Now;
        }

        public byte[] SortedList()
        {
            var result = new byte[] { Numero1, Numero2, Numero3, Numero4, Numero5, Numero6 };
            Array.Sort(result);
            return result;
        }

        public override string ToString() => $"{Numero1} {Numero2} {Numero3} {Numero4} {Numero5} {Numero6} {Mas} {SuperMas}";
    }
}
