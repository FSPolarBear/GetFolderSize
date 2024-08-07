﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 
    /// </summary>
    /// 2023.12.13
    /// version 1.3.1
    internal static class RandomUtil
    {
        private static string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        /// <summary>
        /// 生成一个由字母和数字构成的指定长度的随机字符串
        /// </summary>
        /// 2023.12.13
        /// version 1.3.1
        /// <param name="lenth">指定的长度</param>
        /// <returns></returns>
        public static string RandomString(int lenth=10)
        {
            Random random = new Random();
            char[] result = new char[lenth];
            for (int i = 0; i < lenth; i++)
            {
                result[i] = ALPHABET[random.Next(ALPHABET.Length)];
            }
            return new string(result);
        }
    }
}
