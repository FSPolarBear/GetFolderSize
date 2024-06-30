using GetFolderSize.util;

namespace GetFolderSizeTest
{

    /// <summary>

    /// </summary>
    /// 2024.6.14
    /// version 1.5.0
    [TestClass]
    public class WildcardUtilTest
    {
        /// <summary>

        /// </summary>
        /// 2024.6.14
        /// version 1.5.0
        [TestMethod]
        public void TestCheckValidity()
        {
            string pattern;
            bool expected, actual;

            pattern = "abc";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "abc*";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "[abc]";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "[a-e]";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "a-c";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "[A-e]";
            expected = false;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "ab[]c";
            expected = false;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "ab`[c";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "a[[b]c";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "ab[a]]c";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "ab[]]c";
            expected = true;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "abc[";
            expected = false;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "[";
            expected = false;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);

            pattern = "abc`";
            expected = false;
            actual = WildcardUtil.CheckValidity(pattern);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>

        /// </summary>
        /// 2024.6.14
        /// version 1.5.0
        [TestMethod]
        public void TestIsMatched()
        {
            string input, pattern;
            bool expected, actual;

            input = "abc.txt"; pattern = "ab?.txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc.tx"; pattern = "ab?.txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc.txtt"; pattern = "ab?.txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "avdsfgasdf4869da4f65.txt"; pattern = "*.txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "avdsfgasdf4869da4f65.ttxt"; pattern = "*.txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abcdefghijklmn"; pattern = "abc*";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abcdefghijklmn"; pattern = "abe*";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc!.txt"; pattern = "abc[!].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc^.txt"; pattern = "abc[^].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc*.txt"; pattern = "abc`*.txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc!.txt"; pattern = "abc[!!].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc1.txt"; pattern = "abc[!!].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc2.txt"; pattern = "abc[3-7].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc3.txt"; pattern = "abc[3-7].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc6.txt"; pattern = "abc[3-7].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc7.txt"; pattern = "abc[3-7].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc8.txt"; pattern = "abc[3-7].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc2.txt"; pattern = "abc[!3-7].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc3.txt"; pattern = "abc[!3-7].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc6.txt"; pattern = "abc[!3-7].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc7.txt"; pattern = "abc[!3-7].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc8.txt"; pattern = "abc[!3-7].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc7.txt"; pattern = "abc[^3-7].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc8.txt"; pattern = "abc[^3-7].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc5.txt"; pattern = "abc[56].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc6.txt"; pattern = "abc[56].txt";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc7.txt"; pattern = "abc[56].txt";
            expected = false;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc"; pattern = "abc*";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

            input = "abc]"; pattern = "abc[]]";
            expected = true;
            actual = WildcardUtil.IsMatched(input, pattern);
            Assert.AreEqual(expected, actual);

        }
    }
}
