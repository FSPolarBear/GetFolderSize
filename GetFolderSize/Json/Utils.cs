using System.Reflection;

namespace Json
{
    internal static class Utils
    {
        /// <summary>
        /// Get the actual type of nullable type. For example, int? is inputted, and int is returnd.
        /// if the input is not nullable, return the type itself.
        /// </summary>
        /// 2024.1.3
        /// version 1.0.0
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetActualType(Type type)
        {   
            // int? equals Nullable<int>
            if (!type.IsGenericType) { return type; }
            if (type.GetGenericTypeDefinition()  != typeof(Nullable<>)) { return type;}
            Type[] args = type.GetGenericArguments();
            if (args.Length != 1) {  return type; }
            return args[0];
        }


        /// <summary>
        /// Check whether the given type is List<T>.
        /// </summary>
        /// 2024.3.7
        /// version 1.0.2
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsList(Type type)
        {
            if (!type.IsGenericType)
                return false;
            return type.GetGenericTypeDefinition() == typeof(List<>);
        }

        /// <summary>
        /// Check whether the given type is Dictionary<TKey, TValue> and TKey is string or JsonString.
        /// </summary>
        /// 2024.3.7
        /// version 1.0.2
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDictionaryWithStringKey(Type type) 
        {
            if (!type.IsGenericType)
                return false;
            if (type.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                return false;
            Type[] args = type.GetGenericArguments();
            return args[0] == typeof(string) || args[0] == typeof(JsonString);
        }

        /// <summary>
        /// Get type T of List<T>. If the given type is not List<T>, return null.
        /// </summary>
        /// 2024.6.26
        /// version 1.0.4
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type? GetGenericOfList(Type type)
        {
            if (!type.IsGenericType)
                return null;
            if (type.GetGenericTypeDefinition() != typeof(List<>))
                return null;
            return type.GetGenericArguments()[0];
        }

        /// <summary>
        /// Get type T of Dictionary<string, T> or Dictionary<JsonString, T>. If the given type is neither Dictionary<string, T> nor Dictionary<JsonString, T>, return null.
        /// </summary>
        /// 2024.6.26
        /// version 1.0.4
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type? GetGenericOfDictionaryWithStringKey(Type type)
        {
            if (!type.IsGenericType)
                return null;
            if (type.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                return null;
            Type[] args = type.GetGenericArguments();
            if (args[0] != typeof(string) && args[0] != typeof(JsonString))
                return null;
            return args[1];
        }


        /// <summary>
        /// Run a method with generic, and the generic is a given Type value.
        /// </summary>
        /// 2024.6.26
        /// version 1.0.4
        /// <param name="obj"></param>
        /// <param name="method"></param>
        /// <param name="T"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? RunMethodWithGeneric(object? obj, MethodInfo method, Type[] T, object?[]? args = null)
        {
            MethodInfo target_method = method.MakeGenericMethod(T);
            return target_method.Invoke(obj, args);
        }
    }
}
