using Castle.MicroKernel;
using System;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// IoC Resolver Interface
  /// </summary>
  public interface IIocResolver
  {
    IIocRegisterer Registerer { get; }
    /// <summary>
    /// Determines whether the specified name has component.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>
    ///   <c>true</c> if the specified name has component; otherwise, <c>false</c>.
    /// </returns>
    bool HasComponent(string name);

    /// <summary>
    /// Determines whether the specified service type has component.
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <returns>
    ///   <c>true</c> if the specified service type has component; otherwise, <c>false</c>.
    /// </returns>
    bool HasComponent(Type serviceType);

    /// <summary>
    /// Determines whether this instance has component.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <returns>
    ///   <c>true</c> if this instance has component; otherwise, <c>false</c>.
    /// </returns>
    bool HasComponent<TService>();

    /// <summary>
    /// Releases a component instance
    /// </summary>
    /// <param name="instance">The instance.</param>
    void Release(object instance);

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="service">The service.</param>
    /// <returns></returns>
    object Resolve(string key, Type service);

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="service">The service.</param>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns></returns>
    object Resolve(string key, Type service, Arguments arguments);

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns></returns>
    object Resolve(Type service);

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns></returns>
    object Resolve(Type service, Arguments arguments);

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <typeparam name="T">Service Type</typeparam>
    /// <returns>The component instance</returns>
    T Resolve<T>();

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns>The component instance</returns>
    T Resolve<T>(Arguments arguments);

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    /// <param name="key">Component's key</param>
    /// <returns>The Component instance</returns>
    T Resolve<T>(string key);

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    /// <param name="key">Component's key</param>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns>The Component instance</returns>
    T Resolve<T>(string key, Arguments arguments);

    /// <summary>
    /// Resolve all valid components that match this service the service to match
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns></returns>
    Array ResolveAll(Type service);

    /// <summary>
    /// Resolve all valid components that match this service the service to match Arguments
    /// to resolve the service.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="arguments">The arguments.</param>
    /// <returns></returns>
    Array ResolveAll(Type service, Arguments arguments);

    /// <summary>
    /// Resolve all valid components that match this type.
    /// </summary>
    /// <typeparam name="T">The service type</typeparam>
    /// <returns></returns>
    T[] ResolveAll<T>();

    /// <summary>
    /// Resolve all valid components that match this type. The service type Arguments to resolve the service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arguments">The arguments.</param>
    /// <returns></returns>
    T[] ResolveAll<T>(Arguments arguments);
  }
}