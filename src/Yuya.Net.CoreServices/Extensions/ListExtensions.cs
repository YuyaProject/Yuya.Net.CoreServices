using System.Collections.Generic;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// The Extensions of List
  /// </summary>
  public static class ListExtensions
  {

    /// <summary>Adds the items.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public static List<T> AddItems<T>(this List<T> list, params T[] items)
    {
      list.AddRange(items);
      return list;
    }

    /// <summary>Adds the item.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public static List<T> AddItem<T>(this List<T> list, T item)
    {
      list.Add(item);
      return list;
    }
  }
}