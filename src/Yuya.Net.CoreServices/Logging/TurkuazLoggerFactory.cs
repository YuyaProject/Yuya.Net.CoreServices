using System;
using Yuya.Net.CoreServices.Reflection;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  /// The Turkuaz Logger Factory
  /// </summary>
  internal class TurkuazLoggerFactory : ITurkuazLoggerFactory
  {
    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>The log instance</returns>
    public ILogger GetLogger(string name)
    {
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

      return new TurkuazLog4NetLogger(name)
      {
        ReflectionService = ReflectionService.Instance,
        Container = LogFactory.InternalApplication?.Container
      };
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The log instance</returns>
    public ILogger GetLogger(Type type)
    {
      if (type == null) throw new ArgumentNullException(nameof(type));

      return new TurkuazLog4NetLogger(type)
      {
        ReflectionService = ReflectionService.Instance,
        Container = LogFactory.InternalApplication?.Container
      };
    }

    /// <summary>Gets the logger.</summary>
    /// <typeparam name="T">The log name</typeparam>
    /// <returns>The log instance</returns>
    public ILogger GetLogger<T>()
    {
      return new TurkuazLog4NetLogger(typeof(T))
      {
        ReflectionService = ReflectionService.Instance,
        Container = LogFactory.InternalApplication?.Container
      };
    }
  }
}
