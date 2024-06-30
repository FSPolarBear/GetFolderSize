using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    /// <summary>
    /// If you want your class to be supported as a json item, you can:
    /// 1. Implement interface IJsonObject with a method "public JsonObject ToJson();";
    /// 2. Add a public constractor with a JsonObject argument to your class.
    /// 
    /// Example:
    /// public class SomeClass(): IJsonObject{
    ///     public SomeClass(){}
    ///     public SomeClass(JsonObject json){
    ///         /* Load fileds from json */
    ///     }
    /// 
    ///     public override JsonObject ToJson(){
    ///         return new JsonObject(){
    ///             /* Save fileds to json */
    ///         }
    ///     }
    /// }
    /// 
    /// SomeClass obj1 = new SomeClass();
    /// 
    /// // "item" is a JsonObject object.
    /// JsonItem item = JsonItem.CreateFromValue(obj1); 
    /// 
    /// // If no constructor "public SomeClass(JsonObject json);", an exception will be thrown.
    /// SomeClass obj2 = item.GetValue<SomeClass>();
    /// IJsonObject obj3 = item.GetValue<IJsonObject>();
    /// </summary>
    /// 2024.6.18
    /// version 1.0.4
    public interface IJsonObject
    {
        JsonObject ToJson();

        // public IJsonObject(JsonObject json);
    }
}
