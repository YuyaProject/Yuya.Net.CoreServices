using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using System;
using System.Diagnostics;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  ///   Custom resolver used by Windsor. It gives
  ///   us some contextual information that we use to set up a logging
  ///   before satisfying the dependency
  /// </summary>
  public class LoggerResolver : ISubDependencyResolver
  {
    private static readonly Type LoggerType = typeof(ILogger);
    private static readonly Type CastleLoggerType = typeof(Castle.Core.Logging.ILogger);

    /// <summary>
    /// Can Resolve?
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="parentResolver">The parent resolver</param>
    /// <param name="model">The model</param>
    /// <param name="dependency">The dependency</param>
    /// <returns>If can be resolve this model, return true otherwise false.</returns>
    public bool CanResolve(CreationContext context, ISubDependencyResolver parentResolver, ComponentModel model, DependencyModel dependency)
    {
      return dependency.TargetType == LoggerType || dependency.TargetType == CastleLoggerType;
    }

    /// <summary>
    /// Resolve
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="parentResolver">The parent resolver</param>
    /// <param name="model">The model</param>
    /// <param name="dependency">The dependency</param>
    /// <returns>If can be resolve this model, return true otherwise false.</returns>
    public object Resolve(CreationContext context, ISubDependencyResolver parentResolver, ComponentModel model, DependencyModel dependency)
    {
      Debug.Assert(CanResolve(context, parentResolver, model, dependency));

      return LogFactory.GetLogger(model.Implementation);
    }
  }
}
