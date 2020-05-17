using Castle.Windsor;
using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.CoreServices.Reflection;

namespace Yuya.Net.CoreServices.Logging
{
  internal class TurkuazLog4NetLogger : ILogger, Castle.Core.Logging.ILogger
  {
    private static readonly Type declaringType = typeof(TurkuazLog4NetLogger);
    private readonly log4net.Core.ILogger _logger;

    public TurkuazLog4NetLogger(string loggerName)
    {
      _logger = LogFactory.GetInternalLogger(loggerName).Logger;
    }

    public TurkuazLog4NetLogger(Type loggerType)
    {
      _logger = LogFactory.GetInternalLogger(loggerType).Logger;
    }

    public IWindsorContainer Container { get; set; }
    public IReflectionService ReflectionService { get; set; }
    //public IHttpContextService HttpContextService { get; set; }

    public bool IsWarnEnabled => _logger.IsEnabledFor(Level.Warn);

    public bool IsFatalEnabled => _logger.IsEnabledFor(Level.Fatal);

    public bool IsErrorEnabled => _logger.IsEnabledFor(Level.Error);

    public bool IsDebugEnabled => _logger.IsEnabledFor(Level.Debug);

    public bool IsTraceEnabled => _logger.IsEnabledFor(Level.Trace);

    public bool IsInfoEnabled => _logger.IsEnabledFor(Level.Info);

    public bool IsEnabledFor(Level level) => _logger.IsEnabledFor(level);

    public ErrorMessage GetErrorMessage(object message)
    {
      var msg = CreateErrorMessage(message);

      FillStandartFields(msg);

      if (message == null) return msg;

      if (message is string || message is ErrorMessage || message.GetType().IsPrimitive)
      {
      }
      else if (message is IDictionary<string, object> d)
      {
        FillMessage(msg, d);
      }
      else if (message is Dictionary<string, object> d2)
      {
        FillMessage(msg, d2);
      }
      else if (message is IDictionary d3)
      {
        FillMessage(msg, d3.Keys.OfType<object>().ToDictionary(x => x.ToString(), x => d3[x]));
      }
      else
      {
        FillMessage(msg, ReflectionService.GetPropertiesWithValues(message).ToDictionary(x => x.Name, x => x.Value));
      }

      return msg;
    }

    private void FillStandartFields(ErrorMessage message)
    {
      var httpContextService = (Container?.Kernel.HasComponent(typeof(IHttpContextService)) ?? false)
        ? Container?.Resolve<IHttpContextService>()
        : null;
      if (httpContextService != null)
      {
        message.Url = httpContextService.Url;
        message.FilePath = httpContextService.FilePath;
        message.HttpMethod = httpContextService.HttpMethod;
        message.RequestId = httpContextService.RequestId;
        message.SessionId = httpContextService.SessionId;
        message.UrlReferrer = httpContextService.UrlReferrer;
      }
    }

    private ErrorMessage CreateErrorMessage(object message)
    {
      if (message == null)
      {
        return new ErrorMessage();
      }

      if (message is ErrorMessage em)
      {
        return em.Clone() as ErrorMessage;
      }
      else if (message is string str)
      {
        return new ErrorMessage(str);
      }
      else if (message.GetType().IsPrimitive)
      {
        return new ErrorMessage(message.ToString());
      }
      else
      {
        return new ErrorMessage();
      }
    }

    private void FillMessage(ErrorMessage msg, IDictionary<string, object> d)
    {
      if (msg.AdditionalData == null)
      {
        msg.AdditionalData = new Dictionary<string, object>(d);
      }
      else
      {
        foreach (KeyValuePair<string, object> item in d)
        {
          msg.AdditionalData[item.Key] = item.Value;
        }
      }

      if (d.ContainsKey("RawMessage"))
      {
        msg.RawMessage = d["RawMessage"].ToString();
        msg.AdditionalData.Remove("RawMessage");
      }
    }

    public void Debug(Func<object> messageFactory, Exception exception = null)
    {
      if (IsDebugEnabled)
        _logger.Log(declaringType, Level.Debug, GetErrorMessage(messageFactory.Invoke()), exception);
    }

    public void Debug(object message, Exception exception = null)
    {
      if (IsDebugEnabled)
        _logger.Log(declaringType, Level.Debug, GetErrorMessage(message), exception);
    }

    public void Error(Func<object> messageFactory, Exception exception = null)
    {
      if (IsErrorEnabled)
        _logger.Log(declaringType, Level.Error, GetErrorMessage(messageFactory.Invoke()), exception);
    }

    public void Error(object message, Exception exception = null)
    {
      if (IsErrorEnabled)
        _logger.Log(declaringType, Level.Error, GetErrorMessage(message), exception);
    }

    public void Fatal(Func<object> messageFactory, Exception exception = null)
    {
      if (IsFatalEnabled)
        _logger.Log(declaringType, Level.Fatal, GetErrorMessage(messageFactory.Invoke()), exception);
    }

    public void Fatal(object message, Exception exception = null)
    {
      if (IsFatalEnabled)
        _logger.Log(declaringType, Level.Fatal, GetErrorMessage(message), exception);
    }

