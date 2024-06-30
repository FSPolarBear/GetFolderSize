# CSharpJson
This is a json library for C#. 

## version
1.0.3

## Support
.Net 6.0
## Usage
To use this library, import json.dll or copy the code of this project to your project, and then
``` 
using Json
```
We provide class `JsonObject` as the object in json and class `JsonArray` as the array in json. We use class `JsonItem` as the item in json.
### Supported type of item
These types are supported as an item of json:

| Json type | C# type                                  |
|:----------|:-----------------------------------------|
| object    | JsonObject, Dictionary\<string, T>       |
| array     | JsonArray, Array, List\<T>               |
| string    | string                                   |
| string (Length == 1)| string, char |
| number (integer)    | long, int, short, decimal, double, float |
| number    | decimal, double, float                   |
| true      | bool                                     |
| false     | bool                                     |
| null      | null                                     |

where T is any type supported as an item of json.<br>
Notice that we use `long` to store integers, whose range is `-9,223,372,036,854,775,808` to `9,223,372,036,854,775,807`; we use `decimal` to store non-integer numbers, whose range is `±1.0 x 10^-28` to `±7.9228 x 10^28`. Out-of-range numbers are not supported.<br>

### JsonItem
#### Create a JsonItem
You can create a JsonItem from a value by `CreateFromValue` or parse a string to a JsonItem by `Parse`.
```
//public static JsonItem CreateFromValue(object? value);
JsonItem item1 = JsonItem.CreateFromValue("value1");
JsonItem item2 = JsonItem.CreateFromValue(1.5);
JsonItem item3 = JsonItem.CreateFromValue(true);
JsonItem item4 = JsonItem.CreateFromValue(new object?[]{"value2", 2, false});
JsonItem item5 = JsonItem.CreateFromValue(new Dictionary<string, object?>(){{"key1", null}});

//public static JsonItem Parse(string str);
JsonItem item6 = JsonItem.Parse("\"value1\"");
JsonItem item7 = JsonItem.Parse("1.5");
JsonItem item8 = JsonItem.Parse("true");
JsonItem item9 = JsonItem.Parse("{\"key1\": [0, 1, 2.5, true]}");
```

#### Get value from a JsonItem
You can get the value in the specified type by `GetValue`. You can provide a default value, and the default value will be return when the specified type is not valid for the item.
```
JsonItem item = JsonItem.CreateFromValue(1.5);

// public T GetValue<T>();
double v1 = item.GetValue<double>(); // v1: 1.5

// public T GetValue<T>(T defaultValue);
double v2 = item.GetValue<double>(0.0); // v2: 1.5
string v3 = item.GetValue<string>("default"); // v3: "default"

```

---

### JsonObject
#### Create a JsonObject
You can create a JsonObject in a similar way to create a Dictionary. 
```
JsonObject obj1 = new JsonObject();
JsonObject obj2 = new JsonObject()
{
    {"key1", "value1"},
    {"key2", 0 },
    {"key3", true}, 
    {"key4", null}
};
```
You can also parse a string to a JsonObject by `Parse`.
```
string str = "{\"key1\": \"value1\", \"key2\": 0, \"key3\": true, \"key4\": null}";

// public static JsonObject Parse(string str);
JsonObject obj3 = JsonObject.Parse(str);
```

#### Set value to a JsonObject
You can add an item by `Add`.
```
JsonObject obj = new JsonObject();

// public void Add(string key, object? value);
obj.Add("key1", "value1");
obj.Add("key2", 0);
obj.Add("key3", true);
obj.Add("key4", null);
```
You can also add or modify an item by key.
```
JsonObject obj = new JsonObject();

// public object? this[string key]{ get; set; }
obj["key1"] = "value1";
obj["key1"] = "modified_value";
obj["key2"] = 0;
obj["key3"] = true;
```

