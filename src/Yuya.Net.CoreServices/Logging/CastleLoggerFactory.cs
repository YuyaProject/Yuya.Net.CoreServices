using Castle.Core.Logging;
using System;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  /// Castle Logger Factory
  /// </summary>
  /// <seealso cref="Castle.Core.Logging.ILoggerFactory" />
  public class CastleLoggerFactory : Castle.Core.Logging.ILoggerFactory
  {
    private readonly ITurkuazLoggerFactory _turkuazLoggerFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="CastleLoggerFactory"/> class.
    /// </summary>
    /// <param name="turkuazLoggerFactory">The turkuaz logger factory.</param>
    public CastleLoggerFactory(ITurkuazLoggerFactory turkuazLoggerFactory)
    {
      _turkuazLoggerFactory = turkuazLoggerFactory;
    }

    /// <summary>
    /// Creates a new logger, getting the logger name from the specified type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Castle.Core.Logging.ILogger Create(Type type)
    {
      return (Castle.Core.Logging.ILogger)_turkuazLoggerFactory.GetLogger(type);
    }

    /// <summary>Creates a new logger.</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Castle.Core.Logging.ILogger Create(string name)
    {
      return (Castle.Core.Logging.ILogger)_turkuazLoggerFactory.GetLogger(name);
    }

    /// <summary>
    /// Creates a new logger, getting the logger name from the specified type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public Castle.Core.Logging.ILogger Create(Type type, LoggerLevel level)
    {
      return (Castle.Core.Logging.ILogger)_turkuazLoggerFactory.GetLogger(type);
    }

    /// <summary>Creates a new logger.</summary>
    /// <param name="name"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public Castle.Core.Logging.ILogger Create(string name, LoggerLevel level)
    {
      return (Castle.Core.Logging.ILogger)_turkuazLoggerFactory.GetLogger(name);
    }
  }
}
