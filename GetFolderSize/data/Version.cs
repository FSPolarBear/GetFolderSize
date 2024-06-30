using GetFolderSize.util;
using Json;

namespace GetFolderSize
{
    /// <summary>
    /// 版本，以及读取旧版本数据时兼容的操作。
    /// </summary>
    /// 2024.6.28
    /// version 2.0.0
    internal static class Version
    {
        public static readonly string CurrentVersion = "2.0.0";

        /// <summary>
        /// 导入时更新导入数据。
        /// </summary>
        /// 2024.6.28
        /// version 2.0.0
        /// <param name="json"></param>
        /// <returns></returns>
        public static JsonObject UpdateImportedData(JsonObject json)
        {
            return UpdateImportedDataTo1_5_0(json);
        }

        /// <summary>
        /// 导入时将低于2.0.0版本的数据更新至2.0.0版本。
        /// </summary>
        /// 2024.6.28
        /// version 2.0.0
        /// <param name="json"></param>
        /// <returns></returns>
        private static JsonObject UpdateImportedDataTo2_0_0(JsonObject json)
        {
            string target_version = "2.0.0";
            string? version = json.Get<string?>("Version", null);
            if (CompareVersionUtil.isLowerVersion(version, target_version))
            {
                _UpdateImportedDataTo1_5_0(json);
                json["Version"] = target_version;  
            }
            return json;
        }

        /// <summary>
        /// UpdateImportedDataTo2_0_0方法中使用的递归函数。
        /// </summary>
        /// 2024.6.28
        /// version 2.0.0
        /// <param name="json"></param>
        private static void _UpdateImportedDataTo2_0_0(JsonObject json)
        {
            bool? IsFolder = json.Get<bool?>("IsFolder", null);
            if (IsFolder == null)
                return;
            if (IsFolder.Value)
            {
                json["__Type"] = "GetFolderSize.Folder";
                foreach (JsonObject child in json.Get<List<JsonObject>>("Children"))
                {
                    _UpdateImportedDataTo2_0_0(child);
                }
            }
            else
            {
                json["__Type"] = "GetFolderSize.File";
            }
        }
    }
}
