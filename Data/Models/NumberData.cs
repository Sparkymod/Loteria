namespace Loteria.Data.Models
{
    public class NumberData
    {
        public int Frequency { get; set; }
        public int Number { get; set; }
        public float Percentage { get; set; }

        public NumberData()
        {
            Number = 0;
            Frequency = 0;
            Percentage = 0;
        }

        public static List<NumberData> List => new();
    }
}