    public void Info(Func<object> messageFactory, Exception exception = null)
    {
      if (IsInfoEnabled)
        _logger.Log(declaringType, Level.Info, GetErrorMessage(messageFactory.Invoke()), exception);
    }

    public void Info(object message, Exception exception = null)
    {
      if (IsInfoEnabled)
        _logger.Log(declaringType, Level.Info, GetErrorMessage(message), exception);
    }

    public void Trace(Func<object> messageFactory, Exception exception = null)
    {
      if (IsTraceEnabled)
        _logger.Log(declaringType, Level.Trace, GetErrorMessage(messageFactory.Invoke()), exception);
    }

    public void Trace(object message, Exception exception)
    {
      if (IsTraceEnabled)
        _logger.Log(declaringType, Level.Trace, GetErrorMessage(message), exception);
    }

    public void Warn(object message, Exception exception = null)
    {
      if (IsWarnEnabled)
        _logger.Log(declaringType, Level.Warn, GetErrorMessage(message), exception);
    }

    public void Warn(Func<object> messageFactory, Exception exception = null)
    {
      if (IsWarnEnabled)
        _logger.Log(declaringType, Level.Warn, GetErrorMessage(messageFactory.Invoke()), exception);
    }

    public void Log(Level level, object message, Exception exception = null)
    {
      if (IsEnabledFor(level))
        _logger.Log(declaringType, level, GetErrorMessage(message), exception);
    }

    public void Log(Level level, Func<object> messageFactory, Exception exception = null)
    {
      if (IsEnabledFor(level))
        _logger.Log(declaringType, level, GetErrorMessage(messageFactory.Invoke()), exception);
    }

    #region Castle ILogger Interface Implementation

    public Castle.Core.Logging.ILogger CreateChildLogger(string loggerName)
    {
      return new TurkuazLog4NetLogger($"{this._logger.Name}.{loggerName}");
    }

    public void Trace(string message)
    {
      Log(Level.Trace, message);
    }

    public void Trace(Func<string> messageFactory)
    {
      Log(Level.Trace, messageFactory);
    }

    public void Trace(string message, Exception exception)
    {
      Log(Level.Trace, message, exception);
    }

    public void TraceFormat(string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Trace, message);
    }

    public void TraceFormat(Exception exception, string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Trace, message, exception);
    }

    public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Trace, message);
    }

    public void TraceFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Trace, message, exception);
    }

    public void Debug(string message)
    {
      Log(Level.Debug, message);
    }

    public void Debug(Func<string> messageFactory)
    {
      Log(Level.Debug, messageFactory);
    }

    public void Debug(string message, Exception exception)
    {
      Log(Level.Debug, message, exception);
    }

    public void DebugFormat(string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Debug, message);
    }

    public void DebugFormat(Exception exception, string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Debug, message, exception);
    }

    public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Debug, message);
    }

    public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Debug, message, exception);
    }

    public void Error(string message)
    {
      Log(Level.Error, message);
    }

    public void Error(Func<string> messageFactory)
    {
      Log(Level.Error, messageFactory);
    }

    public void Error(string message, Exception exception)
    {
      Log(Level.Error, message, exception);
    }

    public void ErrorFormat(string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Error, message);
    }

    public void ErrorFormat(Exception exception, string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Error, message, exception);
    }

    public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Error, message);
    }

    public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Error, message, exception);
    }

    public void Fatal(string message)
    {
      Log(Level.Fatal, message);
    }

    public void Fatal(Func<string> messageFactory)
    {
      Log(Level.Fatal, messageFactory);
    }

    public void Fatal(string message, Exception exception)
    {
      Log(Level.Fatal, message, exception);
    }

    public void FatalFormat(string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Fatal, message);
    }

    public void FatalFormat(Exception exception, string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Fatal, message, exception);
    }

    public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Fatal, message);
    }

    public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Fatal, message, exception);
    }

    public void Info(string message)
    {
      Log(Level.Info, message);
    }

    public void Info(Func<string> messageFactory)
    {
      Log(Level.Info, messageFactory);
    }

    public void Info(string message, Exception exception)
    {
      Log(Level.Info, message, exception);
    }

    public void InfoFormat(string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Info, message);
    }

    public void InfoFormat(Exception exception, string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Info, message, exception);
    }

    public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Info, message);
    }

    public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Info, message, exception);
    }

    public void Warn(string message)
    {
      Log(Level.Warn, message);
    }

    public void Warn(Func<string> messageFactory)
    {
      Log(Level.Warn, messageFactory);
    }

    public void Warn(string message, Exception exception)
    {
      Log(Level.Warn, message, exception);
    }

    public void WarnFormat(string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Warn, message);
    }

    public void WarnFormat(Exception exception, string format, params object[] args)
    {
      var message = string.Format(format, args);
      Log(Level.Warn, message, exception);
    }

    public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Warn, message);
    }

    public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
    {
      var message = string.Format(formatProvider, format, args);
      Log(Level.Warn, message, exception);
    }

    #endregion Castle ILogger Interface Implementation
  }
}
