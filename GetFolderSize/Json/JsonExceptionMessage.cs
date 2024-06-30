namespace Json
{
    /// <summary>
    /// 
    /// </summary>
    /// 2024.6.29
    /// version 1.0.4
    internal static class JsonExceptionMessage
    {

        public static string GetInvalidTypeExceptionMessage(string[] needed, Type recieved)
        {
            return string.Join(" or ", needed) + " are needed, but " + recieved + " is recieved.";
        }

        public static string GetInvalidTypeExceptionMessage(string needed, Type recieved)
        {
            return needed + " is needed, but " + recieved + " is recieved.";
        }

        public static string GetInvalidTypeExceptionNoTypeMessage()
        {
            return "The json object dose not have a string value for key \"__Type\", so it cannot be convert to IJsonObject.";
        }

        public static string GetInvalidTypeExceptionTypeIsInvalidMessage(string type_name)
        {
            return string.Format("The type {0} is invalid.", type_name);
        }

        public static string GetInvalidTypeExceptionShouldHaveConstructorWithJsonObjectMessage(Type type)
        {
            return string.Format("The type {0} should have a constractor \"public {1}(JsonObject json);\".", type.FullName, type.FullName);
        }

        public static string GetInvalidTypeExceptionTypeCannotBeConvertedMessage(Type type, Type target_type)
        {
            return string.Format("This json object is an object for class {0}, and it cannot be converted to {1}.", target_type.FullName, type.FullName);
        }

        public static string GetFormatExceptionMessage(string name)
        {
            return "The inputted string cannot be parsed to " + name + ".";
        }
        public static string GetFormatExceptionMessage()
        {
            return "The inputted string cannot be parsed.";
        }

        public static string GetInvalidPathMessage()
        {
            return "The path is invalid.";
        }

        public static string GetInvalidTypeExceptionKeyShouldBeStringMessage()
        {
            return "If you want to create a JsonObject by a Dictionary, the keys should be string or JsonString.";
        }

    }
}
