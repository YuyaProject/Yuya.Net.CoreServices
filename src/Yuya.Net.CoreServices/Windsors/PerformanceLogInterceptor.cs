using Castle.DynamicProxy;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Yuya.Net.CoreServices.Logging;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Performance Log Interceptor
  /// </summary>
  /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
  public class PerformanceLogInterceptor : IInterceptor
  {
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceLogInterceptor"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    public PerformanceLogInterceptor(ITurkuazLoggerFactory loggerFactory)
    {
      _logger = loggerFactory.GetLogger("PerformanceLog");
    }

    /// <summary>
    /// Intercepts the specified invocation.
    /// </summary>
    /// <param name="invocation">The invocation.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Intercept(IInvocation invocation)
    {
      var sw = Stopwatch.StartNew();

      if (InterceptorUtils.IsAsyncMethod(invocation.Method))
      {
        InterceptAsync(invocation, sw);
      }
      else
      {
        InterceptSync(invocation, sw);
      }
    }

    private void InterceptAsync(IInvocation invocation, Stopwatch stopwatch)
    {
      //Calling the actual method, but execution has not been finished yet
      invocation.Proceed();

      //We should wait for finishing of the method execution
      ((Task)invocation.ReturnValue)
          .ContinueWith(task =>
          {
            if (task.IsFaulted)
            {
              _logger.Error(Log(GetClassName(invocation.MethodInvocationTarget.DeclaringType), invocation.Method.Name, stopwatch.Elapsed, task.Exception));
            }
            else
            {
              _logger.Info(Log(GetClassName(invocation.MethodInvocationTarget.DeclaringType), invocation.Method.Name, stopwatch.Elapsed));
            }
          });
    }

    private void InterceptSync(IInvocation invocation, Stopwatch stopwatch)
    {
      try
      {
        invocation.Proceed();
        _logger.Info(Log(GetClassName(invocation.MethodInvocationTarget.DeclaringType), invocation.Method.Name, stopwatch.Elapsed));
      }
      catch (Exception ex)
      {
        // loglama
        _logger.Error(Log(GetClassName(invocation.MethodInvocationTarget.DeclaringType), invocation.Method.Name, stopwatch.Elapsed, ex));
        throw;
      }
    }

    private string GetClassName(Type type)
    {
      if (type.IsGenericType)
      {
        return $"{type.Name}<{string.Join(", ", type.GetGenericArguments().Select(x => GetClassName(x)))}>";
      }
      return type.Name;
    }

    private string Log(string className, string methodName, TimeSpan duration, Exception exception = null, bool? isError = null)
    {
      var errorText = (isError ?? exception != null) ? "Error" : "Success";
      return string.Format("{0}.{1};{3};{2};{4}", className, methodName, duration, errorText, exception?.Message);
    }
  }
}
