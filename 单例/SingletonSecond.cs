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
    public class SingletonSecond
    {
        private SingletonSecond()
        {
            long lResult = 0;
            for (int i = 0; i < 100 - 000 - 000; i++)
            {
                lResult += i;
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{this.GetType().Name}完成构造....");
        }
        private static SingletonSecond Instance = null;
        /// <summary>
        /// 静态构造函数，由CLR调用，在类型第一次被使用前调用，且只调用一次！
        /// </summary>
        static SingletonSecond()
        {
            Instance = new SingletonSecond();
        }
        public static SingletonSecond CreateInstance()
        {
            
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
