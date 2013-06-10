/************************************
 * Copyright (c) Plume.studio
 * Creater : Soul
 * Createtime : 2011/04/30
 * 
 * Filename : RandomShuffle.cs
 * Description :
 * 一个产生随机数、随机数序列的类
 *************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ORA.Utils
{
    public sealed class RandomShuffle
    {
        /// <summary>
        /// 产生随机数列（随机性弱）
        /// </summary>
        /// <param name="size">随机序列长度</param>
        static public List<int> Shuffle(int size)
        {
            List<int> list = new List<int>(size);
            for (int i = 0; i < size; i++ )
            {
                list.Insert(i, i);
            }
            System.Random random = new Random();

            for (int i = 0; i < list.Count; i++ )
            {
                int var = random.Next(0, list.Count);
                int temp = list[i];
                list[i] = list[var];
                list[var] = temp;
            }

            return list;
        }

        /// <summary>
        /// 产生加密强随机数列
        /// </summary>
        /// <param name="size">随机序列长度</param>
        static public List<int> ShuffleEx(int size)
        {
            List<int> list = new List<int>(size);

            for (int i = 0; i < list.Count; i++ )
            {
                list.Insert(i, i);
            }

            System.Random random = new Random(new RNGCryptoServiceProvider().GetHashCode());

            for (int i = 0; i < list.Count; i++)
            {
                int var = random.Next(0, list.Count);
                int temp = list[i];
                list[i] = list[var];
                list[var] = temp;
            }

            return list;
        }

        /// <summary>
        /// 经过加密的强随机值序列填充字节数组
        /// 并且用字节数组构造INT型整数后求模
        /// </summary>
        /// <param name="size">随机序列长度</param>
        static public List<int> ShuffleProSpecial(int size)
        {
            // 长度拓充两位
            List<int> list = new List<int>(size + 2);

            for (int i = 0; i < size + 2; i++)
            {
                list.Insert(i, i);
            }

            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            for (int i = 0; i < list.Count; i++)
            {
                rng.GetNonZeroBytes(randomBytes);
                int randomSeed = (randomBytes[0] << 24) |
                                 (randomBytes[1] << 16) |
                                 (randomBytes[2] << 8) |
                                 (randomBytes[3]);
                int var = randomSeed % list.Count;
                var = (var < 0) ? (-var) : var;

                int temp = list[i];
                list[i] = list[var];
                list[var] = temp;
            }
            return list;
        }

        static public List<int> ShufflePro(int size)
        {
            List<int> list = new List<int>(size);

            for (int i = 0; i < size; i++)
            {
                list.Insert(i, i);
            }

            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            for (int i = 0; i < list.Count; i++)
            {
                rng.GetNonZeroBytes(randomBytes);
                int randomSeed = (randomBytes[0] << 24) |
                                 (randomBytes[1] << 16) |
                                 (randomBytes[2] << 8) |
                                 (randomBytes[3]);
                int var = randomSeed % list.Count;
                var = (var < 0) ? (-var) : var;

                int temp = list[i];
                list[i] = list[var];
                list[var] = temp;
            }

            return list;
        }

        /// <summary>
        /// 产生0~1
        /// </summary>
        /// <returns></returns>
        static public double RandDouble()
        {
            //int seed = (int)System.DateTime.Now.ToBinary();
            //return (float)(low + new System.Random(seed).NextDouble() * (upper - low));

            decimal _base = (decimal)long.MaxValue;
            byte[] rndSeries = new byte[8];
            System.Security.Cryptography.RNGCryptoServiceProvider rng
                = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(rndSeries);
            //不含100需去掉+1 
            return (double)(Math.Abs(BitConverter.ToInt64(rndSeries, 0)) / _base);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public int RandInt(int low, int upper)
        {
            int seed = (int)System.DateTime.Now.ToBinary();
            return new System.Random(seed).Next(low, upper);
        }
    }
}
