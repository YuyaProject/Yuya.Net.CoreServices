using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Begin Scope Interceptor
  /// </summary>
  public class BeginScopeInterceptor : IInterceptor
  {
    private readonly IKernel _kernel;

    /// <summary>
    /// Initializes a new instance of the <see cref="BeginScopeInterceptor"/> class.
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    public BeginScopeInterceptor(IKernel kernel)
    {
      _kernel = kernel;
    }

    /// <summary>Intercepts the specified invocation.</summary>
    /// <param name="invocation">The invocation.</param>
    public void Intercept(IInvocation invocation)
    {
      using (_kernel.BeginScope())
      {
        invocation.Proceed();
      }
    }
  }
}
