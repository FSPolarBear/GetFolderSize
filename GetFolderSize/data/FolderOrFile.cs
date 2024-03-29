﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GetFolderSize
{

    /// <summary>
    /// 文件夹或文件。数据项，用于在列表中展示
    /// <para>2023.12.27</para>
    /// <para>version 1.4.1</para>
    /// </summary>
    public class FolderOrFile : IComparable<FolderOrFile>
    {
        
        public static readonly string DATETIME_FORMAT = "yyyy/M/d H:mm:ss";
 
        public bool IsFolder { get; set; } //此项为true表示是文件夹，为false表示是文件
        public string FullName { get; set; } //包含路径的文件名或文件夹名
        public string Name { get; set; } //文件名或文件夹名

        //大小。单位为字节
        public long Size { get; set; }
        
        public FolderOrFile? Parent { get; set; }

        //文件夹内的文件和文件夹
        public FolderOrFile[]? Children { get; set; }

        //文件数
        public int FileCount { get; set; }

        public DateTime? LastWriteTime { get; set; }



        public FolderOrFile()
        {
            IsFolder = false;
            FullName = String.Empty;
            Name = String.Empty;
            Size = 0;
            Parent = null;
            Children = null;
            FileCount = 0;
            LastWriteTime = null;
        }

        /// <summary>
        /// 将文件/文件夹大小转换为合适的单位，并保留小数点后2位
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <returns>转化后的文件/文件夹大小字符串</returns>
        public string SizeFormat()
        {
            string unit = "B";
            double size_d = (double)Size;
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "KB";
            }
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "MB";
            }
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "GB";
            }
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "TB";
            }
            if (unit == "B") // 单位是B的时候不需要表示为小数的形式
                return Size.ToString() + "B";
            else
                return string.Format("{0:f2}{1}", size_d, unit);
        }



        /// <summary>
        /// 依据文件大小，降序排序
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public int CompareTo(FolderOrFile f)
        {
            if (this.Size < f.Size)
                return 1;
            if (this.Size > f.Size)
                return -1;
            return 0;
        }


        /// <summary>
        /// 给定文件夹路径，获取文件夹信息
        /// <para>2023.12.13</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        /// <exception cref="Exception">文件夹不存在</exception>
        public static FolderOrFile GetObjectFromPath(string path)
        {
            if (!Directory.Exists(path))
                throw new Exception("Folder not found");


            //文件夹基本信息
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FolderOrFile f = new FolderOrFile();
            f.FullName = dinfo.FullName;
            f.Name = dinfo.Name;
            f.IsFolder = true;
            f.Size = 0;
            f.FileCount = 0;
            f.LastWriteTime = dinfo.LastWriteTime;
            
            //计算文件夹下文件和文件夹的数量。若出错则为0
            int dir_count = 0, file_count = 0;
            DirectoryInfo[] dirs = null;
            FileInfo[] files = null;
            try
            {
                dirs = dinfo.GetDirectories();
                dir_count = dirs.Length;
                files = dinfo.GetFiles();
                file_count = files.Length;
            }
            catch (Exception)
            {

             }

            f.Children = new FolderOrFile[dir_count + file_count];

            int j = 0;
            //遍历文件夹内的文件
            for (int i = 0; i < file_count; i++)
            {
                f.Children[j] = new FolderOrFile();
                f.Children[j].IsFolder = false;
                f.Children[j].FullName = files[i].FullName;
                f.Children[j].Name = files[i].Name;
                f.Children[j].Size = files[i].Length;
                f.Children[j].FileCount = 1;
                f.Children[j].Parent = f;
                f.Children[j].LastWriteTime = files[i].LastWriteTime;
                j++;
            }

            //遍历文件夹内的文件夹
            for (int i = 0; i < dir_count; i++)
            {
                f.Children[j] = GetObjectFromPath(dirs[i].FullName);
                f.Children[j].Parent = f;
                j++;
            }
            //计算文件夹大小和文件数量
            f.Size = 0;
            for (int i = 0; i < f.Children.Length; i++)
            {
                f.Size += f.Children[i].Size;
                f.FileCount += f.Children[i].FileCount;
            }
                  

            //对Children以Size大小进行排序
            Array.Sort<FolderOrFile>(f.Children);

            return f;
        }

        /// <summary>
        /// 给定多个文件夹路径，获取这些文件夹的信息
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static FolderOrFile GetObjectFromPaths(string[] paths)
        {
            FolderOrFile f = new FolderOrFile();
            f.Name = string.Join("|", paths);
            f.IsFolder = true;
            f.Children = new FolderOrFile[paths.Length];
            for (int i=0;i < paths.Length; i++)
            {
                f.Children[i] = GetObjectFromPath(paths[i]);
                f.FileCount += f.Children[i].FileCount;
                f.Size += f.Children[i].Size;
                f.Children[i].Parent = f;
            }
            return f;
        }

        /// <summary>
        /// 由已有的对象生成json对象
        /// <para>2023.12.13</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <returns>json对象</returns>
        public JObject ToJson()
        {
            JObject jobj = new JObject();
            jobj["IsFolder"] = IsFolder;
            jobj["FullName"] = FullName;
            jobj["Name"] = Name;
            jobj["Size"] = Size;
            jobj["FileCount"] = FileCount;
            if(IsFolder)
            {
                JArray children = new JArray();
                foreach (FolderOrFile child in Children)
                {
                    children.Add(child.ToJson());
                }
                jobj["Children"] = children;
            }
            if (LastWriteTime != null)
            {
                jobj["LastWriteTime"] = LastWriteTime.Value.ToString(DATETIME_FORMAT);
            }
            return jobj;
        }

        /// <summary>
        /// ToString
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
        /// </summary>
        /// <returns>ToJson得到的json的字符串</returns>
        override
        public string ToString()
        {
            return this.ToJson().ToString();
        }


        /// <summary>
        /// 从json对象生成对象
        /// <para>2023.12.13</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="json">json对象</param>
        /// <returns>生成的对象。如果生成失败则为null。</returns>
        public static FolderOrFile? FromJson(JObject jobj)
        {
            try
            {
                //JObject jobj = JObject.Parse(json);
                FolderOrFile f = new FolderOrFile();
                f.IsFolder = (bool)jobj["IsFolder"];
                f.FullName = (string)jobj["FullName"];
                f.Name = (string)jobj["Name"];
                f.Size = (long)jobj["Size"];
                f.FileCount = (int)jobj["FileCount"];
                if(f.IsFolder)
                {
                    JArray children = (JArray)jobj["Children"];
                    f.Children = new FolderOrFile[children.Count];
                    for(int i = 0; i < children.Count; i++)
                    {
                        f.Children[i] = FromJson((JObject)children[i]);
                        f.Children[i].Parent = f;
                    }
                }

                string str_LastWriteTime = JsonUtil.GetValue<string>(jobj, "LastWriteTime", string.Empty);
                if (string.IsNullOrEmpty(str_LastWriteTime))
                {
                    f.LastWriteTime = null;
                }
                else
                {
                    f.LastWriteTime = DateTime.Parse(str_LastWriteTime);
                }

                return f;
            }catch(Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 从json字符串生成对象
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>生成的对象。如果生成失败则为null。</returns>
        public static FolderOrFile? FromJson(string json)//接受字符串作为参数并在函数内转化为JObject对象，而不是直接接受JObject参数。这是为了避免在调用此函数时需要写一个try-catch以将字符串转化JObject对象
        {
            try
            {
                JObject jobj = JObject.Parse(json);
                return FromJson(jobj);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 检验文件名或文件夹名是否与搜索内容匹配
        /// <para>2023.12.7</para>
        /// <para>version 1.3.0</para>
        /// </summary>
        /// <param name="name">文件名（或文件夹名）</param>
        /// <param name="str">搜索内容</param>
        /// <param name="searchRule">匹配方式。include：文件名包含搜索内容；same：文件名与搜索内容相同；regular：搜索内容为正则表达式，文件名匹配此正则表达式</param>
        /// <returns>若文件名匹配搜索内容则返回true，否则返回false</returns>
        private static bool _Match(string name, string str, SearchRules searchRule = SearchRules.Include)
        {
            switch (searchRule)
            {
                case SearchRules.Include:
                    return name.Contains(str);
                    break;
                case SearchRules.Same:
                    return name == str;
                    break;
                case SearchRules.Regular:
                    System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(str);
                    System.Text.RegularExpressions.Match match = reg.Match(name);
                    return match.Success;
                    break;
                default:
                    throw new Exception("Search Rule Exception");

            }
        }

        /// <summary>
        /// 将正则表达式转化为小写
        /// \B, \D, \S等转义字符不转化
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="regex"></param>
        /// <returns></returns>
        private static string RegexToLower(string regex)
        {
            StringBuilder builder = new StringBuilder(regex);
            for(int i = 0; i < builder.Length; i++)
            {
                if (builder[i] == '\\')//跳过反斜杠后的一个字符
                {
                    i++;
                    continue;
                }
                if (builder[i] >= 'A' && builder[i] <= 'Z')
                {
                    builder[i] = Char.ToLower(builder[i]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// 搜索此文件夹下名字匹配的文件或文件夹，并返回查找到的内容列表。用于递归
        /// <para>2023.12.27</para>
        /// <para>version 1.4.1</para>
        /// </summary>
        /// <param name="str">搜索内容</param>
        /// <param name="searchRule">匹配方式。include：文件名包含搜索内容；same：文件名与搜索内容相同；regular：搜索内容为正则表达式，文件名匹配此正则表达式</param>
        /// <param name="searchFile">是否搜索文件</param>
        /// <param name="searchFolder">是否搜索文件夹</param>
        /// <param name="recursiveSearch">是否递归搜索。若为true，则在此文件夹及其子文件夹进行递归搜索；若为false，则仅在此文件夹搜索，不在子文件夹搜索</param>
        /// <param name="caseSensitive">是否区分大小写</param>
        /// <param name="fileSizeLowerLimit">文件的大小下限。为null则不设下限</param>
        /// <param name="fileSizeUpperLimit">文件的大小上限。为null则不设上限</param>
        /// <param name="folderSizeLowerLimit">文件夹的大小下限。为null则不设下限</param>
        /// <param name="folderSizeUpperLimit">文件夹的大小上限。为null则不设上限</param>
        /// <param name="fileCountLowerLimit">文件夹中文件数量下限。为null则不设下限</param>
        /// <param name="fileCountUpperLimit">文件夹中文件数量上限。为null则不设上限</param>
        /// <returns>查找到的内容列表</returns>
        private HashSet<FolderOrFile> _Search(string str, SearchRules searchRule = SearchRules.Include, bool searchFile = true, bool searchFolder = true, bool recursiveSearch = true, bool caseSensitive = false, long? fileSizeLowerLimit = null, long? fileSizeUpperLimit = null, long? folderSizeLowerLimit = null, long? folderSizeUpperLimit = null, int? fileCountLowerLimit = null, int? fileCountUpperLimit = null)
        {
            if (!this.IsFolder || this.Children == null)  // 仅对文件夹进行搜索。如果是文件则报错
                throw new Exception("Search in a non-folder object.");

            //List<FolderOrFile> result = new List<FolderOrFile> ();
            HashSet<FolderOrFile> result = new HashSet<FolderOrFile>();

            if (!caseSensitive)
            {
                if (searchRule == SearchRules.Regular)
                    str = RegexToLower(str);
                else
                    str = str.ToLower();
            }

            for(int i=0;i<this.Children.Length;i++)
            {
                FolderOrFile child = this.Children[i];



                if ((searchFile && !child.IsFolder) || (searchFolder && child.IsFolder))
                {
                    string name = child.Name;
                    if (!caseSensitive)
                        name = name.ToLower();



                    //检查是否符合文件、文件夹大小要求
                    if ((!child.IsFolder && child.InLimitedSize(fileSizeLowerLimit, fileSizeUpperLimit)) || (child.IsFolder && child.InLimitedSize(folderSizeLowerLimit, folderSizeUpperLimit)))
                    {
                        // 检查文件夹是否符合文件数量要求
                        if (!child.IsFolder || child.InLimitedFileCount(fileCountLowerLimit, fileCountUpperLimit))
                        //if (true)
                        {
                            // 如果匹配则加入结果中
                            if (_Match(name, str, searchRule))
                            {
                                result.Add(child);
                            }
                        }

                    }

                }

                if (recursiveSearch && child.IsFolder)  // 递归查询
                {
                    //result.AddRange(child._Search(str, searchRule, searchFile, searchFolder, recursiveSearch, caseSensitive, fileSizeLowerLimit, fileSizeUpperLimit, folderSizeLowerLimit, folderSizeUpperLimit, fileCountLowerLimit, fileCountUpperLimit));
                    result.UnionWith(child._Search(str, searchRule, searchFile, searchFolder, recursiveSearch, caseSensitive, fileSizeLowerLimit, fileSizeUpperLimit, folderSizeLowerLimit, folderSizeUpperLimit, fileCountLowerLimit, fileCountUpperLimit));
                }
            }
            return result;

        }

        /// <summary>
        /// 搜索此文件夹下名字匹配的文件或文件夹，并返回一个包含查找内容的文件夹对象
        /// <para>2023.12.27</para>
        /// <para>version 1.4.1</para>
        /// </summary>
        /// <param name="str">搜索内容</param>
        /// <param name="searchRule">匹配方式。include：文件名包含搜索内容；same：文件名与搜索内容相同；regular：搜索内容为正则表达式，文件名匹配此正则表达式</param>
        /// <param name="searchFile">是否搜索文件</param>
        /// <param name="searchFolder">是否搜索文件夹</param>
        /// <param name="recursiveSearch">是否递归搜索。若为true，则在此文件夹及其子文件夹进行递归搜索；若为false，则仅在此文件夹搜索，不在子文件夹搜索</param>
        /// <param name="caseSensitive">是否区分大小写</param>
        /// <param name="fileSizeLowerLimit">文件的大小下限。为null则不设下限</param>
        /// <param name="fileSizeUpperLimit">文件的大小上限。为null则不设上限</param>
        /// <param name="folderSizeLowerLimit">文件夹的大小下限。为null则不设下限</param>
        /// <param name="folderSizeUpperLimit">文件夹的大小上限。为null则不设上限</param>
        /// <param name="fileCountLowerLimit">文件夹中文件数量下限。为null则不设下限</param>
        /// <param name="fileCountUpperLimit">文件夹中文件数量上限。为null则不设上限</param>
        /// <returns>包含查找内容的文件夹对象</returns>
        public FolderOrFile Search(string str, SearchRules searchRule = SearchRules.Include, bool searchFile=true, bool searchFolder = true, bool recursiveSearch=true, bool caseSensitive=false, long? fileSizeLowerLimit = null, long? fileSizeUpperLimit = null, long? folderSizeLowerLimit = null, long? folderSizeUpperLimit = null, int? fileCountLowerLimit = null, int? fileCountUpperLimit = null)
        {
            if (!this.IsFolder || this.Children == null)  // 仅对文件夹进行搜索。如果是文件则报错
                throw new Exception("Search in a non-folder object.");
            FolderOrFile result = new FolderOrFile();
            result.IsFolder = true;
            result.Name = "Search result";
            result.Children = _Search(str, searchRule, searchFile, searchFolder, recursiveSearch, caseSensitive, fileSizeLowerLimit, fileSizeUpperLimit, folderSizeLowerLimit, folderSizeUpperLimit, fileCountLowerLimit, fileCountUpperLimit).ToArray();
            result.FileCount = result.Children.Length;
            Array.Sort<FolderOrFile>(result.Children);  // 对搜索的结果按大小进行排序

            //添加一个父节点，以显示查找到的文件总数
            FolderOrFile root = new FolderOrFile();
            root.Children = new FolderOrFile[] {result};
            result.Parent = root;
            root.IsFolder = true;//修复在root页面进行搜索时报错的bug
            return result;
        }

        /// <summary>
        /// 搜索此文件夹下名字匹配的文件或文件夹，并返回一个包含查找内容的文件夹对象
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="json">包含搜索内容和搜索配置的json字符串</param>
        /// <returns></returns>
        public FolderOrFile Search(string json)
        {
            JObject jobj = JObject.Parse(json);
            string str = JsonUtil.GetValue<string>(jobj, "str", "");
            SearchRules searchRule = (SearchRules)JsonUtil.GetValue<int>(jobj, "searchRule", (int)SearchRules.Include);
            bool searchFile = JsonUtil.GetValue<bool>(jobj, "searchFile", true);
            bool searchFolder = JsonUtil.GetValue<bool>(jobj, "searchFolder", true);
            bool recursiveSearch = JsonUtil.GetValue<bool>(jobj, "recursiveSearch", true);
            bool caseSensitive = JsonUtil.GetValue<bool>(jobj, "caseSensitive", false);
            long? fileSizeLowerLimit = JsonUtil.GetLong(jobj, "fileSizeLowerLimit");
            long? fileSizeUpperLimit = JsonUtil.GetLong(jobj, "fileSizeUpperLimit");
            long? folderSizeLowerLimit = JsonUtil.GetLong(jobj, "folderSizeLowerLimit");
            long? folderSizeUpperLimit = JsonUtil.GetLong(jobj, "folderSizeUpperLimit");
            int? fileCountLowerLimit = JsonUtil.GetInt(jobj, "fileCountLowerLimit");
            int? fileCountUpperLimit = JsonUtil.GetInt(jobj, "fileCountUpperLimit");

            return Search(str, searchRule, searchFile, searchFolder, recursiveSearch, caseSensitive, fileSizeLowerLimit, fileSizeUpperLimit, folderSizeLowerLimit, folderSizeUpperLimit, fileCountLowerLimit, fileCountUpperLimit);

        }

        /// <summary>
        /// 检查当前文件或文件夹的大小是否在指定范围内
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="lowerLimit">大小下限。为null则不设下限</param>
        /// <param name="upperLimit">大小上限。为null则不设上限</param>
        /// <returns></returns>
        public bool InLimitedSize(long? lowerLimit, long? upperLimit)
        {
            if (lowerLimit != null && Size < lowerLimit)
                return false;
            if (upperLimit != null && Size > upperLimit)
                return false;
            return true;
        }

        /// <summary>
        /// 检查当前文件夹的文件数是否在指定范围内
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="lowerLimit">文件数下限。为null则不设下限</param>
        /// <param name="upperLimit">文件数上限。为null则不设上限</param>
        /// <returns></returns>
        public bool InLimitedFileCount(int? lowerLimit, int? upperLimit)
        {
            if (!IsFolder)
                throw new Exception("Only folder has file count.");
            if (lowerLimit != null && FileCount < lowerLimit)
                return false;
            if (upperLimit != null && FileCount > upperLimit)
                return false;
            return true;
        }

        /// <summary>
        /// 全路径相同的两个对象视为相等
        /// <para>2023.12.27</para>
        /// <para>version 1.4.1</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            FolderOrFile? f = obj as FolderOrFile;
            if (f == null)
                return false;
            return f.FullName == this.FullName;
        }

        /// <summary>
        /// 重写Equals需重写GetHashCode
        /// <para>2023.12.27</para>
        /// <para>version 1.4.1</para>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }
    }
}
