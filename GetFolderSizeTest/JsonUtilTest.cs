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

    namespace GetFolderSizeTest
    {
        /// <summary>
        /// 测试JsonUtil类
        /// <para>2023.12.19</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        [TestClass]
        public class JsonUtilTest
        {
            
            

            /// <summary>
            /// 测试JsonUtil类的GetValue方法
            /// GetValue: 从json对象中读取指定键对应的值。如果json对象中不存在指定的键或指定键对应的值与读取类型不符则返回默认值
            /// <para>2023.12.12</para>
            /// <para>version 1.3.1</para>
            /// </summary>
            [TestMethod]
            public void TestGetValue()
            {
                JObject jobj = new JObject();

                jobj["key1"] = "abcdefg";
                jobj["key2"] = false;
                jobj["key3"] = 1234;
                jobj["key4"] = new JArray() { 1, 2, 3, 4 };
                jobj["key5"] = new JObject() { { "abc", 123 }, { "efg", true }, { "hig", "asdf" } };
                jobj["key6"] = 123.4567;

                Type type = typeof(JsonUtil);
                MethodInfo method = type.GetMethod("GetValue", BindingFlags.Static | BindingFlags.Public);
                MethodInfo method_string = method.MakeGenericMethod(typeof(string));
                MethodInfo method_bool = method.MakeGenericMethod(typeof(bool));
                MethodInfo method_int = method.MakeGenericMethod(typeof(int));
                MethodInfo method_array = method.MakeGenericMethod(typeof(JArray));
                MethodInfo method_objcet = method.MakeGenericMethod(typeof(JObject));
                MethodInfo method_float = method.MakeGenericMethod(typeof(double));

                Object[] para1 = { jobj, "key1", "a" }; // 测试成功读取字符串
                string res1 = (string)method_string.Invoke(null, para1);
                Assert.AreEqual("abcdefg", res1);
                Console.WriteLine(res1);

                Object[] para2 = { jobj, "key2", true }; // 测试成功读取布尔值
                bool res2 = (bool)method_bool.Invoke(null, para2);
                Assert.AreEqual(false, res2);
                Console.WriteLine(res2);

                Object[] para3 = { jobj, "key3", -1 }; // 测试成功读取整数
                int res3 = (int)method_int.Invoke(null, para3);
                Assert.AreEqual(1234, res3);
                Console.WriteLine(res3);

                Object[] para4 = { jobj, "key4", new JArray() }; // 测试成功读取数组
                JArray res4 = (JArray)method_array.Invoke(null, para4);
                Assert.AreEqual(new JArray() { 1, 2, 3, 4 }.ToString(), res4.ToString());
                Console.WriteLine(res4);

                Object[] para5 = { jobj, "key5", new JObject() }; // 测试成功读取对象
                JObject res5 = (JObject)method_objcet.Invoke(null, para5);
                Assert.AreEqual(new JObject() { { "abc", 123 }, { "efg", true }, { "hig", "asdf" } }.ToString(), res5.ToString());
                Console.WriteLine(res5);

                Object[] para6 = { jobj, "key6", 0.0 }; // 测试浮点数
                double res6 = (double)method_float.Invoke(null, para6);
                Assert.AreEqual(123.4567, res6, 1e-7);
                Console.WriteLine(res6);


                Object[] para7 = { jobj, "key4", "a" }; // 测试键不存在
                string res7 = (string)method_string.Invoke(null, para7);
                Assert.AreEqual("a", res7);
                Console.WriteLine(res7);

                Object[] para8 = { jobj, "key1", -1 }; // 测试指定键对应的值与读取类型不符
                int res8 = (int)method_int.Invoke(null, para8);
                Assert.AreEqual(-1, res8);
                Console.WriteLine(res8);

                Object[] para9 = { jobj, "key2", "a" }; // 测试指定键对应的值与读取类型不符
                string res9 = (string)method_string.Invoke(null, para9);
                Assert.AreEqual("a", res9);
                Console.WriteLine(res9);

                Object[] para10 = { null, "any_key", "default_value" }; // 测试jobj为null
                string res10 = (string)method_string.Invoke(null, para10);
                Assert.AreEqual("default_value", res10);
                Console.WriteLine(res10);

            }


            /// <summary>
            /// 测试JsonUtil类的GetInt、GetDouble、GetString、GetBoolen、GetJObject、GetJArray方法
            /// <para>2023.12.19</para>
            /// <para>version 1.4.0</para>
            /// </summary>
            [TestMethod]
            public void TestGet()
            {
                JObject jobj = new JObject();

                jobj["key1"] = "1234567";
                jobj["key2"] = true;
                jobj["key3"] = 1234;
                jobj["key4"] = new JArray() { 1, 2, 3, 4 };
                jobj["key5"] = new JObject() { { "abc", 123 }, { "efg", true }, { "hig", "asdf" } };
                jobj["key6"] = 123.4567;
                jobj["key7"] = 5000000000L;

                int? res1 = JsonUtil.GetInt(jobj, "key3"); //测试GetInt
                Assert.AreEqual(1234, res1);

                int? res2 = JsonUtil.GetInt(jobj, "key2"); 
                Assert.IsNull(res2);

                double? res3 = JsonUtil.GetDouble(jobj, "key6"); //测试GetDouble
                Assert.AreEqual(123.4567, res3.Value, 1e-7);

                double? res4 = JsonUtil.GetDouble(jobj, "key3");
                Assert.IsNull(res4);

                double? res5 = JsonUtil.GetDouble(jobj, "any_key", 0.321);
                Assert.AreEqual(0.321, res5.Value, 1e-7);

                bool? res6 = JsonUtil.GetBool(jobj, "key2"); //测试GetBool
                Assert.IsTrue(res6);

                bool? res7 = JsonUtil.GetBool(jobj, "key1");
                Assert.IsNull(res7);

                string? res8 = JsonUtil.GetString(jobj, "key1"); //测试GetString
                Assert.AreEqual("1234567", res8);

                string? res9 = JsonUtil.GetString(jobj, "key6");
                Assert.IsNull(res9);

                JObject? res10 = JsonUtil.GetJObject(jobj, "key5"); //测试GetJObject
                Assert.AreEqual(new JObject() { { "abc", 123 }, { "efg", true }, { "hig", "asdf" } }.ToString(), res10.ToString());

                JObject? res11 = JsonUtil.GetJObject(jobj, "key1");
                Assert.IsNull (res11);

                JArray? res12 = JsonUtil.GetJArray(jobj, "key4"); //测试GetJArray
                Assert.AreEqual(new JArray() { 1, 2, 3, 4 }.ToString(), res12.ToString());

                JArray? res13 = JsonUtil.GetJArray(jobj, "key1");
                Assert.IsNull(res13);

                long? res14 = JsonUtil.GetLong(jobj, "key7", null); //测试GetLong
                Assert.AreEqual(5000000000L, res14);

                long? res15 = JsonUtil.GetLong(jobj, "key1", null);
                Assert.IsNull(res15);


            }
        }
    }
}
