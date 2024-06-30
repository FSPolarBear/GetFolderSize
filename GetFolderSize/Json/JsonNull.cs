namespace Json
{
    /// <summary>
    /// Null item of json.
    /// </summary>
    /// 2024.1.4
    /// version 1.0.0
    public class JsonNull : JsonItem
    {
        public JsonNull()
        {
            ItemType = JsonItemType.Null;
        }

        public override string ToString()
        {
            return "null";
        }


        /// <summary>
        /// Get the value of the item in the specified type.
        /// </summary>
        /// 2024.1.4
        /// version 1.0.0
        /// <typeparam name="T">JsonItem, JsonNull, nullable types</typeparam>
        /// <returns></returns>
        /// <exception cref="JsonInvalidTypeException"></exception>
        public override T GetValue<T>()
        {
            if(typeof(T) == typeof(JsonItem) || typeof(T) == typeof(JsonNull)) 
                return (T)(object)this; 
            else if(default(T) ==  null)
                // We want this default to be null, but it leads to a warning CS8603. 
                // So we add ! to avoid the warning.
                return default!;
            else
                throw new JsonInvalidTypeException(JsonExceptionMessage.GetInvalidTypeExceptionMessage("Nullable", typeof(T)));
        }

        /// <summary>
        /// Parse a string to JsonNull.
        /// </summary>
        /// 2024.1.4
        /// version 1.0.0
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="JsonFormatException">The string cannot be parsed.</exception>
        public static new JsonNull Parse(string str)
        {
            if (str == "null")
                return new JsonNull();
            throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonNull"));
        }

    }
}
