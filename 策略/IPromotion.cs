using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 策略
{
    public interface IPromotion
    {
        /// <summary>
        /// 根据原价和策略计算新价格
        /// </summary>
        /// <param name="originPrice">原价</param>
        /// <returns></returns>
        double GetPrice(double originPrice);
    }

    public class Discount : IPromotion
    {
        public double GetPrice(double originPrice)
        {
            Console.WriteLine("打八折:");
            return originPrice * 0.8;
        }
    }

    public class MoneyBack : IPromotion
    {
        public double GetPrice(double originPrice)
        {
            Console.WriteLine("满100返50");
            return originPrice - (int)originPrice / 100 * 50;
        }
    }

    public class PromotionContext
    {
        private IPromotion p = null;

        public PromotionContext(IPromotion p)
        {
            this.p = p;
        }

        public double GetPrice(double originPrice)
        {
            // 默认策略
            if (this.p == null)
            {
                this.p = new Discount();
            }
            return this.p.GetPrice(originPrice);
        }

        /// <summary>
        /// 更改策略的方法
        /// </summary>
        /// <param name="p"></param>
        public void ChangePromotion(IPromotion p)
        {
            this.p = p;
        }
    }
}