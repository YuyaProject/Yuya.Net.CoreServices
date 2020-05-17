using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Named Components Sub Dependency Resolver
  /// </summary>
  /// <seealso cref="Castle.MicroKernel.ISubDependencyResolver" />
  public class NamedComponentsSubDependencyResolver : ISubDependencyResolver
  {
    private readonly IKernel _kernel;

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedComponentsSubDependencyResolver" /> class.
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    public NamedComponentsSubDependencyResolver(IKernel kernel)
    {
      _kernel = kernel;
    }

    /// <summary>
    /// Returns true if the resolver is able to satisfy this dependency.
    /// </summary>
    /// <param name="context">Creation context, which is a resolver itself</param>
    /// <param name="contextHandlerResolver">Parent resolver - normally the IHandler implementation</param>
    /// <param name="model">Model of the component that is requesting the dependency</param>
    /// <param name="dependency">The dependency model</param>
    /// <returns>
    ///   <c>true</c> if the dependency can be satisfied
    /// </returns>
    public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
    {
      if (dependency.IsPrimitiveTypeDependency) return false;
      if (_kernel.HasComponent(dependency.DependencyKey)) return true;
      return false;
    }

    /// <summary>
    /// Should return an instance of a service or property values as
    /// specified by the dependency model instance.
    /// It is also the responsibility of <see cref="T:Castle.MicroKernel.IDependencyResolver" />
    /// to throw an exception in the case a non-optional dependency
    /// could not be resolved.
    /// </summary>
    /// <param name="context">Creation context, which is a resolver itself</param>
    /// <param name="contextHandlerResolver">Parent resolver - normally the IHandler implementation</param>
    /// <param name="model">Model of the component that is requesting the dependency</param>
    /// <param name="dependency">The dependency model</param>
    /// <returns>
    /// The dependency resolved value or null
    /// </returns>
    public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
    {
      return _kernel.Resolve(dependency.DependencyKey, dependency.TargetItemType);
    }
  }
}
