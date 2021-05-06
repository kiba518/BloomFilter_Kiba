using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomTest
{
    public class BloomFilter
    {
        //布隆缓存数组
        public BitArray BloomCache;
        //布隆缓存数组的长度
        public Int64 BloomCacheLength { get; } 
        public Int64 HashCount { get; }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bloomCacheLength">布隆缓存数组的长度，默认20000</param> 
        /// <param name="hashCount">hash运算次数，默认3</param>
        public BloomFilter(int bloomCacheLength = 20000,  int hashCount = 3)
        {
            BloomCache = new BitArray(bloomCacheLength);
            BloomCacheLength = bloomCacheLength; 
            HashCount = hashCount;
        }


        public void Add(string str)
        {
            var hashCode =str.GetHashCode();
            Random random = new Random(hashCode);
            for (int i = 0; i < HashCount; i++)
            {
                var x = random.Next((int)(BloomCacheLength - 1));
                BloomCache[x] = true;
            }
        }

        public bool IsExist(string str)
        {
            var hashCode = str.GetHashCode();
            Random random = new Random(hashCode);
            for (int i = 0; i < HashCount; i++)
            {
                if (!BloomCache[random.Next((int)(BloomCacheLength - 1))])
                {
                    return false;
                }
            }
            return true;
        }
 
        //错误率查看
        public double GetFalsePositiveProbability(double setSize)
        {
            // (1 - e^(-k * n / m)) ^ k
            return Math.Pow((1 - Math.Exp(-HashCount * setSize / BloomCacheLength)), HashCount);
        }
        //计算基于布隆过滤器散列的最佳数量，即hashCount的最佳值
        public int OptimalNumberOfHashes(int setSize)
        {
            return (int)Math.Ceiling((BloomCacheLength / setSize) * Math.Log(2.0));
        }
 
    }

  

}
