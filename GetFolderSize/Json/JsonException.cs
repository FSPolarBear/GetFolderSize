namespace Json
{
    /// <summary>
    /// Exception of json.
    /// </summary>
    /// 2024.1.3
    /// version 1.0.0
    public class JsonException : Exception
    {
        public JsonException() { }
        public JsonException(string? message) : base(message) { }
        public JsonException(string? message, Exception? innerException) : base(message, innerException) { }

    }
    /// <summary>
    /// The exception that is thrown when get value in an invalid type or create a json item by a value with an invalid type.
    /// </summary>
    /// 2024.1.3
    /// version 1.0.0
    public class JsonInvalidTypeException : JsonException
    {
        public JsonInvalidTypeException() { }
        public JsonInvalidTypeException(string? message) : base(message) { }
        public JsonInvalidTypeException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// The exception that is thrown when failed to parse a string to a json item.
    /// </summary>
    /// 2024.1.3
    /// version 1.0.0
    public class JsonFormatException: JsonException 
    {
        public JsonFormatException() { }
        public JsonFormatException(string? message) : base(message) { }
        public JsonFormatException(string? message, Exception? innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 
    /// </summary>
    /// 2024.4.3
    /// version 1.0.3
    public class JsonInvalidPathException: JsonException
    {
        public JsonInvalidPathException() { }
        public JsonInvalidPathException(string? message) : base(message) { }
        public JsonInvalidPathException(string? message, Exception? innerException) : base(message, innerException) { }
    }

}
