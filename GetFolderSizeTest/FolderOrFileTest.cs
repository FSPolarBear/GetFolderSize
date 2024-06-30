
using GetFolderSize;
using Json;

namespace GetFolderSizeTest
{
    /// <summary>
    /// 测试FolderOrFile类
    /// </summary>
    /// 2024.5.31
    /// version 1.5.0
    [TestClass]
    public class FolderOrFileTest
    {
        /// <summary>
        /// 测试FolderOrFile类的Search方法
        /// Search: 搜索此文件夹下名字匹配的文件或文件夹，并返回一个包含查找内容的文件夹对象
        /// </summary>
        /// 2024.5.31
        /// version 1.5.0
        [TestMethod]
        public void TestSearch()
        {
            Folder root = new Folder("../../../FolderForTest");
            JsonObject args;


            args = new JsonObject() { { "str", "file1" }, { "recursiveSearch", false } };
            Folder sr1 = root.Search(new SearchArgs(args)); // 测试include搜索。文件名包含搜索内容
            Assert.AreEqual(3, sr1.FileCount);

            args = new JsonObject() { { "str", "file1" }, { "searchFile", true }, { "searchFolder", false }, { "recursiveSearch", false } };
            Folder sr2 = root.Search(new SearchArgs(args)); // 测试仅搜索文件
            Assert.AreEqual(2, sr2.FileCount);

            args = new JsonObject() { { "str", "file1" }, { "searchFile", false }, { "searchFolder", true }, { "recursiveSearch", false } };
            Folder sr3 = root.Search(new SearchArgs(args)); // 测试仅搜索文件夹
            Assert.AreEqual(1, sr3.FileCount);

            try
            {
                args = new JsonObject() { { "str", "file1" }, { "searchFile", false }, { "searchFolder", false }, { "recursiveSearch", false } };
                Folder sr4 = root.Search(new SearchArgs(args)); // 测试既不搜索文件也不搜索文件夹。应当抛出异常。
                Assert.Fail();
            }
            catch (Exception)
            {

            }

            args = new JsonObject() { { "str", "file1" },{ "recursiveSearch", true } };
            Folder sr5 = root.Search(new SearchArgs(args)); // 测试递归搜索
            Assert.AreEqual(6, sr5.FileCount);

            args = new JsonObject() { { "str", "file" }, { "recursiveSearch", true } };
            Folder sr6 = root.Search(new SearchArgs(args)); // 测试递归搜索
            Assert.AreEqual(8, sr6.FileCount);

            args = new JsonObject() { { "str", "file1" }, { "searchRule", (int)SearchRules.Same }, { "recursiveSearch", false } };
            Folder sr7 = root.Search(new SearchArgs(args)); // 测试same搜索
            Assert.AreEqual(1, sr7.FileCount);

            args = new JsonObject() { { "str", "file1.txt" }, { "searchRule", (int)SearchRules.Same }, { "recursiveSearch", true } };
            Folder sr8 = root.Search(new SearchArgs(args)); // 测试same搜索。文件名与搜索内容相同
            Assert.AreEqual(3, sr8.FileCount);

            args = new JsonObject() { { "str", "file1.tx" }, { "searchRule", (int)SearchRules.Same } };
            Folder sr9 = root.Search(new SearchArgs(args)); // 测试没有搜索到结果的情况
            Assert.AreEqual(0, sr9.FileCount);

            args = new JsonObject() { { "str", "\\.txt$" }, { "searchRule", (int)SearchRules.Regular }, { "recursiveSearch", true } };
            Folder   sr10 = root.Search(new SearchArgs(args)); ;  // 测试regular搜索。正则搜索
            Assert.AreEqual(4, sr10.FileCount);

            args = new JsonObject() { { "str", "\\.txt$" }, { "searchRule", (int)SearchRules.Regular }, { "recursiveSearch", true } , { "searchFolder", false } };
            Folder sr11 = root.Search(new SearchArgs(args));  // 测试regular搜索
            Assert.AreEqual(3, sr11.FileCount);

            args = new JsonObject() { { "str", "upper" }, { "caseSensitive", true }, { "recursiveSearch", false }};
            Folder sr12 = root.Search(new SearchArgs(args));// 测试大小写敏感
            Assert.AreEqual(1, sr12.FileCount);

            args = new JsonObject() { { "str", "upper" }, { "caseSensitive", false }, { "recursiveSearch", false }};
            Folder sr13 = root.Search(new SearchArgs(args)); // 测试大小写敏感
            Assert.AreEqual(2, sr13.FileCount);

            args = new JsonObject() { { "str", "upper" }, { "caseSensitive", true }};
            Folder   sr14 = root.Search(new SearchArgs(args)); // 测试大小写敏感
            Assert.AreEqual(2, sr14.FileCount);

            args = new JsonObject() { { "str", "upper" }, { "caseSensitive", false } };
            Folder sr15 = root.Search(new SearchArgs(args)); // 测试大小写敏感
            Assert.AreEqual(4, sr15.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "fileSizeLowerLimit", 1L }, { "searchFolder", false } };
            Folder sr16 = root.Search(new SearchArgs(args)); // 测试文件大小上下限
            Assert.AreEqual(3, sr16.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "fileSizeLowerLimit", 1L }, { "fileSizeUpperLimit", 4096L }, { "searchFolder", false } };
            Folder sr17 = root.Search(new SearchArgs(args)); // 测试文件大小上下限
            Assert.AreEqual(2, sr17.FileCount);

            args = new JsonObject() { { "str", ".sizetest" }, { "fileSizeUpperLimit", 4096L }, { "searchFolder", false } };
            Folder   sr18 = root.Search(new SearchArgs(args)); // 测试文件大小上下限
            Assert.AreEqual(2, sr18.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "folderSizeLowerLimit", 1L }, { "searchFile", false } };
            Folder sr19 = root.Search(new SearchArgs(args)); // 测试文件夹大小上下限
            Assert.AreEqual(2, sr19.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "folderSizeLowerLimit", 1L }, { "folderSizeUpperLimit", 4000L }, { "searchFile", false } };
            Folder sr20 = root.Search(new SearchArgs(args)); // 测试文件夹大小上下限
            Assert.AreEqual(1, sr20.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "folderSizeLowerLimit", 4000L }, { "searchFile", false } };
            Folder sr21 = root.Search(new SearchArgs(args)); // 测试文件夹大小上下限
            Assert.AreEqual(1, sr21.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "fileCountUpperLimit", 0 }, { "searchFile", false } };
            Folder sr22 = root.Search(new SearchArgs(args)); // 测试文件夹文件数上下限
            Assert.AreEqual(1, sr22.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "fileCountLowerLimit", 1 }, { "fileCountUpperLimit", 2 }, { "searchFile", false } };
            Folder sr23 = root.Search(new SearchArgs(args)); // 测试文件夹文件数上下限
            Assert.AreEqual(1, sr23.FileCount);

            args = new JsonObject() { { "str", ".*" }, { "searchRule", (int)SearchRules.Regular }, { "fileCountLowerLimit", 3 }, { "searchFile", false } };
            Folder sr24 = root.Search(new SearchArgs(args)); // 测试文件夹文件数上下限
            Assert.AreEqual(2, sr24.FileCount);


        }



    }
}