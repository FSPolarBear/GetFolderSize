
using GetFolderSize;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace GetFolderSizeTest
{
    /// <summary>
    /// ����FolderOrFile��
    /// <para>2023.12.18</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    [TestClass]
    public class FolderOrFileTest
    {
        /// <summary>
        /// ����FolderOrFile���Search����
        /// Search: �������ļ���������ƥ����ļ����ļ��У�������һ�������������ݵ��ļ��ж���
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        [TestMethod]
        public void TestSearch()
        {
            FolderOrFile root = FolderOrFile.GetObjectFromPath("../../../FolderForTest");
    

            FolderOrFile sr1 = root.Search("file1", recursiveSearch: false); // ����include�������ļ���������������
            Assert.AreEqual(3, sr1.FileCount);

            FolderOrFile sr2 = root.Search("file1", searchFile: true, searchFolder:false, recursiveSearch: false);  // ���Խ������ļ�
            Assert.AreEqual(2, sr2.FileCount);

            FolderOrFile sr3 = root.Search("file1", searchFile: false, searchFolder:true, recursiveSearch: false);  // ���Խ������ļ���
            Assert.AreEqual(1, sr3.FileCount);

            try
            {
                FolderOrFile sr4 = root.Search("file1", searchFile: false, searchFolder: false, recursiveSearch: false);  // ���ԼȲ������ļ�Ҳ�������ļ��С�Ӧ���׳��쳣��
                Assert.Fail();
            }
            catch (Exception)
            {

            }
            
            FolderOrFile sr5 = root.Search("file1", recursiveSearch: true);  // ���Եݹ�����
            Assert.AreEqual(6, sr5.FileCount);

            FolderOrFile sr6 = root.Search("file", recursiveSearch: true);  // ���Եݹ�����
            Assert.AreEqual(8, sr6.FileCount);

            FolderOrFile sr7 = root.Search("file1", SearchRules.Same, recursiveSearch: false);  // ����same����
            Assert.AreEqual(1, sr7.FileCount);

            FolderOrFile sr8 = root.Search("file1.txt", SearchRules.Same, recursiveSearch: true);  // ����same�������ļ���������������ͬ
            Assert.AreEqual(3, sr8.FileCount);

            FolderOrFile sr9 = root.Search("file1.tx", SearchRules.Same);  // ����û����������������
            Assert.AreEqual(0, sr9.FileCount);

            FolderOrFile sr10 = root.Search("\\.txt$", SearchRules.Regular, recursiveSearch: true);  // ����regular��������������
            Assert.AreEqual(4, sr10.FileCount);

            FolderOrFile sr11 = root.Search("\\.txt$", SearchRules.Regular, recursiveSearch: true, searchFolder: false);  // ����regular����
            Assert.AreEqual(3, sr11.FileCount);

            FolderOrFile sr12 = root.Search("upper", caseSensitive: true, recursiveSearch: false); // ���Դ�Сд����
            Assert.AreEqual(1, sr12.FileCount);

            FolderOrFile sr13 = root.Search("upper", caseSensitive: false, recursiveSearch: false); // ���Դ�Сд����
            Assert.AreEqual(2, sr13.FileCount);

            FolderOrFile sr14 = root.Search("upper", caseSensitive: true); // ���Դ�Сд����
            Assert.AreEqual(2, sr14.FileCount);

            FolderOrFile sr15 = root.Search("upper", caseSensitive: false); // ���Դ�Сд����
            Assert.AreEqual(4, sr15.FileCount);

            FolderOrFile sr16 = root.Search(".*", SearchRules.Regular, fileSizeLowerLimit: 1L, searchFolder: false); // �����ļ���С������
            Assert.AreEqual(3, sr16.FileCount);

            FolderOrFile sr17 = root.Search(".*", SearchRules.Regular, fileSizeLowerLimit: 1L, fileSizeUpperLimit: 4096L , searchFolder: false); // �����ļ���С������
            Assert.AreEqual(2, sr17.FileCount);

            FolderOrFile sr18 = root.Search(".sizetest", fileSizeUpperLimit: 4096L, searchFolder: false); // �����ļ���С������
            Assert.AreEqual(2, sr18.FileCount);

            FolderOrFile sr19 = root.Search(".*", SearchRules.Regular, folderSizeLowerLimit: 1L, searchFile: false); // �����ļ��д�С������
            Assert.AreEqual(2, sr19.FileCount);

            FolderOrFile sr20 = root.Search(".*", SearchRules.Regular, folderSizeLowerLimit: 1L, folderSizeUpperLimit: 2049L, searchFile: false); // �����ļ��д�С������
            Assert.AreEqual(1, sr20.FileCount);

            FolderOrFile sr21 = root.Search(".*", SearchRules.Regular, folderSizeLowerLimit: 4000L, searchFile: false); // �����ļ��д�С������
            Assert.AreEqual(1, sr21.FileCount);

            FolderOrFile sr22 = root.Search(".*", SearchRules.Regular, fileCountUpperLimit: 0, searchFile: false); // �����ļ����ļ���������
            Assert.AreEqual(1, sr22.FileCount);

            FolderOrFile sr23 = root.Search(".*", SearchRules.Regular, fileCountLowerLimit: 1, fileCountUpperLimit: 2, searchFile: false); // �����ļ����ļ���������
            Assert.AreEqual(1, sr23.FileCount);

            FolderOrFile sr24 = root.Search(".*", SearchRules.Regular, fileCountLowerLimit: 3, searchFile: false); // �����ļ����ļ���������
            Assert.AreEqual(2, sr24.FileCount);


        }


        /// <summary>
        /// ����FolderOrFile���RegexToLower����
        /// RegexToLower: ��������ʽת��ΪСд
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