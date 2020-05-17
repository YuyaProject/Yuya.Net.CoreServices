using System;

namespace Yuya.Net.CoreServices.Json
{
  /// <summary>
  /// Json Converter Interface
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IJsonConverter<T>
  {
    /// <summary>
    /// Serializes the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    string Serialize(T value);

    /// <summary>
    /// Deserializes the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <returns></returns>
    T Deserialize(string jsonString);
  }

  /// <summary>
  /// Json Converter Class
  /// </summary>
  public interface IJsonConverter
  {
    /// <summary>
    /// Serializes the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    string Serialize<T>(T value);

    /// <summary>
    /// Serializes the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    string Serialize(object value);

    /// <summary>
    /// Deserializes the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <returns></returns>
    T Deserialize<T>(string jsonString);

    /// <summary>
    /// Deserializes the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <returns></returns>
    object Deserialize(string jsonString);

    /// <summary>
    /// Deserializes the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    object Deserialize(string jsonString, Type destinationType);

    /// <summary>
    /// Deserialize2s the specified json string.
    /// </summary>
    /// <param name="jsonString">The json string.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    object Deserialize2(string jsonString, Type destinationType);
  }
}
