using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 多线程test
{
    public class test
    {
        /// <summary>
        /// 线程总数
        /// </summary>
        private int threadNum = 4;

        /// <summary>
        /// 总数
        /// </summary>
        private int totalCount = 0;

        /// <summary>
        /// 已处理
        /// </summary>
        private int index = 0;

        /// <summary>
        /// 队列
        /// </summary>
        private ConcurrentQueue<AssetRepayment> queues = new ConcurrentQueue<AssetRepayment>();

        public void SubDeTransaction()
        {
            var list = new List<AssetRepayment>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(new AssetRepayment() { Title = i.ToString() + "---" + Guid.NewGuid().ToString() });
            }

            if (list == null || list.Count() == 0)
            {
                Console.WriteLine("没有可执行的数据");
                return;
            }
            totalCount = list.Count;
            Console.WriteLine("可执行的数据:" + list.Count() + "条");
            foreach (var item in list)
            {
                queues.Enqueue(item);
            }
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < threadNum; i++)
            {
                var task = Task.Run(() =>
                {
                    Process();
                });
                tasks.Add(task);
            }
            var taskList = Task.Factory.ContinueWhenAll(tasks.ToArray(), (ts) =>
            {
            });
            taskList.Wait();
        }

        private void Process()
        {
            while (true)
            {
                var currentIndex = Interlocked.Increment(ref index);
                AssetRepayment repayId = null;
                var isExit = queues.TryDequeue(out repayId);
                if (!isExit)
                {
                    break;
                }
                try
                {
                    Console.WriteLine(repayId.Title);

                    Console.WriteLine(string.Format(" 共{0}条 当前第{1}条", totalCount, currentIndex));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }

    public class AssetRepayment
    {
        public string Title { get; set; }
    }
}