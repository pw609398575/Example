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
    public class SingletonThird
    {
        private SingletonThird()
        {
            long lResult = 0;
            for (int i = 0; i < 100 - 000 - 000; i++)
            {
                lResult += i;
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{this.GetType().Name}完成构造....");
        }
        /// <summary>
        /// 静态字段，由CLR调用，在类型第一次被使用前初始化，且只初始化一次！
        /// </summary>
        private static SingletonThird Instance = new SingletonThird();
       
        public static SingletonThird CreateInstance()
        {
            
            return Instance;
        }

        public static void DoNothing()
        {
            Console.WriteLine("DoNothing");
        }

        public int iNum = 0;
        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name} Show..{iNum++}..");
        }

        public void Add()
        {
            this.iNum++;
        }
    }
}
