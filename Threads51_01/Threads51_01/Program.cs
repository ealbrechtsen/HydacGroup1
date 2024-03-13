namespace Threads51_01
{
    class Program
    {
        private static object _myLock = new object();
        private char _sharedChar;
        private const int SIMULATE_WORK = 100;


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }


        public void Run()
        {
            Thread tA = new Thread(WriteA);
            Thread tB = new Thread(WriteB);
            tA.Name = "Thread A";
            tB.Name = "Thread B";
            tA.Start();
            tB.Start();
            tA.Join();
            tB.Join();
            Console.Write("\nPress a key ....");
            Console.ReadKey();
        }


        private void WriteA()
        {
            lock (_myLock)
            {
                for (int i = 0; i < 10; i++)
                {
                    _sharedChar = 'A';
                    Thread.Sleep(SIMULATE_WORK);
                    Console.WriteLine($"{Thread.CurrentThread.Name} : {_sharedChar}");
                    Thread.Yield();
                }
            }
        }


        private void WriteB()
        {
            lock (_myLock)
            {
                for (int i = 0; i < 10; i++)
                {
                    _sharedChar = 'B';
                    Thread.Sleep(SIMULATE_WORK);
                    Console.WriteLine($"{Thread.CurrentThread.Name} : {_sharedChar}");
                    Thread.Yield();
                }
            }

        }
    }

}
