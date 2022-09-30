using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.SingletonPattern
{
    /// <summary>
    /// 单例类
    /// </summary>
    public class Singleton
    {
        private Singleton()
        {
            long lResult = 0;
            for (int i = 0; i < 100 - 000 - 000; i++)
            {
                lResult += i;
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{this.GetType().Name}完成构造....");
        }
        private static Singleton Instance = null;
        private static readonly object Singleton_Lock = new object();
        public static Singleton CreateInstance()
        {
            if (Instance == null)
            {
                lock (Singleton_Lock)//保证方法块儿只有一个线程可以进入
                {
                    Console.WriteLine("进入lock排队....");
                    Thread.Sleep(1000);
                    if (Instance == null)
                        Instance = new Singleton();
                }
            }
            return Instance;
        }

        public static void DoNothing()
        {
            Console.WriteLine("DoNothing");
        }

        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name} Show....");
        }
    }
}
