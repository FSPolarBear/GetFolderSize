using GetFolderSize.util;
using Json;


namespace GetFolderSize
{


    /// <summary>
    /// 
    /// </summary>
    /// 2024.6.14
    /// version 2.0.0
    internal static class SearchUtil
    {
        static string[] unlocalizedSearchRuleNames = {
            "contain",
            "same",
            "regular",
            "wildcard",
            "extension",
        };
        static char unlocalizedColon = ':';
        /// <summary>
        /// 检验文件名或文件夹名是否与搜索内容匹配
        /// </summary>
        /// 2024.5.13
        /// version 2.0.0
        /// <param name="name">待匹配文件名（或文件夹名）</param>
        /// <param name="pattern">匹配内容</param>
        /// <param name="searchRule">匹配方式。Contain：文件名包含搜索内容；Same：文件名与搜索内容相同；Regular：搜索内容为正则表达式，文件名匹配此正则表达式;
        /// </param>
        /// <param name="caseSensitive">是否大小写敏感</param>
        /// <returns>若文件名匹配搜索内容则返回true，否则返回false</returns>
        public static bool Match(string name, string pattern, SearchRules searchRule = SearchRules.Contain, bool caseSensitive = false)
        {
            if (!caseSensitive && searchRule != SearchRules.Regular)
            {
                name = name.ToLower();
                pattern = pattern.ToLower();
            }
            switch (searchRule)
            {
                case SearchRules.Contain:
                    return name.Contains(pattern);
                case SearchRules.Same:
                    return name == pattern;
                case SearchRules.Regular:
                    System.Text.RegularExpressions.RegexOptions option;
                    if (caseSensitive)
                        option = System.Text.RegularExpressions.RegexOptions.None;
                    else
                        option = System.Text.RegularExpressions.RegexOptions.IgnoreCase;
                    System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern, option);
                    System.Text.RegularExpressions.Match match = reg.Match(name);
                    return match.Success;
                case SearchRules.Wildcard:
                    return WildcardUtil.IsMatched(name, pattern);
                case SearchRules.Extension:
                    //包含/或\的字符串（如folder1/1.txt）显然不是有效的扩展名
                    if (pattern.Contains('/') || pattern.Contains("\\"))
                        return false;
                    string extension;
                    if (pattern.StartsWith('.'))
                        extension = pattern;
                    else
                        extension = '.' + pattern;
                    return name.EndsWith(extension);
                default:
                    throw new Exception("Search Rule Exception");
            }
        }

        /// <summary>
        /// 组合搜索时检验文件名或文件夹名是否与搜索内容匹配
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        /// <param name="name">文件名（或文件夹名）</param>
        /// <param name="conditions">条件。每个条件的格式是"匹配方式:匹配内容"，多个条件间使用|分隔。
        /// 例如搜索扩展名为".txt"且包含"abc"的文件，则conditions为"扩展名：.txt|包含：abc"</param>
        /// <param name="caseSensitive"></param>
        /// <param name="localizedSearchRuleNames"></param>
        /// <param name="localizedColon"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool CombinationMatch(string name, string conditions, bool caseSensitive = false,string[]? localizedSearchRuleNames = null, char localizedColon = ':')
        {
            if (localizedSearchRuleNames == null)
                localizedSearchRuleNames = unlocalizedSearchRuleNames;
            if (localizedSearchRuleNames.Length != unlocalizedSearchRuleNames.Length) // 正常调用时此处应相等
                throw new Exception("Number of localized search rule names is not correct.");

            int searchRuleLength = localizedSearchRuleNames.Length; // 搜索规则（不含组合搜索）的数量

            string[] singleConditions = conditions.Split('|');
            foreach(string singlePattern in singleConditions)
            {          
                // 冒号前的是匹配方式，冒号后是匹配内容
                int pos_colon = IndexOfColon(singlePattern, localizedColon);
                if (pos_colon == -1)
                    throw new Exception("The string of conditions is not correct.");

                // 分析匹配方式
                string searchRuleName = singlePattern.Substring(0, pos_colon).Trim().ToLower();
                string pattern = singlePattern.Substring(pos_colon + 1);
                SearchRules rule = SearchRules.Contain;
                bool matched_rule = false;
                for (int i = 0; i < searchRuleLength; i++)
                {
                    // 条件中的匹配方式可以使用当前语言或英语。例如当前语言是简体中文时，条件"包含：abc"和"Contain:abc"均表示匹配包含"abc"的字符串。
                    if (searchRuleName == unlocalizedSearchRuleNames[i].ToLower() || searchRuleName == localizedSearchRuleNames[i].ToLower())
                    {
                        rule = (SearchRules)i;
                        matched_rule = true;
                        break;
                    }
                }
                if (!matched_rule)
                    throw new Exception("Search rule is not correct.");

                // 所有条件均满足时才能成功匹配，因此任意条件不满足则匹配失败
                if (!Match(name, pattern, rule, caseSensitive))
                    return false;
            }
            // 所有条件均满足时成功匹配
            return true;
        }

