using HtmlAgilityPack;
using Loteria.Data.Extensions;
using Loteria.Data.Models;
using Loteria.Data.Services;
using Serilog;
using System.Net;

namespace Loteria.Data.Core
{
    public class Extractor
    {
        public List<Loto> Lotos = new ();

        public void ExtractFromWeb(string date = "11-08-2010")
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(new WebClient().DownloadString($"https://www.conectate.com.do/loterias/leidsa/loto-mas?date={date}"));

            var numeros = from x in doc.DocumentNode.DescendantNodes()
                          where x.Name == "div" && x.GetAttributeValue("class", "") == "game-scores ball-mode"
                          select x.InnerText;

            var fechas = from x in doc.DocumentNode.DescendantNodes()
                         where x.Name == "span" && x.GetAttributeValue("class", "") == "session-date session-badge"
                         select x.InnerText;

            var fechasList = fechas.ToList();
            var numerosList = numeros.ToList();

            for (int i = 0; i < fechasList.Count; i++)
            {
                var newFecha = fechasList[i].Replace("\n", "").Trim();
                var newListNumer = new List<byte>();
                var loto = new Loto();

                string formatDate = string.Format("{0}-{1,2:00}-{2,2:00}", newFecha[6..10], newFecha[3..5], newFecha[0..2]);
                loto.Fecha = DateTime.Parse(formatDate);

                var tempList = FormatNumbers(numerosList[i]);
                
                if (tempList.Count < 6)
                {
                    Log.Logger.Warning($"Error in date: {formatDate} --> Can't obtain all numbers correctly.");
                    continue;
                }

                loto.Numero1 = tempList[0];
                loto.Numero2 = tempList[1];
                loto.Numero3 = tempList[2];
                loto.Numero4 = tempList[3];
                loto.Numero5 = tempList[4];
                loto.Numero6 = tempList[5];
                if (tempList.Count == 7)
                {
                    loto.Mas = tempList[6];
                }
                else if(tempList.Count == 8)
                {
                    loto.SuperMas = tempList[7];
                }

                if (Lotos.Where(x => x.Fecha == loto.Fecha).Any())
                {
                    continue;
                }
                Lotos.Add(loto);
            }
        }

        public void ExecuteScript()
        {
            for (int year = 2010; year <= 2022; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    for (int day = 1; day <= 28; day+=+4)
                    {
                        string formatStartDate = string.Format("{0}-{1,2:00}-{2,2:00}", year, month, day);
                        if (DateTime.Parse(formatStartDate) > DateTime.Today)
                        {
                            break;
                        }
                        formatStartDate = string.Format("{0,2:00}-{1,2:00}-{2}", day, month, year);
                        ExtractFromWeb(formatStartDate);
                    }
                }

                Log.Logger.Information($"Success adding Year: {year}");
                Log.Logger.Information($"Lotos in the list => {Lotos.Count}");
            }

            LotoServices services = new();
            foreach (var loto in Lotos)
            {
                try
                {
                    services.AddLoto(loto);
                }
                catch (Exception ex)
                {
                    Log.Logger.Warning($"Error adding {loto}. => {ex.Message}");
                    continue;
                }
            }
        }

        public List<byte> FormatNumbers(string item)
        {
            var result = new List<byte>();
            item.RemoveSpecialChars().Split(new[] { " " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToList().ForEach(x => result.Add(byte.Parse(x)));
            return result;
        }
    }
}
