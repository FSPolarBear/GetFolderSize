using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 快速排序
    /// <para>2022.6.8</para>
    /// <para>version 1.1.0</para>
    /// </summary>
    public class QuickSort
    {
        /// <summary>
        /// 使用自定义的比较函数对数组快速排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <typeparam name="T">需要排序的类型</typeparam>
        /// <param name="t">需要排序的数组</param>
        /// <param name="cmp">比较函数，返回值为负则第一个参数对象排在第二个参数对象之前，为正则第一个参数对象排在第二个参数对象之后，为0则随意</param>
        public static void Sort<T>(T[] t, Func<T,T,int> cmp)
        {
            SortPass<T>(t,cmp,0,t.Length-1);
        }

        /// <summary>
        /// 使用默认比较函数对数组进行快速排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <typeparam name="T">需要排序的类型</typeparam>
        /// <param name="t">需要排序的数组</param>
        public static void Sort<T>(T[] t)
        {
            Sort<T>(t, DefaultCmp<T>);
        }


        /// <summary>
        /// 默认比较函数，以CompareTo作为比较函数。
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <typeparam name="T">需要比较的类型</typeparam>
        /// <param name="t1">需要比较的第一个变量</param>
        /// <param name="t2">需要比较的第二个变量</param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        /// <exception cref="Exception">如果需要排序的值无法比较大小，抛出异常</exception>
        private static int DefaultCmp<T>(T t1, T t2)
        {
            IComparable<T>? t1_icmp = t1 as IComparable<T>;
            if(t1_icmp == null)
                throw new Exception("Values are not compareable.");
            return t1_icmp.CompareTo(t2);
        }


        /// <summary>
        /// 一趟快速排序
        /// 将start和end之间第一个元素作为flag，把所有小于flag的元素放在flag左边，所有大于flag的元素放在flag右边，然后对两侧的部分（如果存在）递归调用此函数
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="cmp"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private static void SortPass<T>(T[] t, Func<T, T, int> cmp, int start, int end)
        {
            if (start >= end)
                return;
            int i=start, j=end;
            T flag = t[start];

            T temp;
            while (i<j)
            {
                while (i < j && cmp(t[j], flag) >= 0)
                    j--;                    
                while (i < j && cmp(t[i], flag) <= 0)
                    i++;
                if(i < j)
                {
                    temp = t[i];
                    t[i] = t[j];
                    t[j] = temp;
                }
            }

            temp = t[i];
            t[i] = t[start];
            t[start] = temp;

            if (start < i-1)
                SortPass<T>(t,cmp, start, i-1);
            if(i+1 < end)
                SortPass<T>(t,cmp, i+1, end);


        }

    }
}