        /// <summary>
        /// 寻找字符串中的首个冒号的位置，包括当前语言的冒号(如简体中文的：)和英语的冒号(:)。
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        /// <param name="str">需要寻找冒号的字符串</param>
        /// <param name="localizedColon">当前语言的冒号</param>
        /// <returns></returns>
        private static int IndexOfColon(string str, char localizedColon = ':')
        {
            // 若当前语言的冒号与英语冒号相同，直接寻找其位置
            if (localizedColon == unlocalizedColon) return str.IndexOf(localizedColon);

            // 若仅找到当前语言的冒号或仅找到英语的冒号，则直接返回其位置。若二者均被找到，则返回位置较前者的位置
            int index_localizedColon = str.IndexOf(localizedColon);
            int index_unlocalizedColon = str.IndexOf(unlocalizedColon);
            if (index_localizedColon == -1)
                return index_unlocalizedColon;
            else if (index_unlocalizedColon == -1)
                return index_localizedColon;
            else 
                return Math.Min(index_localizedColon, index_unlocalizedColon);
        }
    }

    /// <summary>
    /// 搜索规则
    /// </summary>
    /// 2024.5.19
    /// version 2.0.0
    internal enum SearchRules
    {
        Contain,    // 包含
        Same,       // 相同
        Regular,    // 正则
        Wildcard,   // 通配符
        Extension,  // 文件扩展名
        Combination, // 组合条件
    }

    /// <summary>
    /// 搜索时的参数
    /// </summary>
    /// 2024.5.19
    /// version 2.0.0
    internal class SearchArgs
    {
        // 搜索内容
        public string str { get; private set; }

        // 匹配方式
        public SearchRules searchRule { get; private set; }

        // 是否搜索文件
        public bool searchFile { get; private set; }

        // 是否搜索文件夹
        public bool searchFolder { get; private set; }

        // 是否递归搜索。若为true，则在此文件夹及其子文件夹进行递归搜索；若为false，则仅在此文件夹搜索，不在子文件夹搜索
        public bool recursiveSearch { get; private set; }

        // 是否区分大小写
        public bool caseSensitive { get; private set; }

        // 是否匹配全路径
        public bool matchFullName { get; private set; }

        // 文件的大小下限。为null则不设下限
        public long? fileSizeLowerLimit { get; private set; }

        // 文件的大小上限。为null则不设上限
        public long? fileSizeUpperLimit { get; private set; }

        // 文件夹的大小下限。为null则不设下限
        public long? folderSizeLowerLimit { get; private set; }

        // 文件夹的大小上限。为null则不设上限
        public long? folderSizeUpperLimit { get; private set; }

        // 文件夹中文件数量下限。为null则不设下限
        public int? fileCountLowerLimit { get; private set; }

        // 文件夹中文件数量上限。为null则不设上限
        public int? fileCountUpperLimit { get; private set; }

        public string[]? localizedSearchRuleNames { get; private set; }
        public char localizedColon { get; private set; }

        public SearchArgs(JsonObject jobj)
        {
            str = jobj.Get<string>("str", "");
            searchRule = (SearchRules)jobj.Get<int>("searchRule", (int)SearchRules.Contain);
            searchFile = jobj.Get<bool>("searchFile", true);
            searchFolder = jobj.Get<bool>("searchFolder", true);
            recursiveSearch = jobj.Get<bool>("recursiveSearch", true);
            caseSensitive = jobj.Get<bool>("caseSensitive", false);
            matchFullName = jobj.Get<bool>("matchFullName", false);
            fileSizeLowerLimit = jobj.Get<long?>("fileSizeLowerLimit", null);
            fileSizeUpperLimit = jobj.Get<long?>("fileSizeUpperLimit", null);
            folderSizeLowerLimit = jobj.Get<long?>("folderSizeLowerLimit", null);
            folderSizeUpperLimit = jobj.Get<long?>("folderSizeUpperLimit", null);
            fileCountLowerLimit = jobj.Get<int?>("fileCountLowerLimit", null);
            fileCountUpperLimit = jobj.Get<int?>("fileCountUpperLimit", null);
            localizedSearchRuleNames = jobj.Get<JsonArray?>("localizedSearchRuleNames", null)?.ToArray<string>();
            localizedColon = jobj.Get<char>("localizedColon", ':');
        }
    }
}
