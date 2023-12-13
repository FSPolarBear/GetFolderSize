using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GetFolderSize
{
    /// <summary>
    /// 配置信息
    /// <para>2023.12.12</para>
    /// <para>version 1.3.1</para>
    /// </summary>
    public static class Config
    {
        //使用分批加载的项数阈值
        private static int thresholdBatchLoad = 1000;
        public static int ThresholdBatchLoad { get { return thresholdBatchLoad; } set { thresholdBatchLoad = value; Save(); } }

        //分批加载时每批的数量
        private static int batchSize = 500;
        public static int BatchSize { get { return batchSize; } set { batchSize = value; Save(); } }

        //分批加载每批间隔时间（毫秒）
        private static int batchInterval = 500;
        public static int BatchInterval { get { return batchInterval; } set { batchInterval = value; Save(); } }



        /// <summary>
        /// 将配置信息保存到配置文件
        /// 每次对配置信息进行修改后均需要调用此方法
        /// <para>2023.12.12</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="path"></param>
        public static void Save(string path="config.json")
        {
            JObject json = new JObject();
            json["ThresholdBatchLoad"] = thresholdBatchLoad;
            json["BatchSize"] = batchSize;
            json["BatchInterval"] = batchInterval;
            try
            {
                File.WriteAllText(path, json.ToString());
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 从配置文件中读取配置信息。若不存在配置文件或配置文件中不存在对应项则使用默认值
        /// 程序开始时调用此方法
        /// <para>2023.12.12</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="path"></param>
        public static void Load(string path = "config.json")
        {
            if (!File.Exists(path)) { return; }

            try
            {
                JObject json = JObject.Parse(File.ReadAllText(path));
                thresholdBatchLoad = JsonUtil.GetValue<int>(json, "ThresholdBatchLoad", thresholdBatchLoad);
                batchSize = JsonUtil.GetValue<int>(json, "BatchSize", batchSize);
                batchInterval = JsonUtil.GetValue<int>(json, "BatchInterval", batchInterval);
            }
            catch (Exception) { }
        }

    }
}
