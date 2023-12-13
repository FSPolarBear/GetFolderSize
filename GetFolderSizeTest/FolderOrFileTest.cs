
using GetFolderSize;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace GetFolderSizeTest
{
    /// <summary>
    /// 测试FolderOrFile类
    /// <para>2023.12.8</para>
    /// <para>version 1.3.0</para>
    /// </summary>
    [TestClass]
    public class FolderOrFileTest
    {
        /// <summary>
        /// 测试FolderOrFile类的Search方法
        /// Search: 搜索此文件夹下名字匹配的文件或文件夹，并返回一个包含查找内容的文件夹对象
        /// <para>2023.12.8</para>
        /// <para>version 1.3.0</para>
        /// </summary>
        [TestMethod]
        public void TestSearch()
        {
            FolderOrFile root = FolderOrFile.GetObjectFromPath("../../../FolderForTest");
    

            FolderOrFile sr1 = root.Search("file1"); // 测试include搜索。文件名包含搜索内容
            Assert.AreEqual(3, sr1.FileCount);

            FolderOrFile sr2 = root.Search("file1", searchFile: true, searchFolder:false);  // 测试仅搜索文件
            Assert.AreEqual(2, sr2.FileCount);

            FolderOrFile sr3 = root.Search("file1", searchFile: false, searchFolder:true);  // 测试仅搜索文件夹
            Assert.AreEqual(1, sr3.FileCount);

            try
            {
                FolderOrFile sr4 = root.Search("file1", searchFile: false, searchFolder: false);  // 测试既不搜索文件也不搜索文件夹。应当抛出异常。
                Assert.Fail();
            }
            catch (Exception)
            {

            }

            FolderOrFile sr5 = root.Search("file1", recursiveSearch: true);  // 测试递归搜索
            Assert.AreEqual(6, sr5.FileCount);

            FolderOrFile sr6 = root.Search("file", recursiveSearch: true);  // 测试递归搜索
            Assert.AreEqual(8, sr6.FileCount);

            FolderOrFile sr7 = root.Search("file1", "same");  // 测试same搜索
            Assert.AreEqual(1, sr7.FileCount);

            FolderOrFile sr8 = root.Search("file1.txt", "same", recursiveSearch: true);  // 测试same搜索。文件名与搜索内容相同
            Assert.AreEqual(3, sr8.FileCount);

            FolderOrFile sr9 = root.Search("file1.tx", "same");  // 测试没有搜索到结果的情况
            Assert.AreEqual(0, sr9.FileCount);

            FolderOrFile sr10 = root.Search("\\.txt$", "regular", recursiveSearch: true);  // 测试regular搜索。正则搜索
            Assert.AreEqual(4, sr10.FileCount);

            FolderOrFile sr11 = root.Search("\\.txt$", "regular", recursiveSearch: true, searchFolder: false);  // 测试regular搜索
            Assert.AreEqual(3, sr11.FileCount);

        }

    }
}