using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize.util
{
    /// <summary>
    /// 
    /// </summary>
    /// 2024.6.14
    /// version 2.0.0
    internal static class WildcardUtil
    {

        /// <summary>
        /// 判断一个字符串是不是有效的通配符字符串
        /// </summary>
        /// 2024.6.14
        /// version 2.0.0
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool CheckValidity(string pattern)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                // `是转义字符，跳过下一个字符。如果`是最后一个字符，则通配符无效。
                if (pattern[i] == '`')
                {
                    if (i + 1 < pattern.Length)
                        i++;
                    else
                        return false;
                }

                else if (pattern[i] == '[')
                {
                    // 包含空[]的通配符无效
                    if (i + 1 < pattern.Length && pattern[i+1] == ']' && (i+2 >= pattern.Length || pattern[i+2] != ']'))
                        return false;

                    int start = i + 1;

                    // 找到]的位置
                    for (; i < pattern.Length && pattern[i] != ']'; i++) ;
                    if (i + 1 < pattern.Length && pattern[i + 1] == ']')
                        i++;
                    
                    // 如果没有]则通配符无效
                    if ( i >= pattern.Length )
                        return false;

                    // 如果其中包含错误的范围，则通配符无效。尽管此处调用ProcessRange的字符串包含了]和表示排除的!或^(匹配通配符时调用ProcessRange不应包含这些字符)，但不会影响返回值是否是null的结果，因此无需额外处理去掉这些字符。
                    if (ProcessRange(pattern.Substring(start, i - start)) == null)
                        return false;
                }
            }
            return true;

        }

        /// <summary>
        /// 判断一个字符串是否与通配符匹配
        /// </summary>
        /// 2024.6.7
        /// version 2.0.0
        /// <param name="input">待判断的字符串</param>
        /// <param name="pattern">通配符</param>
        /// <param name="i_input">input开始的位置</param>
        /// <param name="i_pattern">pattern开始的位置</param>
        /// <returns>若匹配成功则返回true，否则返回false。pattern不正确也返回false。</returns>
        private static bool _IsMatched(string input, string pattern, int i_input = 0, int i_pattern = 0)
        {

            // 遍历通配符
            while( i_pattern < pattern.Length )
            {
                // 若通配符遍历完成前待判断字符串遍历完成，且通配符剩余字符不全为*，则匹配失败
                if (i_input >= input.Length && pattern[i_pattern] != '*')
                    return false;

                // *匹配任意数量的任意字符
                if (pattern[i_pattern] == '*')
                {
                    // 因为*匹配任意数量任意字符，因此多个*与一个*无区别，若遇到多个*则只考虑最后一个
                    while (i_pattern < pattern.Length && pattern[i_pattern] == '*')
                        i_pattern++;
                    // 若位于通配符最后的*前所有内容均匹配成功，则匹配成功
                    if (i_pattern == pattern.Length)
                        return true;
                    // 对于通配符中间的*，判断待判断字符串在i_input后是否存在一个字串匹配*后的内容。若存在则匹配成功，否则匹配失败
                    for(;i_input < input.Length;i_input++)
                    {
                        // 此时i_pattern是*的索引+1
                        if (_IsMatched(input, pattern, i_input, i_pattern))
                            return true;
                    }
                    return false;
                }
                // ?匹配任意字符
                else if (pattern[i_pattern] == '?')
                { 
                    i_input++;
                }
                // 匹配[]中的一个字符。例如：[abc]匹配a或b或c。[]中的特殊字符作为普通字符进行匹配，例如[*]匹配字符*
                // 若[后的第一个字符是!则表示匹配不在[]中的一个字符，例如[!abc]匹配除a、b、c以外的任意字符。
                // 可以使用范围表示字母或数字。例如[a-z]匹配a至z之间的任意字母，[3-7]匹配3至7之间的任意数字。
                // 转义的]只能放在表示[]结束的]前。例如匹配a或]，应使用[a]]而非[]a]。
                // 若[]中除转义的!还有其他元素，!和^应放在非第一个字符。例如匹配a或!，应使用[a!]而非[!a]。
                // 空括号[]无效。
                else if (pattern[i_pattern] == '[')
                {
                    if (i_pattern + 1 < pattern.Length && pattern[i_pattern + 1] == ']' && (i_pattern + 2 >= pattern.Length || pattern[i_pattern + 2] != ']'))
                        return false;

                    // [!]匹配!，[^]匹配^
                    if (i_pattern+2 < pattern.Length && (pattern[i_pattern + 1] == '!' || pattern[i_pattern + 1] == '^') && pattern[i_pattern+2] == ']' && (i_pattern+3 >= pattern.Length || pattern[i_pattern+3] != ']'))
                    {
                        if (input[i_input] == pattern[i_pattern + 1])
                        {
                            i_input++;
                            i_pattern+=3;
                            continue;
                        }  
                        else
                            return false;
                    }
                    bool exclude = false; // 若[后的第一个字符为!，则表示匹配除[]中字符外的字符
                    int start = i_input+1;
                    // pattern不正确，返回false
                    if (start >= pattern.Length)
                        return false;
                    // 若[后的第一个字符为!或^，则表示匹配除[]中字符外的字符
                    else if (pattern[start] == '!' || pattern[start] == '^')
                    {
                        exclude = true;
                        start++;
                    }

                    int end = start;
                    // 完成时end应为表示[]结束的]的位置
                    while (pattern[end] != ']')
                    {
                        end++;
                        // 不封闭的[]，pattern不正确，返回false
                        if (end >= pattern.Length)
                            return false;
                    }
                    // 处理转义的]
                    if (end + 1 < pattern.Length && pattern[end + 1] == ']')
                        end++;
                    // 展开范围(如"A-F" -> "ABCDEF")
                    string? chars = ProcessRange(pattern.Substring(start, end - start));
                    if (chars != null)
                    {
                        if ((exclude && !chars.Contains(input[i_input])) || (!exclude && chars.Contains(input[i_input])))
                        {
                            i_input++;
                            // end是]的位置，start是[后(不含表示排除的!)下一个字符的位置，除end和start之间的end-start个字符外还需要跳过[，因此期间需要跳过end - start + 1个字符
                            i_pattern += end - start + 1;
                            // 如果存在表示排除的!，跳过!
                            if (exclude)
                                i_pattern++;
                        }
                        else 
                            return false;
                    }
                    else
                        return false;
                }
                // `是转义字符，其后的一个字符作为普通字符进行匹配，例如`*匹配字符*，而非任意字符串。
                else if (pattern[i_pattern] == '`')
                {
                    if(i_pattern +1 < pattern.Length)
                    {
                        if (pattern[i_pattern + 1] == input[i_input])
                        {
                            i_input++;
                            i_pattern++;
                        }
                        else
                            return false;
                    }
                    else // 转义字符是最后一个字符时pattern不正确
                        return false;
                }
                // 普通字符，检验是否与待匹配字符串的对应字符相同
                else
                {
                    if (pattern[i_pattern] == input[i_input])
                        i_input++;
                    else
                        return false;
                }

                i_pattern++;
            }
            //若完成遍历通配符时刚好完成遍历待判断字符串，则匹配成功。
            //若完成遍历通配符时仍未完成遍历待判断字符串，则匹配失败。
            return i_input == input.Length;
        }


        /// <summary>
        /// 判断一个字符串是否与通配符匹配
        /// </summary>
        /// 2024.5.13
        /// version 2.0.0
        /// <param name="input">待判断的字符串</param>
        /// <param name="pattern">通配符</param>
        /// <returns>若匹配成功则返回true，否则返回false。pattern不正确也返回false。</returns>
        public static bool IsMatched(string input, string pattern)
        {
            return _IsMatched(input, pattern);
        }

        static readonly string UPPER = "ABCEDFGHIJKLMNOPQRSTUVWXYZ";
        static readonly string LOWER = "abcdefghijklmnopqrstuvwxyz";
        static readonly string NUMBER = "0123456789";

        /// <summary>
        /// 给定可能包含范围的字符串，将范围展开，其他部分不变。
        /// 例如，输入"a-f"，返回"abcdef"。输入"abc0-7efA-Gc"，返回"abc01234567efABCDEFGc"。
        /// 范围的两端应均为大写字母，或均为小写字母，或均为数字。
        /// 若-位于字符串头尾，或-前后的字符种类不同，或-后的字符排不在-前的字符之后(如"9-0")，则返回null
        /// </summary>
        /// 2024.5.13
        /// version 2.0.0
        /// <param name="string_with_range"></param>
        /// <returns></returns>
        private static string? ProcessRange(string string_with_range)
        {
            StringBuilder result = new StringBuilder();
            int pos = string_with_range.IndexOf('-');
            int last_pos = -1;
            while (pos >= 0)
            {
                result.Append(string_with_range.Substring(last_pos+1, pos - last_pos-1));
                last_pos = pos;
                if (pos == 0 || pos == string_with_range.Length - 1)
                    return null;

                char start = string_with_range[pos-1], end = string_with_range[pos+1];
                if (UPPER.Contains(start) && UPPER.Contains(end))
                {
                    int pos_start = UPPER.IndexOf(start);
                    int pos_end = UPPER.IndexOf(end);
                    if (pos_start >= pos_end)
                        return null;
                    result.Append(UPPER.Substring(pos_start, pos_end - pos_start + 1));
                }
                else if (LOWER.Contains(start) && LOWER.Contains(end))
                {
                    int pos_start = LOWER.IndexOf(start);
                    int pos_end = LOWER.IndexOf(end);
                    if (pos_start >= pos_end)
                        return null;
                    result.Append(LOWER.Substring(pos_start, pos_end - pos_start + 1));
                }
                else if (NUMBER.Contains(start) && NUMBER.Contains(end))
                {
                    int pos_start = NUMBER.IndexOf(start);
                    int pos_end = NUMBER.IndexOf(end);
                    if (pos_start >= pos_end)
                        return null;
                    result.Append(NUMBER.Substring(pos_start, pos_end - pos_start + 1));
                }
                else 
                    return null;

                pos = string_with_range.IndexOf('-', pos+1);
            }
            result.Append(string_with_range.Substring(last_pos + 1));
            return result.ToString();
        }
    }
}
