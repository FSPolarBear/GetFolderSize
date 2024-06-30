using System.Text;

namespace Json
{
    /// <summary>
    /// String item of json.
    /// </summary>
    /// 2024.5.19
    /// version 1.0.3
    public class JsonString : JsonItem
    {

        private string value;

        public JsonString(string value)
        {
            ItemType = JsonItemType.String;
            this.value = value;
        }

        public JsonString(char value): this(value.ToString()) { }

        /// <summary>
        /// Parse a string to JsonString (i.e. decode a JsonString).
        /// </summary>
        /// 2024.3.6
        /// version 1.0.2
        /// <param name="str"></param>
        /// <returns></returns>
        public static new JsonString Parse(string str)
        {
            if (!(str.StartsWith('"') && str.EndsWith('"')))
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonString"));
            int end;
            JsonString result = JsonParser.ParseString(str, 0, out end);
            if (end != str.Length - 1)
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonString"));
            return result;
        }

        /// <summary>
        /// Get the value of the item in the specified type.
        /// </summary>
        /// 2024.5.19
        /// version 1.0.3
        /// <typeparam name="T">JsonItem、JsonInteger、string</typeparam>
        /// <returns></returns>
        /// <exception cref="JsonInvalidTypeException">The type is invalid.</exception>
        public override T GetValue<T>()
        {
            if (typeof(T) == typeof(JsonItem) || typeof(T) == typeof(JsonString))
                return (T)(object)this;
            if (typeof(T) == typeof(string))
                return (T)(object)value;
            if (typeof(T) == typeof(char) && value.Length == 1)
                return (T)(object)value[0];
            else
                throw new JsonInvalidTypeException(JsonExceptionMessage.GetInvalidTypeExceptionMessage("String", typeof(T)));
        }



        /// <summary>
        /// Convert the JsonString to string and append it to a StringBuilder. 
        /// </summary>
        /// 2024.4.23
        /// version 1.0.2
        /// <param name="result"></param>
        /// <param name="level"></param>
        internal override void AddStringToStringBuilder(StringBuilder result)
        {
            result.Append('"');
            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case '\\': result.Append("\\\\"); break;
                    case '"': result.Append("\\\""); break;
                    case '\b': result.Append("\\b"); break;
                    case '\f': result.Append("\\f"); break;
                    case '\n': result.Append("\\n"); break;
                    case '\r': result.Append("\\r"); break;
                    case '\t': result.Append("\\t"); break;
                    default:
                        if (JsonConfig.EnsureAscii && !Char.IsAscii(value[i]))
                        {
                            result.Append("\\u");
                            result.Append(Convert.ToString((int)value[i], 16));
                        }
                        else
                            result.Append(value[i]);
                        break;
                }
            }
            result.Append('"');
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.3.21
        /// version 1.0.2
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            AddStringToStringBuilder(result);
            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.1.3
        /// version 1.0.0
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            JsonString? jstring = obj as JsonString;
            if (jstring == null)
                return false;
            return jstring.value == value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.1.3
        /// version 1.0.0
        /// <returns></returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }

}
