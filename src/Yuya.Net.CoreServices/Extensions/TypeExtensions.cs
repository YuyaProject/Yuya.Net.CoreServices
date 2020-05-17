using System;
using System.Collections.Generic;
using System.Linq;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Type Extensions
  /// </summary>
  public static class TypeExtensions
  {

    /// <summary>
    /// Determines whether the specified collection is in.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value.</param>
    /// <param name="collection">The collection.</param>
    /// <returns>
    ///   <c>true</c> if the specified collection is in; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsIn<T>(this T value, IEnumerable<T> collection)
    {
      return collection.Contains(value);
    }
  }
}