using Loteria.Data.Models;
using Loteria.Data.Services;
using System.Diagnostics;

namespace Loteria.Data.Core
{
    public class PoolGenerator
    {
        public List<Pool> Pools = new();
        public List<TripletaPool> TripletaPools = new();
        private int Count = 1;
        private int CountTripleta = 1;

        #region Loto Pools
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

            await Task.WhenAll(tasks);
            //task.Wait();
            var services = new LotoServices();
            int count = 1;
            string table = "pool";
            foreach (var pool in Pools)
            {
                Pool? temp = services.GetPool<Pool>(table, count);
                if (temp != null && temp.PoolId == count)
                {
                    Console.WriteLine($"Skipping {count++}");
                    continue;
                }
                services.AddPool(table, pool);
                Console.WriteLine($"Success inserting {count++}");
            }
            Console.WriteLine($"Success in: {sw.Elapsed}");
        }

        public async void GenerateWithinRange(byte min = 1, byte max = 38)
        {
            List<Pool> pools = new();
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

                                Console.WriteLine($"Gen: #{min} => {Count}");
                                Count++;

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
            }

            Pools.AddRange(pools);
            await Task.CompletedTask;
        }

        #endregion

        #region Tripletas
        private void GenerarPoolTripletaToDB()
        {
            List<Task> tasks = new();
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            for (byte i = 0; i <= 99; i++)
            {
                GenerateWithinRangeTripleta(i, 99);
            }

            var services = new LotoServices();
            int count = 1;
            string table = "tripleta_pool";
            foreach (var pool in TripletaPools)
            {
                TripletaPool? temp = services.GetPool<TripletaPool>(table, count);
                if (temp != null && temp.PoolId == count)
                {
                    Console.WriteLine($"Skipping {count++}");
                    continue;
                }
                services.AddPool(table, pool);
                Console.WriteLine($"Success inserting {count++}");
            }
            Console.WriteLine($"Success in: {sw.Elapsed}");
        }

        private async void GenerateWithinRangeTripleta(byte min = 0, byte max = 38)
        {
            List<TripletaPool> pools = new();
            byte index2 = 0;
            byte index3 = 0;

            for (byte pos2 = (byte)(min + 1 + index2); pos2 <= (byte)(max - 1); pos2++)
            {
                if (min == pos2)
                {
                    continue;
                }

                for (byte pos3 = (byte)(min + 2 + index3); pos3 <= max; pos3++)
                {
                    if (min == pos3 || pos2 == pos3)
                    {
                        continue;
                    }

                    TripletaPool pool = new();

                    pool.PoolId = CountTripleta;
                    pool.Numero1 = min;
                    pool.Numero2 = pos2;
                    pool.Numero3 = pos3;

                    var sorted = pool.SortedList();

                    var found = pools.Any(x => x.SortedList().SequenceEqual(sorted));
                    if (found)
                    {
                        continue;
                    }

                    pools.Add(pool);

                    Console.WriteLine($"Gen: #{min} => {Count}");
                    Count++;
                }
                index3 = 0;
                index2++;
            }

            TripletaPools.AddRange(pools);
            await Task.CompletedTask;
        }

        #endregion
    }
}