#### Get value from a JsonObject
You can get a value in the specified type by a key by `Get`. You can provide a default value, and the default value will be return when the key is not found or the specified type is not valid for the item.
```
JsonObject obj = new JsonObject() { { "key", 1.5 } };

// public T Get<T>(string key);
double v1 = obj.Get<double>("key"); // v1: 1.5

// public T Get<T>(string key, T defaultValue);
double v2 = obj.Get<double>("key", 0.0); // v2: 1.5
string v3 = obj.Get<string>("key", "default"); // v3: "default"
```
You can also get the JsonItem by key and then get value by `JsonItem.GetValue` in these three ways.
```
JsonObject obj = new JsonObject() { { "key", 1.5 } };

double v1 = ((JsonItem)obj["key"]).GetValue<double>(); // v1: 1.5
double v2 = JsonItem.CreateFromValue(obj["key"]).GetValue<double>(); // v2: 1.5
double v3 = obj.GetValue<Dictionary<JsonString, JsonItem>>()["key"].GetValue<double>(); // v3: 1.5
```

You can get the JsonItem by path in multi-JsonObject/JsonArray by `GetByPath`.
```
JsonObject obj = JsonObject.Parse("{\"key1\": {\"key2\": {\"key3\": 1}}}");

// public T GetByPath<T>(string[] path);
int v1 = obj.GetByPath<int>(new string[]{"key1", "key2", "key3"}); // v1: 1

// public T GetByPath<T>(string[] path, T defaultValue);
int v2 = obj.GetByPath<int>(new string[]{"incorrect_path"}, 0); // v2: 0

```

#### Convert to string
You can convert a JsonObject to string by `ToString` or `ToFormattedString`.
```
JsonObject obj = new JsonObject()
{
    {"key1", "value1"},
    {"key2", 0},
    {"key3", true},
    {"key4", null}
};

// public string ToString();
string str1 = obj.ToString();
/* 
str1 is a string:
 { "key1": "value1", "key2": 0, "key3": true, "key4": null}
 */

// public string ToFomattedString();
string str2 = obj.ToFormattedString();
/*
str2 is a string: 
{
    "key1": "value1",
    "key2": 0,
    "key3": true,
    "key4": null
}
*/
```

#### Convert to dictionary
You can convert a JsonObject to a dictionary with keys in string and values in a specified type by `ToDictionary`. All values of the JsonObject should be in the specified type.
```
JsonObject obj = new JsonObject()
{
    {"key1", 0},
    {"key2", 1},
    {"key3", 2},
    {"key4", 3}
};

// public Dictionary<string, T> ToDictionary<T>();
Dictionary<string, int> dict = obj.ToDictionary<int>();
```

#### Other fields and methods
JsonObject provides some fields and methods that are similar to Dictionary as following.
```
public string[] Keys;
public int Count;
public bool ContainsKey(string key);
public bool Remove(string key);
```

---

### JsonArray
#### Create a JsonArray
You can create a JsonArray in a similar way to create a List, or create a JsonArray by an array.
```
JsonArray arr1 = new JsonArray();
JsonArray arr2 = new JsonArray() { "value1", 0, true, null};
object?[] objects = new object?[]{ "value1", 0, true, null};
JsonArray arr3 = new JsonArray(objects);
```
You can also parse a string to a JsonArray by `Parse`.
```
string str = "[\"value1\", 1.5, true, null]";

// public static JsonArray Parse(string str);
JsonArray arr3 = JsonArray.Parse(str);
```

#### Set Value to a JsonArray
You can add an item by `Add`.
```
JsonArray arr = new JsonArray();

// public void Add(object? value);
arr.Add("value1");
arr.Add(1.5);
arr.Add(true);
```

You can modify an item by index.
```
JsonArray arr = new JsonArray() { "value1", 1.5, true, null };

// public object? this[int index]{ get; set; }
arr[1] = "modified_value";
```

You can get the JsonItem by path in multi-JsonObject/JsonArray by `GetByPath`.
```
JsonArray arr = JsonArray.Parse("[true, [{\"key1\": [12345]}], false]");

// public T GetByPath<T>(string[] path);
int v1 = arr.GetByPath<int>(new string[]{"1", "0", "key1", "0"}); // v1: 12345

// public T GetByPath<T>(string[] path, T defaultValue);
int v2 = arr.GetByPath<int>(new string[]{"incorrect_path"}, 0); // v2: 0

```

