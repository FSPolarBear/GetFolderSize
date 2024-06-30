using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSizeTest
{

    using GetFolderSize;
    using Newtonsoft.Json.Linq;
    using System.Reflection;

    /// <summary>
    /// 测试FileSizeUtilTest类
    /// </summary>
    /// 2023.12.20
    /// version 1.4.0
    [TestClass]
    public class FileSizeUtilTest
    {

        /// <summary>
        /// 测试FileSizeUtil类的TestFileSizeStringToNumber方法
        /// TestFileSizeStringToNumber: 将表示文件大小的字符串转化为数字类型
        /// </summary>
        /// 2023.12.19
        /// version 1.4.0
        [TestMethod]
        public void TestFileSizeStringToNumber()
        {
            long? res1 = FileSizeUtil.FileSizeStringToNumber("");
            Assert.IsNull(res1);

            long? res2 = FileSizeUtil.FileSizeStringToNumber("1234");
            Assert.AreEqual(1234L, res2);

            long? res3 = FileSizeUtil.FileSizeStringToNumber("4321B");
            Assert.AreEqual(4321L, res3);

            long? res4 = FileSizeUtil.FileSizeStringToNumber("1234K");
            Assert.AreEqual(1234L * 1024L, res4);

            long? res5 = FileSizeUtil.FileSizeStringToNumber("4321 KB");
            Assert.AreEqual(4321L * 1024L, res5);

            long? res6 = FileSizeUtil.FileSizeStringToNumber("5678m");
            Assert.AreEqual(5678L * 1024L * 1024L, res6);

            long? res7 = FileSizeUtil.FileSizeStringToNumber("8765MB");
            Assert.AreEqual(8765L * 1024L * 1024L, res7);

            long? res8 = FileSizeUtil.FileSizeStringToNumber("1234G");
            Assert.AreEqual(1234L * 1024L * 1024L * 1024L, res8);

            long? res9 = FileSizeUtil.FileSizeStringToNumber("4321 GB");
            Assert.AreEqual(4321L * 1024L * 1024L * 1024L, res9);


            long? res10 = FileSizeUtil.FileSizeStringToNumber("1234T");
            Assert.AreEqual(1234L * 1024L * 1024L * 1024L * 1024L, res10);

            long? res11 = FileSizeUtil.FileSizeStringToNumber("4321 TB");
            Assert.AreEqual(4321L * 1024L * 1024L * 1024L * 1024L, res11);

        }


        /// <summary>
        /// 测试FileSizeUtil类的IsFileSizeOrEmpty法
        /// IsFileSizeOrEmpty: 检查字符串是否是文件大小或空字符串
        /// </summary>
        /// 2023.12.19
        /// version 1.4.0
        [TestMethod]
        public void TestIsFileSizeOrEmpty()
        {
            bool res1 = FileSizeUtil.IsFileSizeOrEmpty("");
            Assert.IsTrue(res1);

            bool res2 = FileSizeUtil.IsFileSizeOrEmpty("1234");
            Assert.IsTrue(res2);

            bool res3 = FileSizeUtil.IsFileSizeOrEmpty("1234B");
            Assert.IsTrue(res3);

            bool res4 = FileSizeUtil.IsFileSizeOrEmpty("1234 KB");
            Assert.IsTrue(res4);

            bool res5 = FileSizeUtil.IsFileSizeOrEmpty("1234MB");
            Assert.IsTrue(res5);

            bool res6 = FileSizeUtil.IsFileSizeOrEmpty("1234 GB");
            Assert.IsTrue(res6);

            bool res7 = FileSizeUtil.IsFileSizeOrEmpty(" 1234TB");
            Assert.IsTrue(res7);

            bool res8 = FileSizeUtil.IsFileSizeOrEmpty("-1234");
            Assert.IsFalse(res8);

            bool res9 = FileSizeUtil.IsFileSizeOrEmpty("1234K ");
            Assert.IsTrue(res9);

            bool res10 = FileSizeUtil.IsFileSizeOrEmpty("1234M");
            Assert.IsTrue(res10);

            bool res11 = FileSizeUtil.IsFileSizeOrEmpty("1234G");
            Assert.IsTrue(res11);

            bool res12 = FileSizeUtil.IsFileSizeOrEmpty("1234T");
            Assert.IsTrue(res12);

            bool res13 = FileSizeUtil.IsFileSizeOrEmpty("-1234KB");
            Assert.IsFalse(res13);

            bool res14 = FileSizeUtil.IsFileSizeOrEmpty("1234.5KB");
            Assert.IsFalse(res14);

            bool res15 = FileSizeUtil.IsFileSizeOrEmpty("12a345KB");
            Assert.IsFalse(res15);

            bool res16 = FileSizeUtil.IsFileSizeOrEmpty("aga65c a");
            Assert.IsFalse(res16);

            bool res17 = FileSizeUtil.IsFileSizeOrEmpty("0");
            Assert.IsTrue(res17);

            bool res18 = FileSizeUtil.IsFileSizeOrEmpty("12 34G");
            Assert.IsFalse(res18);

        }

        /// <summary>
        /// 测试FileSizeUtil类的FileSizeNumberToStringWithIntegerValue法
        /// FileSizeNumberToStringWithIntegerValue: 将文件大小转化为字符串表示形式，如10240000 -> "1000KB"。其中数值部分为整数，若使用较大的单位无法保证数值部分为整数则使用较小的单位
        /// </summary>
        /// 2023.12.20
        /// version 1.4.0
        [TestMethod]
        public void TestFileSizeNumberToStringWithIntegerValue()
        {
            string res;
            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(null);
            Assert.AreEqual(string.Empty, res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(0L);
            Assert.AreEqual("0B", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(100L);
            Assert.AreEqual("100B", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(1024L);
            Assert.AreEqual("1KB", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(1025L);
            Assert.AreEqual("1025B", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(4096L);
            Assert.AreEqual("4KB", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(1024L * 1024L * 100);
            Assert.AreEqual("100MB", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(1024L * 1025L);
            Assert.AreEqual("1025KB", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(1024L * 1024L * 1024L * 3L);
            Assert.AreEqual("3GB", res);

            res = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(1024L * 1024L * 1024L * 1024L * 5L);
            Assert.AreEqual("5TB", res);

        }

    }

}
