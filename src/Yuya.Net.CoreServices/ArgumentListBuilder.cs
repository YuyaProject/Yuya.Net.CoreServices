using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Argument List Builder
  /// </summary>
  public class ArgumentListBuilder<T>
  {
    /// <summary>
    /// The arguments
    /// </summary>
    private readonly List<T> _arguments;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentListBuilder{T}"/> class.
    /// </summary>
    public ArgumentListBuilder()
    {
      _arguments = new List<T>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentListBuilder{T}"/> class.
    /// </summary>
    /// <param name="capacity">The capacity.</param>
    public ArgumentListBuilder(int capacity)
    {
      _arguments = new List<T>(capacity);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentListBuilder{T}"/> class.
    /// </summary>
    /// <param name="list">The list.</param>
    public ArgumentListBuilder(IEnumerable<T> list)
    {
      _arguments = list.ToList();
    }

    /// <summary>
    /// Adds the specified object.
    /// </summary>
    /// <param name="object">The object.</param>
    /// <returns></returns>
    public ArgumentListBuilder<T> Add(T @object)
    {
      _arguments.Add(@object);
      return this;
    }

    /// <summary>
    /// Adds the items.
    /// </summary>
    /// <param name="objects">The objects.</param>
    /// <returns></returns>
    public ArgumentListBuilder<T> AddItems(params T[] objects)
    {
      _arguments.AddRange(objects);
      return this;
    }

    /// <summary>
    /// Adds the specified object.
    /// </summary>
    /// <param name="objects">The objects.</param>
    /// <returns></returns>
    public ArgumentListBuilder<T> AddRange(IEnumerable<T> objects)
    {
      objects.ForEach(x => _arguments.Add(x));
      return this;
    }

    /// <summary>
    /// Ases the enumerable.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> AsEnumerable()
    {
      for (int i = 0; i < _arguments.Count; i++)
      {
        yield return _arguments[i];
      }
    }

    /// <summary>
    /// Ases the array.
    /// </summary>
    /// <returns></returns>
    public T[] ToArray()
    {
      return _arguments.ToArray();
    }

    /// <summary>
    /// Converts to list.
    /// </summary>
    /// <returns></returns>
    public List<T> ToList()
    {
      return _arguments;
    }

    /// <summary>
    /// Froms the specified array.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <returns></returns>
    public static ArgumentListBuilder<T> From(ICollection<T> array)
    {
      var arg = new ArgumentListBuilder<T>();
      arg._arguments.AddRange(array);
      return arg;
    }
  }

  /// <summary>
  /// Argument List Builder
  /// </summary>
  public class ArgumentListBuilder
  {
    /// <summary>
    /// The arguments
    /// </summary>
    private readonly ArrayList _arguments;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentListBuilder"/> class.
    /// </summary>
    public ArgumentListBuilder()
    {
      _arguments = new ArrayList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentListBuilder"/> class.
    /// </summary>
    /// <param name="capacity">The capacity.</param>
    public ArgumentListBuilder(int capacity)
    {
      _arguments = new ArrayList(capacity + 1);
      for (int i = 0; i < capacity; i++)
      {
        _arguments.Add(null);
      }
    }

    /// <summary>
    /// Adds the specified object.
    /// </summary>
    /// <param name="object">The object.</param>
    /// <returns></returns>
    public ArgumentListBuilder Add(object @object)
    {
      _arguments.Add(@object);
      return this;
    }

    /// <summary>
    /// Adds the items.
    /// </summary>
    /// <param name="objects">The objects.</param>
    /// <returns></returns>
    public ArgumentListBuilder AddItems(params object[] objects)
    {
      _arguments.AddRange(objects);
      return this;
    }

    /// <summary>
    /// Adds the specified object.
    /// </summary>
    /// <param name="objects">The objects.</param>
    /// <returns></returns>
    public ArgumentListBuilder AddRange(IEnumerable<object> objects)
    {
      objects.ForEach(x => _arguments.Add(x));
      return this;
    }

    /// <summary>
    /// Merges the specified argument list builder.
    /// </summary>
    /// <param name="argumentListBuilder">The argument list builder.</param>
    /// <param name="mergeStrategy">The merge strategy.</param>
    /// <returns></returns>
    public ArgumentListBuilder Merge(ArgumentListBuilder argumentListBuilder, MergeStrategy mergeStrategy = MergeStrategy.NewWins)
    {
      if (argumentListBuilder == null || argumentListBuilder._arguments.Count == 0) return this;

      return Merge(argumentListBuilder._arguments, mergeStrategy);
    }

    /// <summary>
    /// Merges the specified collection.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="mergeStrategy">The merge strategy.</param>
    /// <returns></returns>
    public ArgumentListBuilder Merge(ICollection collection, MergeStrategy mergeStrategy = MergeStrategy.NewWins)
    {
      if (collection == null || collection.Count == 0) return this;

      var i = -1;

      foreach (object item in collection)
      {
        i++;
        var firstIsNull = (i < _arguments.Count && _arguments[i] == null) || (i >= _arguments.Count);
        var secondIsNull = item == null;
        if ((firstIsNull && secondIsNull) || (!firstIsNull && secondIsNull)) continue;
        if (firstIsNull || (mergeStrategy == MergeStrategy.NewWins))
        {
          _arguments[i] = item;
        }
        // if mergeStrategy == MergeStrategy.OldWins, we won't do anything
      }
      return this;
    }

    /// <summary>
    /// Inserts the collection.
    /// </summary>
    /// <param name="argumentListBuilder">The argument list builder.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns></returns>
    public ArgumentListBuilder InsertCollection(ArgumentListBuilder argumentListBuilder, int startIndex = 0)
    {
      if (argumentListBuilder == null || argumentListBuilder._arguments.Count == 0) return this;

      return InsertCollection(argumentListBuilder._arguments, startIndex);
    }

    /// <summary>
    /// Inserts the collection.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns></returns>
    public ArgumentListBuilder InsertCollection(ICollection collection, int startIndex = 0)
    {
      if (collection == null || collection.Count == 0) return this;

      var i = -1;

      foreach (object item in collection)
      {
        i++;
        _arguments[i + startIndex] = item;
      }
      return this;
    }

    /// <summary>
    /// Adds the collection.
    /// </summary>
    /// <param name="argumentListBuilder">The argument list builder.</param>
    /// <returns></returns>
    public ArgumentListBuilder AddCollection(ArgumentListBuilder argumentListBuilder)
    {
      if (argumentListBuilder == null || argumentListBuilder._arguments.Count == 0) return this;

      _arguments.AddRange(argumentListBuilder._arguments);
      return this;
    }

    /// <summary>
    /// Adds the collection.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns></returns>
    public ArgumentListBuilder AddCollection(ICollection collection)
    {
      if (collection == null || collection.Count == 0) return this;

      _arguments.AddRange(collection);
      return this;
    }

    /// <summary>
    /// Inserts the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="object">The object.</param>
    /// <returns></returns>
    public ArgumentListBuilder Insert(int index, object @object)
    {
      _arguments[index] = @object;
      return this;
    }

    /// <summary>
    /// Inserts if null.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="object">The object.</param>
    /// <returns></returns>
    public ArgumentListBuilder InsertIfNull(int index, object @object)
    {
      if (_arguments.Count <= index || _arguments[index] == null)
      {
        _arguments[index] = @object;
      }

      return this;
    }

    /// <summary>
    /// Ases the enumerable.
    /// </summary>
    /// <returns></returns>
    public IEnumerable AsEnumerable()
    {
      for (int i = 0; i < _arguments.Count; i++)
      {
        yield return _arguments[i];
      }
    }

    /// <summary>
    /// Ases the array.
    /// </summary>
    /// <returns></returns>
    public object[] ToArray()
    {
      return _arguments.ToArray();
    }

    /// <summary>
    /// Converts to list.
    /// </summary>
    /// <returns></returns>
    public ArrayList ToList()
    {
      return _arguments;
    }

    /// <summary>
    /// Froms the specified array.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <returns></returns>
    public static ArgumentListBuilder From(ICollection array)
    {
      var arg = new ArgumentListBuilder();
      arg._arguments.AddRange(array);
      return arg;
    }
  }
}