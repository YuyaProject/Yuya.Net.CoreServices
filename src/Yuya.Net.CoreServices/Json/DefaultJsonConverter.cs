using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
using Yuya.Net.CoreServices.Mappings;

namespace Yuya.Net.CoreServices.Json
{
  /// <summary>
  /// Default Json Converter
  /// </summary>
  /// <seealso cref="TurkuazGO.Json.IJsonConverter" />
  public class DefaultJsonConverter : IJsonConverter
  {
    private readonly IMapperService _mapperService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultJsonConverter"/> class.
    /// </summary>
    /// <param name="mapperService">The mapper service.</param>
    public DefaultJsonConverter(IMapperService mapperService)
    {
      _mapperService = mapperService;
    }

    /// <summary>
    /// Deserialize2s the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    public object Deserialize2(string jsonString, Type destinationType)
    {
      if (destinationType.IsClass && !destinationType.IsAbstract)
      {
        return JsonConvert.DeserializeObject(jsonString, destinationType);
      }
      dynamic anonymousValue = JObject.Parse(jsonString);
      if (anonymousValue != null)
      {
        var mapper = _mapperService.SimpleMapper;
        var methodBase = typeof(AutoMapper.IMapper).GetMethods(BindingFlags.Public | BindingFlags.Instance)
          .FirstOrDefault(x => x.Name == "Map" && x.IsGenericMethod && x.GetGenericArguments().Length == 1 && x.GetParameters().Length == 1);

        var method = methodBase.MakeGenericMethod(destinationType);

        return method.Invoke(mapper, new object[] { anonymousValue });
      }
      return default;
    }

    /// <summary>
    /// Deserializes the specified json string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonString">The json string.</param>
    /// <returns></returns>
    public T Deserialize<T>(string jsonString)
    {
      var type = typeof(T);
      if (type.IsClass && !type.IsAbstract)
      {
        return JsonConvert.DeserializeObject<T>(jsonString);
      }
      var anonymousValue = JsonConvert.DeserializeObject(jsonString);
      if (anonymousValue != null)
      {
        return _mapperService.SingleMap<T>(anonymousValue);
      }
      return default;
    }

    /// <summary>
    /// Deserializes the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    public object Deserialize(string jsonString, Type destinationType)
    {
      if (destinationType.IsClass && !destinationType.IsAbstract)
      {
        return JsonConvert.DeserializeObject(jsonString, destinationType);
      }
      var anonymousValue = JsonConvert.DeserializeObject(jsonString);
      if (anonymousValue != null)
      {
        return _mapperService.SingleMap(anonymousValue, destinationType);
      }
      return default;
    }

    /// <summary>
    /// Deserializes the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <returns></returns>
    public object Deserialize(string jsonString)
    {
      return JsonConvert.DeserializeObject(jsonString);
    }

    /// <summary>
    /// Serializes the specified value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public string Serialize<T>(T value)
    {
      return JsonConvert.SerializeObject(value);
    }

    /// <summary>
    /// Serializes the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public string Serialize(object value)
    {
      return JsonConvert.SerializeObject(value);
    }
  }
}
