
using GetFolderSize;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace GetFolderSizeTest
{
    /// <summary>
    /// ����FolderOrFile��
    /// <para>2023.12.8</para>
    /// <para>version 1.3.0</para>
    /// </summary>
    [TestClass]
    public class FolderOrFileTest
    {
        /// <summary>
        /// ����FolderOrFile���Search����
        /// Search: �������ļ���������ƥ����ļ����ļ��У�������һ�������������ݵ��ļ��ж���
        /// <para>2023.12.8</para>
        /// <para>version 1.3.0</para>
        /// </summary>
        [TestMethod]
        public void TestSearch()
        {
            FolderOrFile root = FolderOrFile.GetObjectFromPath("../../../FolderForTest");
    

            FolderOrFile sr1 = root.Search("file1"); // ����include�������ļ���������������
            Assert.AreEqual(3, sr1.FileCount);

            FolderOrFile sr2 = root.Search("file1", searchFile: true, searchFolder:false);  // ���Խ������ļ�
            Assert.AreEqual(2, sr2.FileCount);

            FolderOrFile sr3 = root.Search("file1", searchFile: false, searchFolder:true);  // ���Խ������ļ���
            Assert.AreEqual(1, sr3.FileCount);

            try
            {
                FolderOrFile sr4 = root.Search("file1", searchFile: false, searchFolder: false);  // ���ԼȲ������ļ�Ҳ�������ļ��С�Ӧ���׳��쳣��
                Assert.Fail();
            }
            catch (Exception)
            {

            }

            FolderOrFile sr5 = root.Search("file1", recursiveSearch: true);  // ���Եݹ�����
            Assert.AreEqual(6, sr5.FileCount);

            FolderOrFile sr6 = root.Search("file", recursiveSearch: true);  // ���Եݹ�����
            Assert.AreEqual(8, sr6.FileCount);

            FolderOrFile sr7 = root.Search("file1", "same");  // ����same����
            Assert.AreEqual(1, sr7.FileCount);

            FolderOrFile sr8 = root.Search("file1.txt", "same", recursiveSearch: true);  // ����same�������ļ���������������ͬ
            Assert.AreEqual(3, sr8.FileCount);

            FolderOrFile sr9 = root.Search("file1.tx", "same");  // ����û����������������
            Assert.AreEqual(0, sr9.FileCount);

            FolderOrFile sr10 = root.Search("\\.txt$", "regular", recursiveSearch: true);  // ����regular��������������
            Assert.AreEqual(4, sr10.FileCount);

            FolderOrFile sr11 = root.Search("\\.txt$", "regular", recursiveSearch: true, searchFolder: false);  // ����regular����
            Assert.AreEqual(3, sr11.FileCount);

        }

    }
}