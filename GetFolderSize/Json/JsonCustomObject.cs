using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    /// <summary>
    /// 
    /// </summary>
    /// 2024.6.18
    /// version 1.0.4
    public class JsonCustomObject : JsonObject
    {
        public JsonCustomObject(IJsonObject value): base(value.ToJson().GetValue<Dictionary<JsonString, JsonItem>>()) { this["__Type"] = value.GetType().FullName; }
        public override T GetValue<T>()
        {
            Type type = typeof(T);
            if (!type.GetInterfaces().Contains(typeof(IJsonObject)))
                return base.GetValue<T>();

            if (!ContainsKey("__Type"))
                throw new Exception();
            Type? target_type = Assembly.GetExecutingAssembly().GetType(Get<string>("__Type", ""));
            if (target_type == null) 
                throw new Exception();
            if (!target_type.IsSubclassOf(type))
                throw new Exception();


            ConstructorInfo? constructor = target_type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, new Type[] { typeof(JsonObject) });
            if (constructor == null)
                throw new Exception();
            T result = (T)constructor.Invoke(new object[] { GetValue<JsonObject>()});
            return result;
        }
    }
}
