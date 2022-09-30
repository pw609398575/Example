using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.SingletonPattern
{
    /// <summary>
    /// 环境：VS2019
    ///       .NetFramework4.7.2
    ///       
    /// 1 单例模式(单线程和多线程)
    /// 2 单例模式三种写法
    /// 3 单例模式的优缺点
    /// 4 深度探讨单例模式应用场景
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("大家好，欢迎来到朝夕教育！我是Eleven老师！");
                //{
                //    Console.WriteLine("******************Common*******************");
                //    //CommonClass instance = new CommonClass();//对象复用
                //    //for (int i = 0; i < 3; i++)
                //    //{
                //    //    //CommonClass instance = new CommonClass();
                //    //    instance.Show();
                //    //}

                //    //Show();
                //    //OtherClass.Show();

                //    //CommonClass instance1 = new CommonClass();
                //    //CommonClass instance2 = new CommonClass();
                //    //CommonClass instance3 = new CommonClass();
                //}
                ////强制保证某个类型只有一个实例，这就是单例
                //{
                //    Console.WriteLine("******************Singleton*******************");
                //    //Singleton singleton = new Singleton();
                //    //Singleton singleton = Singleton.CreateInstance();
                //    for (int i = 0; i < 3; i++)
                //    {
                //        Singleton singleton = Singleton.CreateInstance();
                //        singleton.Show();
                //    }
                //    //单线程的单例模式，但是下一步，多线程将引发什么学案？下个视频分晓~
                //}
                //{
                //    Console.WriteLine("******************Thread Singleton*******************");
                //    List<Task> tasks = new List<Task>();
                //    for (int i = 0; i < 3; i++)
                //    {
                //        tasks.Add(Task.Run(() =>//启动线程完成计算，并发执行
                //        {
                //            Singleton singleton = Singleton.CreateInstance();
                //            singleton.Show();
                //        }));
                //    }
                //    Task.WaitAll(tasks.ToArray());//等待全部任务的完成

                //    for (int i = 0; i < 3; i++)
                //    {
                //        tasks.Add(Task.Run(() =>
                //        {
                //            Singleton singleton = Singleton.CreateInstance();
                //            singleton.Show();
                //        }));
                //    }
                //}
                //下次课将带领大家完成更简易的单例实现！
                //{
                //    Console.WriteLine("******************Other Singleton*******************");
                //    //Singleton.DoNothing();
                //    //Singleton singleton = Singleton.CreateInstance();
                //    //singleton.Show();

                //    SingletonSecond.DoNothing();
                //    SingletonThird.DoNothing();
                //    for (int i = 0; i < 3; i++)
                //    {
                //        Task.Run(() =>
                //        {
                //            SingletonSecond singleton = SingletonSecond.CreateInstance();
                //            singleton.Show();

                //        });
                //    }
                //    for (int i = 0; i < 3; i++)
                //    {
                //        Task.Run(() =>
                //        {
                //            SingletonThird singleton = SingletonThird.CreateInstance();
                //            singleton.Show();

                //        });
                //    }
                //}
                //why when
                {
                    CommonClass commonClass1 = new CommonClass();
                    CommonClass commonClass2 = new CommonClass();
                    CommonClass commonClass3 = new CommonClass();
                    //commonClass1.Show();
                    //commonClass2.Show();
                    //commonClass3.Show();

                    for (int i = 0; i < 10_000; i++)
                    {
                        Task.Run(() => {
                            commonClass1.Add();
                        });
                    }

                    SingletonThird singleton1 = SingletonThird.CreateInstance();
                    SingletonThird singleton2 = SingletonThird.CreateInstance();
                    SingletonThird singleton3 = SingletonThird.CreateInstance();

                    //singleton1.Show();
                    //singleton2.Show();
                    //singleton3.Show();
                    for (int i = 0; i < 10_000; i++)
                    {
                        Task.Run(() => {
                            singleton1.Add();
                        });
                    }

                    Thread.Sleep(2000);

                    Console.WriteLine(commonClass1.iNum);
                    Console.WriteLine(singleton1.iNum);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        //public static CommonClass instance = new CommonClass();//对象复用

        private static void Show()
        {
            ////CommonClass instance = new CommonClass();//对象复用
            //for (int i = 0; i < 3; i++)
            //{
            //    //CommonClass instance = new CommonClass();
            //    instance.Show();
            //}
        }
    }
}
