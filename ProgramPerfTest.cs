using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollectionsPerfTest
{
    class ProgramPerfTest
    {
        static LinkedList<BigInteger>[] _bigInts;

        static void Main(string[] args)
        {
            //MemEater();
            Case1();
        }

        public static void MemEater()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            int threadCount = 16;

            _bigInts = new LinkedList<BigInteger>[threadCount];

            for (int i = 0; i < _bigInts.Length; i++)
                _bigInts[i] = new LinkedList<BigInteger>();


            Task[] tasks = new Task[threadCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskInd = i;

                tasks[i] = Task.Run(() =>
                {
                    BigInteger iter = 0;
                    try
                    {

                        while (true)
                        {
                            _bigInts[taskInd].AddLast(iter * threadCount + taskInd);
                            iter++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Job done!");
        }

        public static void Case1()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            int threadCount = 16;

            _bigInts = new LinkedList<BigInteger>[threadCount];

            for (int i = 0; i < _bigInts.Length; i++)
                _bigInts[i] = new LinkedList<BigInteger>();


            Task[] tasks = new Task[threadCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskInd = i;

                tasks[i] = Task.Run(() =>
                {
                    BigInteger iter = 0;
                    try
                    {

                        while (true)
                        {
                            _bigInts[taskInd].AddLast(iter * threadCount + taskInd);
                            iter++;

                            if (_bigInts[taskInd].Count == 1_000_000)                            
                                _bigInts[taskInd].Clear();                            
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Job done!");
        }


        public static void Case2()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            LinkedList<BigInteger>[] bigInts;
            int threadCount = 16;

            bigInts = new LinkedList<BigInteger>[threadCount];

            for (int i = 0; i < bigInts.Length; i++)
                bigInts[i] = new LinkedList<BigInteger>();


            Task[] tasks = new Task[threadCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskInd = i;

                tasks[i] = Task.Run(() =>
                {
                    BigInteger iter = 0;
                    try
                    {

                        while (true)
                        {
                            bigInts[taskInd].AddLast(iter * threadCount + taskInd);
                            iter++;

                            if (bigInts[taskInd].Count == 1_000_000)                            
                                bigInts[taskInd].Clear();                            
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Job done!");
        }

        public static void Case3()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            LinkedList<BigInteger>[] bigInts;
            int threadCount = 16;

            bigInts = new LinkedList<BigInteger>[threadCount];
            
            Task[] tasks = new Task[threadCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskInd = i;

                tasks[i] = Task.Run(() =>
                {
                    bigInts[taskInd] = new LinkedList<BigInteger>();

                    BigInteger iter = 0;
                    try
                    {

                        while (true)
                        {
                            bigInts[taskInd].AddLast(iter * threadCount + taskInd);
                            iter++;

                            if (bigInts[taskInd].Count == 1_000_000)
                                bigInts[taskInd].Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Job done!");
        }

        public static void Case4()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            //LinkedList<BigInteger>[] bigInts;
            int threadCount = 16;

            //bigInts = new LinkedList<BigInteger>[threadCount];

            Task[] tasks = new Task[threadCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskInd = i;

                tasks[i] = Task.Run(() =>
                {
                    var localList = new LinkedList<BigInteger>();

                    BigInteger iter = 0;
                    try
                    {

                        while (true)
                        {
                            localList.AddLast(iter * threadCount + taskInd);
                            iter++;

                            if (localList.Count == 1_000_000)
                                localList.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Job done!");
        }



        public static void Case5()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            ConcurentLinkedList<BigInteger>[] bigInts;
            int threadCount = 16;

            bigInts = new ConcurentLinkedList<BigInteger>[threadCount];

            Task[] tasks = new Task[threadCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskInd = i;

                tasks[i] = Task.Run(() =>
                {
                    bigInts[taskInd] = new ConcurentLinkedList<BigInteger>();

                    BigInteger iter = 0;
                    try
                    {

                        while (true)
                        {
                            bigInts[taskInd].AddLast(iter * threadCount + taskInd);
                            iter++;

                            if (bigInts[taskInd].Count == 1_000_000)
                                bigInts[taskInd].Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Job done!");
        }

        public static void Case6()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            List<ulong>[] bigInts;
            uint threadCount = 16;

            bigInts = new List<ulong>[threadCount];

            Task[] tasks = new Task[threadCount];

            for (uint i = 0; i < tasks.Length; i++)
            {
                uint taskInd = i;

                tasks[i] = Task.Run(() =>
                {
                    bigInts[taskInd] = new List<ulong>(1_000_000);

                    uint iter = 0;
                    try
                    {

                        while (true)
                        {
                            bigInts[taskInd].Add(((ulong)iter) * threadCount + taskInd);
                            iter++;

                            if (bigInts[taskInd].Count == 1_000_000)
                                bigInts[taskInd].Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Job done!");
        }

    }
}
