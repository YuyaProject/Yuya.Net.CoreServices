using System.Reflection;
using System.Threading.Tasks;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Interceptor Utils
  /// </summary>
  public static class InterceptorUtils
  {
    /// <summary>
    /// Determines whether [is asynchronous method] [the specified method].
    /// </summary>
    /// <param name="method">The method.</param>
    /// <returns>
    ///   <c>true</c> if [is asynchronous method] [the specified method]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsAsyncMethod(MethodInfo method)
    {
      return
          method.ReturnType == typeof(Task)
          || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
          ;
    }
  }
}
