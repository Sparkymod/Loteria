using HtmlAgilityPack;
using Loteria.Data.Extensions;
using Loteria.Data.Helper;
using Loteria.Data.Models;
using Loteria.Data.Services;
using System.Net;

namespace Loteria.Data.Core
{
    public class Generator : MathHelper
    {

        /// <summary>
        /// Generacion de loto, del 1 al 38 y verificando que no se repita en la base de dato.
        /// </summary>
        public Task<List<Loto>> GenWithoutRepeating(int maxTickets = 6)
        {
            List<Loto> lotos = new();
            List<Loto> result = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Loto newLoto = new();
                bool flag = true;

                if (lotos.Count == 6)
                {
                    lotos.Clear();
                }

                while (flag)
                {
                    while (newLoto.Numero1 == 0 || CheckForRepeat(lotos, newLoto.Numero1))
                    {
                        newLoto.Numero1 = NewNumber(1, 6);
                    }
                    while (newLoto.Numero2 == 0 || newLoto.Numero1 == newLoto.Numero2 || CheckForRepeat(lotos, newLoto.Numero2))
                    {
                        newLoto.Numero2 = NewNumber(1, 12);
                    }
                    while (newLoto.Numero3 == 0 || newLoto.Numero1 == newLoto.Numero3 || newLoto.Numero2 == newLoto.Numero3 || CheckForRepeat(lotos, newLoto.Numero3))
                    {
                        newLoto.Numero3 = NewNumber(1, 18);
                    }
                    while (newLoto.Numero4 == 0 || newLoto.Numero1 == newLoto.Numero4 || newLoto.Numero2 == newLoto.Numero4 || newLoto.Numero3 == newLoto.Numero4 || CheckForRepeat(lotos, newLoto.Numero4))
                    {
                        newLoto.Numero4 = NewNumber(1, 24);
                    }
                    while (newLoto.Numero5 == 0 || newLoto.Numero1 == newLoto.Numero5 || newLoto.Numero2 == newLoto.Numero5 || newLoto.Numero3 == newLoto.Numero5 || newLoto.Numero4 == newLoto.Numero5 || CheckForRepeat(lotos, newLoto.Numero5))
                    {
                        newLoto.Numero5 = NewNumber(1, 30);
                    }
                    while (newLoto.Numero6 == 0 || newLoto.Numero1 == newLoto.Numero6 || newLoto.Numero2 == newLoto.Numero6 || newLoto.Numero3 == newLoto.Numero6 || newLoto.Numero4 == newLoto.Numero6 || newLoto.Numero5 == newLoto.Numero6 || CheckForRepeat(lotos, newLoto.Numero6))
                    {
                        newLoto.Numero6 = NewNumber(1, 38);
                    }

                    while (newLoto.Mas == 0 || CheckForRepeatMas(lotos, newLoto.Mas))
                    {
                        newLoto.Mas = NewNumber(1, 10);
                    }

                    if (!CheckForRepeatInDb(newLoto))
                    {
                        flag = false;
                    }
                }

                result.Add(newLoto);
                lotos.Add(newLoto);
            }

            return Task.FromResult(result);
        }

        /// <summary>
        /// Generacion de tripleta sin repetirse y verificando en la base de dato.
        /// </summary>
        public Task<List<Tripleta>> GenTripleta(int maxTickets = 6)
        {
            List<Tripleta> tripleta = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Tripleta newTripleta = new();
                bool flag = true;

                while (flag)
                {
                    while (newTripleta.Numero1 == 0)
                    {
                        newTripleta.Numero1 = NewNumber(0, 99);
                    }
                    while (newTripleta.Numero2 == 0 || newTripleta.Numero1 == newTripleta.Numero2)
                    {
                        newTripleta.Numero2 = NewNumber(0, 99);
                    }
                    while (newTripleta.Numero3 == 0 || newTripleta.Numero1 == newTripleta.Numero3 || newTripleta.Numero2 == newTripleta.Numero3)
                    {
                        newTripleta.Numero3 = NewNumber(0, 99);
                    }
                    if (!CheckForRepeatInDb(newTripleta))
                    {
                        flag = false;
                    }
                }

                tripleta.Add(newTripleta);
            }

            return Task.FromResult(tripleta);
        }
    }
}
