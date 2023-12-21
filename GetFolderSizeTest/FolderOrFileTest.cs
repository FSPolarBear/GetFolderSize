
using GetFolderSize;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace GetFolderSizeTest
{
    /// <summary>
    /// 测试FolderOrFile类
    /// <para>2023.12.18</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    [TestClass]
    public class FolderOrFileTest
    {
        /// <summary>
        /// 测试FolderOrFile类的Search方法
        /// Search: 搜索此文件夹下名字匹配的文件或文件夹，并返回一个包含查找内容的文件夹对象
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        [TestMethod]
        public void TestSearch()
        {
            FolderOrFile root = FolderOrFile.GetObjectFromPath("../../../FolderForTest");
    

            FolderOrFile sr1 = root.Search("file1", recursiveSearch: false); // 测试include搜索。文件名包含搜索内容
            Assert.AreEqual(3, sr1.FileCount);

            FolderOrFile sr2 = root.Search("file1", searchFile: true, searchFolder:false, recursiveSearch: false);  // 测试仅搜索文件
            Assert.AreEqual(2, sr2.FileCount);

            FolderOrFile sr3 = root.Search("file1", searchFile: false, searchFolder:true, recursiveSearch: false);  // 测试仅搜索文件夹
            Assert.AreEqual(1, sr3.FileCount);

            try
            {
                FolderOrFile sr4 = root.Search("file1", searchFile: false, searchFolder: false, recursiveSearch: false);  // 测试既不搜索文件也不搜索文件夹。应当抛出异常。
                Assert.Fail();
            }
            catch (Exception)
            {

            }
            
            FolderOrFile sr5 = root.Search("file1", recursiveSearch: true);  // 测试递归搜索
            Assert.AreEqual(6, sr5.FileCount);

            FolderOrFile sr6 = root.Search("file", recursiveSearch: true);  // 测试递归搜索
            Assert.AreEqual(8, sr6.FileCount);

            FolderOrFile sr7 = root.Search("file1", SearchRules.Same, recursiveSearch: false);  // 测试same搜索
            Assert.AreEqual(1, sr7.FileCount);

            FolderOrFile sr8 = root.Search("file1.txt", SearchRules.Same, recursiveSearch: true);  // 测试same搜索。文件名与搜索内容相同
            Assert.AreEqual(3, sr8.FileCount);

            FolderOrFile sr9 = root.Search("file1.tx", SearchRules.Same);  // 测试没有搜索到结果的情况
            Assert.AreEqual(0, sr9.FileCount);

            FolderOrFile sr10 = root.Search("\\.txt$", SearchRules.Regular, recursiveSearch: true);  // 测试regular搜索。正则搜索
            Assert.AreEqual(4, sr10.FileCount);

            FolderOrFile sr11 = root.Search("\\.txt$", SearchRules.Regular, recursiveSearch: true, searchFolder: false);  // 测试regular搜索
            Assert.AreEqual(3, sr11.FileCount);

            FolderOrFile sr12 = root.Search("upper", caseSensitive: true, recursiveSearch: false); // 测试大小写敏感
            Assert.AreEqual(1, sr12.FileCount);

            FolderOrFile sr13 = root.Search("upper", caseSensitive: false, recursiveSearch: false); // 测试大小写敏感
            Assert.AreEqual(2, sr13.FileCount);

            FolderOrFile sr14 = root.Search("upper", caseSensitive: true); // 测试大小写敏感
            Assert.AreEqual(2, sr14.FileCount);

            FolderOrFile sr15 = root.Search("upper", caseSensitive: false); // 测试大小写敏感
            Assert.AreEqual(4, sr15.FileCount);

            FolderOrFile sr16 = root.Search(".*", SearchRules.Regular, fileSizeLowerLimit: 1L, searchFolder: false); // 测试文件大小上下限
            Assert.AreEqual(3, sr16.FileCount);

            FolderOrFile sr17 = root.Search(".*", SearchRules.Regular, fileSizeLowerLimit: 1L, fileSizeUpperLimit: 4096L , searchFolder: false); // 测试文件大小上下限
            Assert.AreEqual(2, sr17.FileCount);

            FolderOrFile sr18 = root.Search(".sizetest", fileSizeUpperLimit: 4096L, searchFolder: false); // 测试文件大小上下限
            Assert.AreEqual(2, sr18.FileCount);

            FolderOrFile sr19 = root.Search(".*", SearchRules.Regular, folderSizeLowerLimit: 1L, searchFile: false); // 测试文件夹大小上下限
            Assert.AreEqual(2, sr19.FileCount);

            FolderOrFile sr20 = root.Search(".*", SearchRules.Regular, folderSizeLowerLimit: 1L, folderSizeUpperLimit: 2049L, searchFile: false); // 测试文件夹大小上下限
            Assert.AreEqual(1, sr20.FileCount);

            FolderOrFile sr21 = root.Search(".*", SearchRules.Regular, folderSizeLowerLimit: 4000L, searchFile: false); // 测试文件夹大小上下限
            Assert.AreEqual(1, sr21.FileCount);

            FolderOrFile sr22 = root.Search(".*", SearchRules.Regular, fileCountUpperLimit: 0, searchFile: false); // 测试文件夹文件数上下限
            Assert.AreEqual(1, sr22.FileCount);

            FolderOrFile sr23 = root.Search(".*", SearchRules.Regular, fileCountLowerLimit: 1, fileCountUpperLimit: 2, searchFile: false); // 测试文件夹文件数上下限
            Assert.AreEqual(1, sr23.FileCount);

            FolderOrFile sr24 = root.Search(".*", SearchRules.Regular, fileCountLowerLimit: 3, searchFile: false); // 测试文件夹文件数上下限
            Assert.AreEqual(2, sr24.FileCount);


        }


        /// <summary>
        /// 测试FolderOrFile类的RegexToLower方法
        /// RegexToLower: 将正则表达式转化为小写
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        [TestMethod]
        public void TestRegexToLower()
        {
            Type type = typeof(FolderOrFile);
            MethodInfo method = type.GetMethod("RegexToLower", BindingFlags.Static | BindingFlags.NonPublic);

            object[] para1 = { "abcA" };
            string res1 = (string) method.Invoke(null, para1);
            Assert.AreEqual("abca", res1);

            object[] para2 = { "A\\BcDE\\fG\\HiJ" };
            string res2 = (string)method.Invoke(null, para2);
            Assert.AreEqual("a\\Bcde\\fg\\Hij", res2);
        }


    }
}