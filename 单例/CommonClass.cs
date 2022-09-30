using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.SingletonPattern
{
    /// <summary>
    /// 普通类，耗时好资源
    /// </summary>
    public class CommonClass
    {
        public CommonClass()
        {
            long lResult = 0;
            for (int i = 0; i < 100 - 000 - 000; i++)
            {
                lResult += i;
            }
            Thread.Sleep(2000);
            Console.WriteLine($"{this.GetType().Name}完成构造....");
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
