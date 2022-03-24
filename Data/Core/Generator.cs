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
        public List<Loto> GenWithoutRepeating(int maxTickets = 6)
        {
            List<Loto> lotos = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Loto newLoto = new();
                bool flag = true;

                while (flag)
                {
                    while (newLoto.Numero1 == 0 || CheckForRepeat(lotos, newLoto.Numero1))
                    {
                        newLoto.Numero1 = NewNumber(1,6);
                    }
                    while (newLoto.Numero2 == 0 || newLoto.Numero1 == newLoto.Numero2 || CheckForRepeat(lotos, newLoto.Numero2))
                    {
                        newLoto.Numero2 = NewNumber(7,12);
                    }
                    while (newLoto.Numero3 == 0 || newLoto.Numero1 == newLoto.Numero3 || newLoto.Numero2 == newLoto.Numero3 || CheckForRepeat(lotos, newLoto.Numero3))
                    {
                        newLoto.Numero3 = NewNumber(13,18);
                    }
                    while (newLoto.Numero4 == 0 || newLoto.Numero1 == newLoto.Numero4 || newLoto.Numero2 == newLoto.Numero4 || newLoto.Numero3 == newLoto.Numero4 || CheckForRepeat(lotos, newLoto.Numero4))
                    {
                        newLoto.Numero4 = NewNumber(19,24);
                    }
                    while (newLoto.Numero5 == 0 || newLoto.Numero1 == newLoto.Numero5 || newLoto.Numero2 == newLoto.Numero5 || newLoto.Numero3 == newLoto.Numero5 || newLoto.Numero4 == newLoto.Numero5 || CheckForRepeat(lotos, newLoto.Numero5))
                    {
                        newLoto.Numero5 = NewNumber(25,30);
                    }
                    while (newLoto.Numero6 == 0 || newLoto.Numero1 == newLoto.Numero6 || newLoto.Numero2 == newLoto.Numero6 || newLoto.Numero3 == newLoto.Numero6 || newLoto.Numero4 == newLoto.Numero6 || newLoto.Numero5 == newLoto.Numero6 || CheckForRepeat(lotos, newLoto.Numero6))
                    {
                        newLoto.Numero6 = NewNumber(31,38);
                    }

                    if (!CheckForRepeatInDb(newLoto))
                    {
                        flag = false;
                    }
                }


                newLoto.Mas = (byte)Random.Next(MASMIN, MASMAX);

                lotos.Add(newLoto);
            }

            return lotos;
        }

        /// <summary>
        /// Generacion de loto, del 1 al 38 y verificando que no se repita en la base de dato.
        /// </summary>
        public List<Loto> Gen1To38(int maxTickets = 6)
        {
            List<Loto> lotos = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Loto newLoto = new();
                bool flag = true;

                while (flag)
                {
                    while (newLoto.Numero1 == 0)
                    {
                        newLoto.Numero1 = NewNumber();
                    }
                    while (newLoto.Numero2 == 0 || newLoto.Numero1 == newLoto.Numero2)
                    {
                        newLoto.Numero2 = NewNumber();
                    }
                    while (newLoto.Numero3 == 0 || newLoto.Numero1 == newLoto.Numero3 || newLoto.Numero2 == newLoto.Numero3)
                    {
                        newLoto.Numero3 = NewNumber();
                    }
                    while (newLoto.Numero4 == 0 || newLoto.Numero1 == newLoto.Numero4 || newLoto.Numero2 == newLoto.Numero4 || newLoto.Numero3 == newLoto.Numero4)
                    {
                        newLoto.Numero4 = NewNumber();
                    }
                    while (newLoto.Numero5 == 0 || newLoto.Numero1 == newLoto.Numero5 || newLoto.Numero2 == newLoto.Numero5 || newLoto.Numero3 == newLoto.Numero5 || newLoto.Numero4 == newLoto.Numero5)
                    {
                        newLoto.Numero5 = NewNumber();
                    }
                    while (newLoto.Numero6 == 0 || newLoto.Numero1 == newLoto.Numero6 || newLoto.Numero2 == newLoto.Numero6 || newLoto.Numero3 == newLoto.Numero6 || newLoto.Numero4 == newLoto.Numero6 || newLoto.Numero5 == newLoto.Numero6)
                    {
                        newLoto.Numero6 = NewNumber();
                    }

                    if (!CheckForRepeatInDb(newLoto))
                    {
                        flag = false;
                    }
                }


                newLoto.Mas = (byte)Random.Next(MASMIN, MASMAX);

                lotos.Add(newLoto);
            }

            return lotos;
        }

        /// <summary>
        /// Generacion de tripleta sin repetirse y verificando en la base de dato.
        /// </summary>
        public List<Tripleta> GenTripleta(int maxTickets = 6)
        {
            List<Tripleta> tripleta = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Tripleta newTripleta = new();
                bool flag = true;

                while (flag)
                {
                    while (newTripleta.Numero1 == 0 || CheckForRepeat(tripleta, newTripleta.Numero1))
                    {
                        newTripleta.Numero1 = NewNumber(0, 99);
                    }
                    while (newTripleta.Numero2 == 0 || newTripleta.Numero1 == newTripleta.Numero2 || CheckForRepeat(tripleta, newTripleta.Numero2))
                    {
                        newTripleta.Numero2 = NewNumber(0, 99);
                    }
                    while (newTripleta.Numero3 == 0 || newTripleta.Numero1 == newTripleta.Numero3 || newTripleta.Numero2 == newTripleta.Numero3 || CheckForRepeat(tripleta, newTripleta.Numero3))
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

            return tripleta;
        }

        /// <summary>
        /// Formula v1.2
        /// </summary>
        public List<Loto> FormulaDePedro(int maxTickets = 5)
        {
            List<Loto> listaDePedro = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Loto newLoto = new();
                bool flag = true;

                while (flag)
                {
                    while (newLoto.Numero1 == 0 || CheckForRepeat(listaDePedro, newLoto.Numero1))
                    {
                        newLoto.Numero1 = NewNumber(1, 10);
                    }
                    while (newLoto.Numero2 == 0 || newLoto.Numero1 == newLoto.Numero2 || CheckForRepeat(listaDePedro, newLoto.Numero2))
                    {
                        newLoto.Numero2 = NewNumber(11, 20);
                    }
                    while (newLoto.Numero3 == 0 || newLoto.Numero1 == newLoto.Numero3 || newLoto.Numero2 == newLoto.Numero3 || CheckForRepeat(listaDePedro, newLoto.Numero3))
                    {
                        newLoto.Numero3 = NewNumber(21, 30);
                    }
                    while (newLoto.Numero4 == 0 || newLoto.Numero1 == newLoto.Numero4 || newLoto.Numero2 == newLoto.Numero4 || newLoto.Numero3 == newLoto.Numero4 || CheckForRepeat(listaDePedro, newLoto.Numero4))
                    {
                        newLoto.Numero4 = NewNumber(1, 20);
                    }
                    while (newLoto.Numero5 == 0 || newLoto.Numero1 == newLoto.Numero5 || newLoto.Numero2 == newLoto.Numero5 || newLoto.Numero3 == newLoto.Numero5 || newLoto.Numero4 == newLoto.Numero5 || CheckForRepeat(listaDePedro, newLoto.Numero5))
                    {
                        newLoto.Numero5 = NewNumber(1, 30);
                    }
                    while (newLoto.Numero6 == 0 || newLoto.Numero1 == newLoto.Numero6 || newLoto.Numero2 == newLoto.Numero6 || newLoto.Numero3 == newLoto.Numero6 || newLoto.Numero4 == newLoto.Numero6 || newLoto.Numero5 == newLoto.Numero6 || CheckForRepeat(listaDePedro, newLoto.Numero6))
                    {
                        newLoto.Numero6 = NewNumber(1, 38);
                    }

                    while (newLoto.Mas == 0 || CheckForRepeat(listaDePedro, newLoto.Mas))
                    {
                        newLoto.Mas = NewNumber(1, 10);
                    }

                    if (!CheckForRepeatInDb(newLoto))
                    {
                        flag = false;
                    }
                }

                listaDePedro.Add(newLoto);
            }

            return listaDePedro;
        }
    }
}
