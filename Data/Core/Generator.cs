using HtmlAgilityPack;
using Loteria.Data.Extensions;
using Loteria.Data.Models;
using Loteria.Data.Services;
using System.Diagnostics;
using System.Net;

namespace Loteria.Data.Core
{
    public class Generator
    {
        private const byte LOTOMIN = 1;
        private const byte LOTOMAX = 38;

        private const byte MASMIN = 1;
        private const byte MASMAX = 10;

        private const byte SUPERMIN = 1;
        private const byte SUPERMAX = 15;

        private List<Pool> Pools = new();

        private Random Random = new();

        public List<Loto> Gen1To38(int maxTickets = 6)
        {
            List<Loto> lotos = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Loto newLoto = new();

                while (newLoto.Numero1 == 0 || CheckForRepeat(lotos, newLoto.Numero1))
                {
                    newLoto.Numero1 = NewNumber();
                }
                while (newLoto.Numero2 == 0 || newLoto.Numero1 == newLoto.Numero2 || CheckForRepeat(lotos, newLoto.Numero2))
                {
                    newLoto.Numero2 = NewNumber();
                }
                while (newLoto.Numero3 == 0 || newLoto.Numero1 == newLoto.Numero3 || newLoto.Numero2 == newLoto.Numero3 || CheckForRepeat(lotos, newLoto.Numero3))
                {
                    newLoto.Numero3 = NewNumber();
                }
                while (newLoto.Numero4 == 0 || newLoto.Numero1 == newLoto.Numero4 || newLoto.Numero2 == newLoto.Numero4 || newLoto.Numero3 == newLoto.Numero4 || CheckForRepeat(lotos, newLoto.Numero4))
                {
                    newLoto.Numero4 = NewNumber();
                }
                while (newLoto.Numero5 == 0 || newLoto.Numero1 == newLoto.Numero5 || newLoto.Numero2 == newLoto.Numero5 || newLoto.Numero3 == newLoto.Numero5 || newLoto.Numero4 == newLoto.Numero5 || CheckForRepeat(lotos, newLoto.Numero5))
                {
                    newLoto.Numero5 = NewNumber();
                }
                while (newLoto.Numero6 == 0 || newLoto.Numero1 == newLoto.Numero6 || newLoto.Numero2 == newLoto.Numero6 || newLoto.Numero3 == newLoto.Numero6 || newLoto.Numero4 == newLoto.Numero6 || newLoto.Numero5 == newLoto.Numero6 || CheckForRepeat(lotos, newLoto.Numero6))
                {
                    newLoto.Numero6 = NewNumber();
                }

                newLoto.Mas = (byte)Random.Next(MASMIN, MASMAX);

                lotos.Add(newLoto);
            }

            return lotos;
        }

        private byte NewNumber(byte min = 1, byte max = 38) => (byte)Random.Next(min, max);

        private bool CheckForRepeat(List<Loto> lotos, byte number)
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
        /// Formula v1.2
        /// </summary>
        /// <param name="maxTickets"></param>
        /// <returns></returns>
        #region Formula Pedro
        public List<Loto> FormulaDePedro(int maxTickets = 5)
        {
            List<Loto> listaDePedro = new();

            for (int i = 0; i < maxTickets; i++)
            {
                Loto loto = new();

                loto.Numero1 = (byte)Random.Next(1, 10);

                while (loto.Numero2 == 0 || loto.Numero1 == loto.Numero2)
                {
                    loto.Numero2 = (byte)Random.Next(11, 20);
                }
                while (loto.Numero3 == 0)
                {
                    loto.Numero3 = (byte)Random.Next(21, 30);
                }
                while (loto.Numero4 == 0 || loto.Numero3 == loto.Numero4)
                {
                    loto.Numero4 = (byte)Random.Next(1, 20);
                }
                while (loto.Numero5 == 0 || loto.Numero1 == loto.Numero5 || loto.Numero2 == loto.Numero5)
                {
                    loto.Numero5 = (byte)Random.Next(1, 30);
                }
                while (loto.Numero6 == 0 || loto.Numero1 == loto.Numero6 || loto.Numero2 == loto.Numero6 || loto.Numero3 == loto.Numero6 || loto.Numero4 == loto.Numero6 || loto.Numero5 == loto.Numero6 || loto.Numero5 == loto.Numero6)
                {
                    loto.Numero6 = (byte)Random.Next(1, 38);
                }

                loto.Mas = (byte)Random.Next(1, 10);

                listaDePedro.Add(loto);
            }

            return listaDePedro;
        }

