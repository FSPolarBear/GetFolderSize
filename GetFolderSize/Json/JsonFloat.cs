namespace Json
{
    /// <summary>
    /// Non-integer number item of json.
    /// </summary>
    /// 2024.3.21
    /// version 1.0.2
    public class JsonFloat : JsonItem
    {
        private decimal value;
        public JsonFloat(decimal value) {
            ItemType = JsonItemType.Float;
            this.value = value; 
        }

        /// <summary>
        /// Get the value of the item in the specified type.
        /// </summary>
        /// 2024.1.3
        /// version 1.0.0
        /// <typeparam name="T">JsonItem, JsonFloat, decimal, double, float</typeparam>
        /// <returns></returns>
        /// <exception cref="JsonInvalidTypeException">The type is invalid.</exception>
        public override T GetValue<T>()
        {
            Type type = Utils.GetActualType(typeof(T));
            if (typeof(T) == typeof(JsonItem) || typeof(T) == typeof(JsonFloat))
            {
                return (T)(object)this;
            }
            else if (type == typeof(decimal))
            {
                return (T)(object)value;
            }
            else if (type == typeof(double))
            {
                return (T)(object)(double)value;
            }
            else if (type == typeof(float))
            {
                return (T)(object)(float)value;
            }
            else
                throw new JsonInvalidTypeException(JsonExceptionMessage.GetInvalidTypeExceptionMessage(new string[] { "decimal", "double", "float" }, type));
        }

        public override string ToString() { return value.ToString(); }

        /// <summary>
        /// Parse a string to JsonFloat.
        /// </summary>
        /// 2024.3.21
        /// version 1.0.2
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="JsonFormatException">The string cannot be parsed.</exception>
        public static new JsonFloat Parse(string str)
        {
            int end;
            JsonItem item;
            try
            {
                item = JsonParser.ParseNumber(str, 0, out end, force_float: true);
            }
            catch (Exception)
            {
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonFloat"));
            }
            JsonFloat? result = item as JsonFloat;
            if (end != str.Length - 1 || result == null)
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonFloat"));
            return result;
        }
    }
}
