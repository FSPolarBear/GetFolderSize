using System.Text;

// For testing
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("JsonTest")]
namespace Json
{
    /// <summary>
    /// Json parser.
    /// Parse a substring of a string to JsonItem.
    /// The start position of the substring is given as a parameter, and json parser find the end position of the substring as an out parameter.
    /// For example, for json string str="{\"key\": true}"
    ///   {  "  k  e  y  "  :  t  r  u  e  }
    ///   0  1  2  3  4  5  6  7  8  9  10 11
    /// Given start=1, json parser will output end=5 and return JsonString: "key"
    /// Given start=7, json parser will output end=10 and return JsonBool: true
    /// Given start=0, json parser will output end=11 and return JsonObject: {"key": true}
    /// </summary>
    /// 2024.3.21
    /// version 1.0.2
    internal static class JsonParser
    {
        /// <summary>
        /// Tokens in object and array.
        /// </summary>
        /// 2024.2.26
        /// version 1.0.2
        private enum TokenType
        {
            Key,
            Value,
            Comma,
            Colon,
        }


        /// <summary>
        /// Parse a substring of a string to JsonString.
        /// </summary>
        /// 2024.2.26
        /// version 1.0.2
        /// <param name="str">The string.</param>
        /// <param name="start">Start position of the substring.</param>
        /// <param name="end">End position of the substring.</param>
        /// <returns>JsonString object.</returns>
        /// <exception cref="JsonFormatException">The substring cannot be parsed as a JsonString.</exception>
        public static JsonString ParseString(string str, int start, out int end)
        {
            if (str[start] != '"')
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonString"));
            StringBuilder result = new StringBuilder();
            char c, escaped;
            for (int i = start + 1; i < str.Length; i++)
            {
                c = str[i];
                // The string is end.
                if (c == '"')
                {
                    end = i;
                    return new JsonString(result.ToString());
                }
                // Escape character
                else if (c == '\\')
                {
                    if (i + 1 >= str.Length)
                        throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonString"));
                    escaped = str[i + 1];
                    if (escaped == 'u')
                    {
                        int unicode_int;
                        try
                        {
                            unicode_int = Convert.ToInt32(str.Substring(i + 2, 4), 16);
                        }
                        catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is FormatException)
                        {
                            throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonString"));
                        }
                        i += 5; // str[i+5] is the last character of \u
                        // C# char use UTF-16, so convert it directly.
                        result.Append((char)unicode_int);
                    }
                    else
                    {
                        // This is more efficiency than if(array.Contains(escaped))
                        if (escaped == '"') result.Append('"');
                        else if (escaped == '\\') result.Append('\\');
                        else if (escaped == '/') result.Append('/');
                        else if (escaped == 'b') result.Append("\b");
                        else if (escaped == 'f') result.Append('\f');
                        else if (escaped == 'n') result.Append('\n');
                        else if (escaped == 'r') result.Append('\r');
                        else if (escaped == 't') result.Append('\t');
                        else
                            throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonString"));
                        // i++ to skip the escaped character.
                        // For example, str[i] == '\\', str[i+1] == 'n', it will be escaped as '\n', and the next character is str[i+2], not str[i+1], so here need another i++.
                        i++;
                    }
                }
                // Non-escape character
                else
                {
                    result.Append(c);
                }
            }
            throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonString"));
        }