        #endregion

        public async void GeneratePoolToDB()
        {
            List<Task> tasks = new();
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            byte max = 38;
            // Generacion de 6 posiciones con rangos de 38 numeros
            tasks.Add(Task.Run(() => GenerateWithinRange(1, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(2, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(3, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(4, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(5, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(6, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(7, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(8, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(9, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(10, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(11, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(12, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(13, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(14, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(15, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(16, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(17, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(18, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(19, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(20, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(21, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(22, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(23, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(24, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(25, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(26, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(27, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(28, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(29, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(30, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(31, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(32, max)));
            tasks.Add(Task.Run(() => GenerateWithinRange(33, max)));

            await Task.WhenAll(tasks);
            //task.Wait();
            var services = new LotoServices();
            int count = 1;
            foreach (var pool in Pools)
            {
                Pool? temp = services.GetPool(count);
                if (temp != null)
                {
                    Console.WriteLine($"Skipping {count++}");
                    continue;
                }
                services.AddPool(pool);
                Console.WriteLine($"Success inserting {count++}");
            }
            Console.WriteLine($"Success in: {sw.Elapsed}");
        }

        int Count = 1;
        private async void GenerateWithinRange(byte min = 1, byte max = 38)
        {
            List<Pool> pools = new();
            //byte index1 = 0;
            byte index2 = 0;
            byte index3 = 0;
            byte index4 = 0;
            byte index5 = 0;
            byte index6 = 0;


            for (byte pos2 = (byte)(min + 1 + index2); pos2 <= (byte)(max - 4); pos2++)
            {
                if (min == pos2)
                {
                    continue;
                }

                for (byte pos3 = (byte)(min + 2 + index3); pos3 <= (byte)(max - 3); pos3++)
                {
                    if (min == pos3 || pos2 == pos3)
                    {
                        continue;
                    }

                    for (byte pos4 = (byte)(min + 3 + index4); pos4 <= (byte)(max - 2); pos4++)
                    {
                        if (min == pos4 || pos2 == pos4 || pos3 == pos4)
                        {
                            continue;
                        }

                        for (byte pos5 = (byte)(min + 4 + index5); pos5 <= (byte)(max - 1); pos5++)
                        {
                            if (min == pos5 || pos2 == pos5 || pos3 == pos5 || pos4 == pos5)
                            {
                                continue;
                            }

                            for (byte pos6 = (byte)(min + 5 + index6); pos6 <= max; pos6++)
                            {
                                if (min == pos6 || pos2 == pos6 || pos3 == pos6 || pos4 == pos6 || pos5 == pos6)
                                {
                                    continue;
                                }

                                Pool pool = new();

                                pool.PoolId = Count;
                                pool.Numero1 = min;
                                pool.Numero2 = pos2;
                                pool.Numero3 = pos3;
                                pool.Numero4 = pos4;
                                pool.Numero5 = pos5;
                                pool.Numero6 = pos6;

                                var sorted = pool.SortedList();

                                var found = pools.Any(x => x.SortedList().SequenceEqual(sorted));
                                if (found)
                                {
                                    continue;
                                }

                                pools.Add(pool);

                                Count++;
                                Console.WriteLine($"Gen: #{min} => [ {min} {pos2} {pos3} {pos4} {pos5} {pos6} ] --> {Count}");
                            }
                            index6 = 0;
                            index5++;
                        }
                        index5 = 0;
                        index4++;
                    }
                    index4 = 0;
                    index3++;
                }
                index3 = 0;
                index2++;

                //index2 = 0;
                //index1++;

            }
            Pools.AddRange(pools);
            //Pools = pools.Except(Pools).ToList();
            await Task.CompletedTask;
        }

        public void Start()
        {
            GeneratePoolToDB();
        }
    }
}
