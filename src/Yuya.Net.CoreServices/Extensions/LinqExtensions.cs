using System;
using System.Collections.Generic;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Linq Extensions
  /// </summary>
  public static class LinqExtensions
  {
    /// <summary>
    /// Fors the each.
    /// </summary>
    /// <typeparam name="T">The item type parameter</typeparam>
    /// <param name="list">The list.</param>
    /// <param name="action">The action.</param>
    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
      foreach (var item in list)
      {
        action(item);
      }
    }
  }
}