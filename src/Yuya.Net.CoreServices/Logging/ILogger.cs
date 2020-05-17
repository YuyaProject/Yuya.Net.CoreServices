using log4net.Core;
using System;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  /// Manages logging.
  /// </summary>
  /// <remarks>
  /// This is a facade for the different logging subsystems. It offers a simplified
  /// interface that follows IOC patterns and a simplified priority/level/severity
  /// abstraction.
  /// </remarks>
  public interface ILogger
  {
    /// <summary>
    /// Determines if messages of priority "warn" will be logged.
    /// </summary>
    bool IsWarnEnabled { get; }

    /// <summary>
    /// Determines if messages of priority "fatal" will be logged.
    /// </summary>
    bool IsFatalEnabled { get; }

    /// <summary>
    /// Determines if messages of priority "error" will be logged.
    /// </summary>
    bool IsErrorEnabled { get; }

    /// <summary>
    /// Determines if messages of priority "debug" will be logged.
    /// </summary>
    bool IsDebugEnabled { get; }

    /// <summary>
    /// Determines if messages of priority "trace" will be logged.
    /// </summary>
    bool IsTraceEnabled { get; }

    /// <summary>
    /// Determines if messages of priority "info" will be logged.
    /// </summary>
    bool IsInfoEnabled { get; }

    /// <summary>
    /// Is Enabled For
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    bool IsEnabledFor(Level level);

    /// <summary>
    /// Logs a debug message with lazily constructed message. The message will be constructed
    /// only if the Castle.Core.Logging.ILogger.IsDebugEnabled is true.
    /// </summary>
    /// <param name="messageFactory">The message to log</param>
    /// <param name="exception">The exception to log</param>
    void Debug(Func<object> messageFactory, Exception exception = null);

    /// <summary>Logs a debug message.</summary>
    /// <param name="message">The message.</param>
    /// <param name="exception">The exception to log</param>
    void Debug(object message, Exception exception = null);

    /// <summary>
    /// Logs an error message with lazily constructed message. The message will be constructed
    /// only if the Castle.Core.Logging.ILogger.IsErrorEnabled is true.
    /// </summary>
    /// <param name="messageFactory">The message factory.</param>
    /// <param name="exception">The exception to log</param>
    void Error(Func<object> messageFactory, Exception exception = null);


    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="exception">The exception to log</param>
    void Error(object message, Exception exception = null);

    /// <summary>
    /// Logs a fatal message with lazily constructed message. The message will be constructed
    /// only if the Castle.Core.Logging.ILogger.IsFatalEnabled is true.
    /// </summary>
    /// <param name="messageFactory"></param>
    /// <param name="exception"></param>
    void Fatal(Func<object> messageFactory, Exception exception = null);


    /// <summary>
    ///  Logs a fatal message.
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="exception">The exception to log</param>
    void Fatal(object message, Exception exception = null);


    /// <summary>
    /// Logs a info message with lazily constructed message. The message will be constructed
    /// only if the Castle.Core.Logging.ILogger.IsInfoEnabled is true.
    /// </summary>
    /// <param name="messageFactory">The message factory to log</param>
    /// <param name="exception">The exception to log</param>
    void Info(Func<object> messageFactory, Exception exception = null);

    /// <summary>
    /// Logs an info message.
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="exception">The exception to log</param>
    void Info(object message, Exception exception = null);


    /// <summary>
    /// Logs a trace message with lazily constructed message. The message will be constructed
    /// only if the Castle.Core.Logging.ILogger.IsTraceEnabled is true.
    /// </summary>
    /// <param name="messageFactory">The message factory to log</param>
    /// <param name="exception">The exception to log</param>
    void Trace(Func<object> messageFactory, Exception exception = null);

    /// <summary>
    /// Logs an trace message.
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="exception">The exception to log</param>
    void Trace(object message, Exception exception);


    /// <summary>
    /// Logs an warn message.
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="exception">The exception to log</param>
    void Warn(object message, Exception exception = null);

    /// <summary>
    /// Logs a warn message with lazily constructed message. The message will be constructed
    /// only if the Castle.Core.Logging.ILogger.IsWarnEnabled is true.
    /// </summary>
    /// <param name="messageFactory">The message factory to log</param>
    /// <param name="exception">The exception to log</param>
    void Warn(Func<object> messageFactory, Exception exception = null);

    /// <summary>Logs an warn message.</summary>
    /// <param name="level">The level.</param>
    /// <param name="message">The message to log</param>
    /// <param name="exception">The exception to log</param>
    void Log(Level level, object message, Exception exception = null);

    /// <summary>
    /// Logs a warn message with lazily constructed message. The message will be constructed
    /// only if the Castle.Core.Logging.ILogger.IsWarnEnabled is true.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="messageFactory">The message factory to log</param>
    /// <param name="exception">The exception to log</param>
    void Log(Level level, Func<object> messageFactory, Exception exception = null);
  }
}