#### Get value from a JsonArray
You can get a value in the specified type by a index by `Get`. You can provide a default value, and the default value will be return when the index is out of range or the specified type is not valid for the item.
```
JsonArray arr = new JsonArray() { "value1", 1.5, true, null };

// public T Get<T>(int index);
double v1 = arr.Get<double>(0); // v1: 1.5

// public T Get<T>(int index, T defaultValue);
double v2 = obj.Get<double>(0, 0.0); // v2: 1.5
string v3 = obj.Get<string>(0, "default"); // v3: "default"
```

You can also get the JsonItem by index and then get value by `JsonItem.GetValue` in these three ways.
```
JsonArray arr = new JsonArray() { 1.5 };

double v1 = ((JsonItem)arr[0]).GetValue<double>(); // v1: 1.5
double v2 = JsonItem.CreateFromValue(arr[0]).GetValue<double>(); // v2: 1.5
double v3 = arr.GetValue<List<JsonItem>>()[0].GetValue<double>(); // v3: 1.5
```

#### Convert to string
You can convert a JsonObject to string by `ToString` or `ToFormattedString`.
```
JsonArray arr = new JsonArray() { "value1", 1.5, true, null};

// public string ToString();
string str1 = arr.ToString();
/*
str1 is a string:
["value1", 1.5, true, null]
*/

// public string ToFormattedString();
string str2 = arr.ToFormattedString();
/*
str2 is a string:
[
    "value1",
    1.5,
    true,
    null
]
*/
```

#### Convert to list or array
You can convert a JsonArray to a list or an array with elements in a specified type by `ToList` or `ToArray`. All elements of the JsonArray should be in the specified type.
```
JsonArray arr = new JsonArray{0, 1, 2, 3};

// public List<T> ToList<T>();
List<int> list = arr.ToList<int>();

// public T[] ToArray<T>();
int[] array = arr.ToArray<int>();
```

#### Other fields and methods
JsonArray provides some fields and methods that are similar to List as following.
```
public int Count;
public void RemoveAt(int index);
public void Insert(int index, object? item);
```

---

### JsonConfig
Class `JsonConfig` provides some configs.
#### EnsureAscii
If `EnsureAscii` is true, all non-ascii characters will be escaped by \u in `ToString` and `ToFormattedString`.
```
// public static bool EnsureAscii = false;

JsonItem item = JsonItem.CreateFromValue("\u5000");
JsonConfig.EnsureAscii = true;
string str1 = item.ToString(); // str1: @"\u5000"
JsonConfig.EnsureAscii = false;
string str2 = item.ToString(); // str1: "倀"
```

---

### Exceptions
#### JsonInvalidTypeException
A JsonInvalidTypeException will be thrown when getting value with an invalid type.
```
JsonItem item = JsonItem.CreateFromValue("This is not an integer");
int v = item.GetValue<int>(); // A JsonInvalidTypeException will be thrown.
```
A JsonInvalidTypeException will be thrown when creating JsonItem by a value with an unsupported type.
```
class SomeClass{}
JsonIten item = JsonItem.CreateFromValue(new SomeClass()); // A JsonInvalidTypeException will be thrown.
```
A JsonInvalidTypeException will be thrown when converting a JsonObject to a dictionary or converting a JsonArray to a list or an array, while not all the elements/values are in the specified type.
```
JsonArray arr = new JsonArray(){0, 1, "This is not an integer", 3, 4};
arr.ToList<int>(); // A JsonInvalidTypeException will be thrown.
```
#### JsonFormatException
A JsonFormatException will be thrown when parsing an invalid string.
```
string str = "This string cannot be parsed to JsonObject";
JsonObject obj = JsonObject.Parse(str); // A JsonFormatException will be thrown.
```

## Author
[FSPolarBear](https://github.com/FSPolarBear)