        /// <summary>
        /// Parse a substring of a string to JsonInteger or JsonFloat.
        /// </summary>
        /// 2024.3.21
        /// version 1.0.2
        /// <param name="str">The string.</param>
        /// <param name="start">Start position of the substring.</param>
        /// <param name="end">End position of the substring.</param>
        /// <param name="force_float">If true, the method will return a JsonFloat, even if the value is an integer.</param>
        /// <returns>JsonInteger or JsonFloat object.</returns>
        /// <exception cref="JsonFormatException">The substring cannot be parsed as a JsonInteger or JsonFloat.</exception>
        public static JsonItem ParseNumber(string str, int start, out int end, bool force_float=false)
        {
            // We change the inputted string to a simple form, and then use long.TryParse or decimal.TryParse to parse it.
            // For example, "100.00" -> "100", "1230e-3" -> "1.23".
            // First, we change the inputted number n to integers i and e, where n == i * 10^e and i % 10 != 0
            // Then, according to e, we find the position of point (if e < 0) or how many 0 should be padded (if e > 0).
            int i;
            bool is_negative = false;
            List<char> numbers = new List<char>();
            int exponent = 0;


            // The first character can be '-'
            i = start;
            if (str[i] == '-')
            {
                is_negative = true;
                i++;
            }

            // Then, the character should be a number.
            // There should be no number after 0, but there can be numbers after 1-9
            if (i >= str.Length)
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage());
            if (str[i] == '0')
                i++;
            else if ('1' <= str[i] && str[i] <= '9')
            {
                while (i < str.Length && '0' <= str[i] && str[i] <= '9')
                {
                    numbers.Add(str[i]);
                    i++;
                }
            }
            else
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage());

            // Then, there can be a point.
            if (i < str.Length && str[i] == '.')
            {
                i++;
                // After point, there can be numbers
                if (i >= str.Length)
                    throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage());
                while (i < str.Length && '0' <= str[i] && str[i] <= '9')
                {
                    numbers.Add(str[i]);
                    exponent--;
                    i++;
                }
            }

            // Then, there can be 'e' or 'E' that indicates exponent.
            if (i < str.Length && (str[i] == 'e' || str[i] == 'E'))
            {
                bool e_is_negative = false;
                int e = 0;
                i++;
                // After 'e' or 'E', there can be '+' or '-', or no sign.
                if (str[i] == '-')
                {
                    i++;
                    e_is_negative = true;
                }
                else if (str[i] == '+')
                    i++;
                // Then, there should be numbers.
                if (i < str.Length && '0' <= str[i] && str[i] <= '9')
                {
                    while (i < str.Length && '0' <= str[i] && str[i] <= '9')
                    {
                        e = e * 10 + str[i] - '0';
                        i++;
                    }
                }
                else
                    throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage());
                if (e_is_negative)
                    e = -e;
                exponent += e;
            }

            end = i - 1;
            int j;
            // Remove 0 at the end, and add the number of removed 0 to exponent.
            for (j = numbers.Count - 1; j >= 0 && numbers[j] == '0'; j--) ;
            j++;
            // If all numbers are 0 (e.g. 0.00), the value is 0.
            if (j == 0)
            {
                return new JsonInteger(0);
            }
            exponent += numbers.Count - j;
            numbers.RemoveRange(j, numbers.Count - j);
            // When exponent >= 0, the value is an integer.
            if (exponent >= 0)
            {
                long value;
                if (exponent > 0)
                    numbers.AddRange(Enumerable.Repeat('0', exponent));
                bool is_succeed = long.TryParse(new string(numbers.ToArray()), out value);
                if (is_succeed)
                {
                    if (is_negative)
                        value = -value;
                    if (force_float)
                        return new JsonFloat(value);
                    else
                        return new JsonInteger(value);
                }
                else
                    throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage());
            }
            // When exponent < 0, the value is not an integer.
            else // exponet < 0
            {
                string value_string;
                // If point is in the number, just insert it. For example, 123e-1 -> 12.3
                if (-exponent < numbers.Count)
                {
                    numbers.Insert(numbers.Count + exponent, '.');
                    value_string = new string(numbers.ToArray());
                }

                // If point is before the number, pad 0. For example, 123e-4 -> 0.0123
                else
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append('0'); builder.Append('.');
                    if (numbers.Count + exponent < 0)
                    {
                        builder.Append('0', -numbers.Count - exponent);
                    }
                    builder.Append(numbers.ToArray());
                    value_string = builder.ToString();
                }
                decimal value;
                bool is_succeed = decimal.TryParse(value_string, out value);
                if (is_succeed)
                {
                    if (is_negative)
                        value = -value;
                    return new JsonFloat(value);
                }
                else
                    throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage());
            }
        }



        /// <summary>
        /// Parse a substring of a string to JsonArray.
        /// </summary>
        /// 2024.3.1
        /// version 1.0.2
        /// <param name="str">The string.</param>
        /// <param name="start">Start position of the substring.</param>
        /// <param name="end">End position of the substring.</param>
        /// <returns>JsonArray object.</returns>
        /// <exception cref="JsonFormatException">The substring cannot be parsed as a JsonArray.</exception>
        public static JsonArray ParseArray(string str, int start, out int end)
        {
            if (str[start] != '[')
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonArray"));
            // Type of the next token
            TokenType nextToken = TokenType.Value;
            // If endable == true, the next token can be ']' to end the array.
            bool endable = true;

            JsonArray result = new JsonArray();

            for (int i = start + 1; i < str.Length; i++)
            {
                // Skip blank characters
                for (; i < str.Length && (str[i] == ' ' || str[i] == '\n' || str[i] == '\r' || str[i] == '\t'); i++) ;
                if (i >= str.Length)
                    throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonArray"));
                
                // ']' to end the array
                if (endable && str[i] == ']')
                {
                    end = i;
                    return result;
                }

                if (nextToken == TokenType.Value) 
                { 
                    result.Add(ParseItem(str, i, out i));
                    nextToken = TokenType.Comma;
                    endable = true;
                }
                else if (nextToken == TokenType.Comma)
                {
                    if (str[i] != ',')
                        throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonArray"));
                    nextToken = TokenType.Value;
                    endable = false;
                }


            }
            throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonArray"));
        }

        /// <summary>
        /// Parse a substring of a string to JsonObject.
        /// </summary>
        /// 2024.3.1
        /// version 1.0.2
        /// <param name="str">The string.</param>
        /// <param name="start">Start position of the substring.</param>
        /// <param name="end">End position of the substring.</param>
        /// <returns>JsonObject object.</returns>
        /// <exception cref="JsonFormatException">The substring cannot be parsed as a JsonObject.</exception>
        public static JsonObject ParseObject(string str, int start, out int end)
        {
            if (str[start] != '{')
                throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonObject"));
            // Type of the next token
            TokenType nextToken = TokenType.Key;
            // If endable == true, the next token can be '}' to end the object.
            bool endable = true;

            JsonObject result = new JsonObject();
            JsonString key = null!; // A not-null value will be assigned to key before it is used. 

            for (int i = start + 1; i < str.Length; i++)
            {
                // Skip blank characters
                for (; i < str.Length && (str[i] == ' ' || str[i] == '\n' || str[i] == '\r' || str[i] == '\t'); i++) ;
                if (i >= str.Length)
                    throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonObject"));

                // '}' to end the object
                if (endable && str[i] == '}')
                {
                    end = i;
                    return result;
                }

                if (nextToken == TokenType.Key)
                {
                    key = ParseString(str, i, out i);
                    nextToken = TokenType.Colon;
                    endable = false;
                }
                else if (nextToken == TokenType.Colon)
                {
                    if (str[i] != ':')
                        throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonObject"));
                    nextToken = TokenType.Value;
                    endable = false;
                }
                else if (nextToken == TokenType.Value)
                {
                    result[key] = ParseItem(str, i, out i);
                    nextToken = TokenType.Comma;
                    endable = true;
                }
                else if (nextToken == TokenType.Comma)
                {
                    if (str[i] != ',')
                        throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonObject"));
                    nextToken = TokenType.Key;
                    endable = false;
                }
            }
            throw new JsonFormatException(JsonExceptionMessage.GetFormatExceptionMessage("JsonObject"));
        }

        /// <summary>
        /// Parse a substring of a string to JsonItem.
        /// </summary>
        /// 2024.3.1
        /// version 1.0.2
        /// <param name="str">The string.</param>
        /// <param name="start">Start position of the substring.</param>
        /// <param name="end">End position of the substring.</param>
        /// <returns>JsonItem object.</returns>
        /// <exception cref="JsonFormatException">The substring cannot be parsed as a JsonItem.</exception>
        public static JsonItem ParseItem(string str, int start, out int end)
        {
            // true
            if (start + 3 < str.Length && str[start] == 't' && str[start + 1] == 'r' && str[start + 2] == 'u' && str[start + 3] == 'e')
            {
                end = start + 3;
                return new JsonBool(true);
            }
            // false
            else if (start + 4 < str.Length && str[start] == 'f' && str[start + 1] == 'a' && str[start + 2] == 'l' && str[start + 3] == 's' && str[start + 4] == 'e')
            {
                end = start + 4;
                return new JsonBool(false);
            }
            // null
            else if (start + 3 < str.Length && str[start] == 'n' && str[start + 1] == 'u' && str[start + 2] == 'l' && str[start + 3] == 'l')
            {
                end = start + 3;
                return new JsonNull();
            }
            // string
            else if (str[start] == '"')
            {
                return ParseString(str, start, out end);
            }
            // array
            else if (str[start] == '[')
            {
                return ParseArray(str, start, out end);
            }
            // object
            else if (str[start] == '{')
            {
                return ParseObject(str, start, out end);
            }
            // number
            else
            {
                return ParseNumber(str, start, out end);
            }
        }

    }
}
