using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Json
{
    /// <summary>
    /// Type of json item.
    /// </summary>
    /// 2024.6.29
    /// version 1.0.4
    public enum JsonItemType
    {
        String,
        Integer,
        Float,
        Bool,
        Object,
        Array,
        Null,
    }

    /// <summary>
    /// Item of json.
    /// </summary>
    /// 2024.6.18
    /// version 1.0.4
    public abstract class JsonItem
    {
        public JsonItemType ItemType { get; protected set; }

        /// <summary>
        /// Parse a string to a json item.
        /// </summary>
        /// 2024.3.21
        /// version 1.0.2
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="JsonFormatException">The string cannot be parsed.</exception>
        public static JsonItem Parse(string str)
        {
            if (str == "null")
                return new JsonNull();
            else if (str == "true")
                return new JsonBool(true);
            else if (str == "false")
                return new JsonBool(false);
            else if (str.Length >= 2 && str[0] == '"' && str[str.Length - 1] == '"')
                return JsonString.Parse(str);
            else if (str.Length >= 2 && str[0] == '[' && str[str.Length - 1] == ']')
                return JsonArray.Parse(str);
            else if (str.Length >= 2 && str[0] == '{' && str[str.Length - 1] == '}')
                return JsonObject.Parse(str);
            else
            {
                JsonItem result = JsonParser.ParseNumber(str, 0, out int end);
                if (end == str.Length - 1)
                    return result;
                else
                    throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage());
            }
                
        }

        /// <summary>
        /// Create a JsonItem object by the specified value. If the specified value is a JsonItem, return it without create a new object.
        /// </summary>
        /// 2024.6.29
        /// version 1.0.4
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="JsonInvalidTypeException">The value cannot be convert to a JsonItem object.</exception>
        public static JsonItem CreateFromValue(object? value)
        {
            if (value == null)
                return new JsonNull();
            string exceptionMessage = JsonExceptionMessage.GetInvalidTypeExceptionMessage(new string[] { "JsonItem", "null", "bool", "string", "char","long", "int", "short", "decimal", "double", "float", "List<T>", "T[]", "Dictionary<string/JsonString, T>", "IJsonObject" }, value.GetType());
            if (value is JsonItem)
            {
                return (JsonItem)value;
            }
            else if (value is bool)
            {
                return new JsonBool((bool)value);
            }
            else if (value is decimal)
            {
                return new JsonFloat((decimal)value);
            }
            else if (value is double)
            {
                return new JsonFloat((decimal)(double)value);
            }
            else if (value is float)
            {
                return new JsonFloat((decimal)((float)value));
            }
            else if (value is long)
            {
                return new JsonInteger((long)value);
            }
            else if (value is int)
            {
                return new JsonInteger((long)(int)value);
            }
            else if (value is short)
            {
                return new JsonInteger((long)(short)value);
            }
            else if (value is string)
            {
                return new JsonString((string)value);
            }
            else if (value is char)
            {
                return new JsonString(((char)value));
            }
            else if (value is List<JsonItem>)
            {
                return new JsonArray((List<JsonItem>)value);
            }
            else if (value is Dictionary<JsonString, JsonItem>)
            {
                return new JsonObject((Dictionary<JsonString, JsonItem>)value);
            }
            else if (value is Array) { 
                return new JsonArray((Array)value);
            }
            else if (Utils.IsList(value.GetType()))
            {
                JsonArray result = new JsonArray();
                foreach (var item in (System.Collections.IEnumerable)value)
                {
                    result.Add(item);
                }
                return result;
            }
            else if (Utils.IsDictionaryWithStringKey(value.GetType()))
            {
                JsonObject result = new JsonObject();
                System.Collections.IDictionary dic = (System.Collections.IDictionary)value;
                foreach (var key in dic.Keys) 
                { 
                    if (key is string)
                    {
                        string key_str = (string)key;
                        result[key_str] = dic[key];
                    }
                    else if (key is JsonString)
                    {
                        JsonString key_jstr = (JsonString)key;
                        result[key_jstr] = dic[key];
                    }
                    else
                        throw new JsonInvalidTypeException(exceptionMessage);
                }
                return result;
            }
            else if (value is IJsonObject ijobj)
            {
                return new JsonObject(ijobj);
            }
            else 
                throw new JsonInvalidTypeException(exceptionMessage);
        }

        /// <summary>
        /// Get the value of the item in the specified type.
        /// </summary>
        /// 2024.1.3
        /// version 1.0.0
        /// <typeparam name="T"></typeparam>
        /// <returns>值</returns>
        /// <exception cref="JsonInvalidTypeException">The type is invalid.</exception>
        public abstract T GetValue<T>();

        /// <summary>
        /// Get the value of the item in the specified type. If the type is invalid, return a specified default value.
        /// </summary>
        /// 2024.1.3
        /// version 1.0.0
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValue<T>(T defaultValue)
        {
            try
            {
                return GetValue<T>();
            }
            catch (JsonInvalidTypeException) { return defaultValue; }    
        }

        /// <summary>
        /// Convert the json item to string and append it to a StringBuilder. 
        /// </summary>
        /// 2024.3.7
        /// version 1.0.2
        /// <param name="result"></param>
        internal virtual void AddStringToStringBuilder(StringBuilder result)
        {
            result.Append(ToString());
        }

        /// <summary>
        /// Convert the json item to string and append it to a StringBuilder. 
        /// For JsonObject and JsonArray, override this method to convert it to formatted string.
        /// JsonString is often-used in JsonObject. Therefore, for JsonString, we override this method to avoid too many string objects are generated when convert a JsonObject to string.
        /// For types except JsonObject and JsonArray, param "level" is not necessary.
        /// </summary>
        /// 2024.3.7
        /// version 1.0.2
        /// <param name="result"></param>
        /// <param name="level"></param>
        internal virtual void AddFormattedStringToStringBuilder(StringBuilder result, int level = 0)
        {
            AddStringToStringBuilder(result);
        }

        /// <summary>
        /// Get a value by path. If succeed, the method returns true and outputs the found item.
        /// The elements of path should be string. For JsonArray, its index should be convert to string.
        /// For example, for json object obj = {"obj1": {"arr1": ["value0", "value1", "value2"]}},
        /// this method will get JsonString "value0" by a path {"obj1", "arr1", "0"}.
        /// It is the same as obj.Get<JsonObject>("obj1").Get<JsonArray>("arr1").Get<JsonItem>(0).
        /// </summary>
        /// 2024.4.4
        /// version 1.0.3
        /// <param name="separator"></param>
        /// <param name="path"></param>
        /// <param name="item"></param>
        /// <returns>True if succeed</returns>
        protected bool TryGetItemByPath(string[] path, [MaybeNullWhen(false)] out JsonItem item)
        {
            JsonItem now = this;
            item = null;

            foreach (string element in path)
            {
                if (now is JsonObject)
                {
                    JsonObject obj_now = (JsonObject)now;
                    if (!obj_now.ContainsKey(element)) 
                        return false;
                    now = obj_now.Get<JsonItem>(element);
                }
                else if (now is JsonArray)
                {
                    JsonArray arr_now = (JsonArray)now;
                    int int_node;
                    if (!int.TryParse(element, out int_node))
                        return false;
                    if (int_node >= arr_now.Count)
                        return false;
                    now = arr_now.Get<JsonItem>(int_node);
                }
                else
                    return false;
            }
            item = now;
            return true;
        }

    }
}
