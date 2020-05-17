using System;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  /// The Turkuaz Logger Factory Interface
  /// </summary>
  public interface ITurkuazLoggerFactory
  {
    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>The log instance</returns>
    public ILogger GetLogger(string name);

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The log instance</returns>
    public ILogger GetLogger(Type type);

    /// <summary>Gets the logger.</summary>
    /// <typeparam name="T">The log name</typeparam>
    /// <returns>The log instance</returns>
    public ILogger GetLogger<T>();
  }
}
