using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using System;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// IoC Resolver Interface
  /// </summary>
  public interface IIocRegisterer
  {
    IIocResolver Resolver { get; }


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
    /// Creates and add an IFacility facility to the container.
    /// </summary>
    /// <typeparam name="TFacility">the facility type parameter</typeparam>
    /// <param name="onCreate">on create action</param>
    /// <returns>the <see cref="IIocRegisterer"/> instance</returns>
    public IIocRegisterer AddFacility<TFacility>(Action<TFacility> onCreate = null)
      where TFacility : IFacility, new();





    /// <summary>
    /// Registers the type instance.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    IIocRegisterer RegisterTypeInstance<TService, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null)
      where TService : class
      where TComponent : TService, new();

    /// <summary>Registers the instance.</summary>
    /// <param name="container">The container.</param>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="instance">The instance.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns></returns>
    IIocRegisterer RegisterInstance(
      Type serviceType,
      object instance,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false);

    /// <summary>Registers the instance.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="instance">The instance.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns></returns>
    IIocRegisterer RegisterInstance<TService>(
      TService instance,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
      where TService : class;

    /// <summary>Registers the instance.</summary>
    /// <typeparam name="TService1">The type of the service1.</typeparam>
    /// <typeparam name="TService2">The type of the service2.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="instance">The instance.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns></returns>
    IIocRegisterer RegisterInstance<TService1, TService2>(
      TService1 instance,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService1>> configuration = null,
      bool ifNotExists = false)
      where TService1 : class;

    /// <summary>Register component by singleton.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns>the container instance</returns>
    IIocRegisterer RegisterSingleton<TService, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
      where TService : class
      where TComponent : TService;

    /// <summary>Register component by singleton.</summary>
    /// <param name="container">The container.</param>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="componentType">Type of the component.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns>the container instance</returns>
    IIocRegisterer RegisterSingleton(
      Type serviceType,
      Type componentType,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false);

    /// <summary>
    /// Registers the scoped.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists"></param>
    /// <returns></returns>
    IIocRegisterer RegisterScoped<TService, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
     where TService : class
     where TComponent : TService;

    /// <summary>Register component by Scoped.</summary>
    /// <param name="container">The container.</param>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="componentType">Type of the component.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns>the container instance</returns>
    IIocRegisterer RegisterScoped(
      Type serviceType,
      Type componentType,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false);

    /// <summary>Register component by transient.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns>the container instance</returns>
    IIocRegisterer RegisterTransient<TService, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
      where TService : class
      where TComponent : TService;

    /// <summary>
    /// Register component by transient.
    /// </summary>
    /// <typeparam name="TService1">The type of the service #1.</typeparam>
    /// <typeparam name="TService2">The type of the service #2.</typeparam>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">The is default.</param>
    /// <param name="isFallback">The is fallback.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>
    /// the container instance
    /// </returns>
    IIocRegisterer RegisterTransient<TService1, TService2, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService1>> configuration = null)
      where TService1 : class
      where TService2 : class
      where TComponent : TService1, TService2;

    /// <summary>Register component by Transient.</summary>
    /// <param name="container">The container.</param>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="componentType">Type of the component.</param>
    /// <param name="name">The name.</param>
    /// <param name="isDefault">if set to <c>true</c> [is default].</param>
    /// <param name="isFallback">if set to <c>true</c> [is fallback].</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="ifNotExists">if set to <c>true</c> [if not exists].</param>
    /// <returns>the container instance</returns>
    IIocRegisterer RegisterTransient(
      Type serviceType,
      Type componentType,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false);

    #region Options

    /// <summary>
    /// Adds services required for using options.
    /// </summary>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer AddOptions();

    /// <summary>
    /// Registers an action used to configure a particular type of options.
    /// Note: These are run before all <seealso cref="PostConfigure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer Configure<TOptions>(Action<TOptions> configureOptions)
      where TOptions : class;

    /// <summary>
    /// Registers an action used to configure a particular type of options.
    /// Note: These are run before all <seealso cref="PostConfigure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="name">The name of the options instance.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer Configure<TOptions>(string name, Action<TOptions> configureOptions)
        where TOptions : class;

    /// <summary>
    /// Registers an action used to configure all instances of a particular type of options.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer ConfigureAll<TOptions>(Action<TOptions> configureOptions)
      where TOptions : class;

    /// <summary>
    /// Registers an action used to initialize a particular type of options.
    /// Note: These are run after all <seealso cref="Configure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer PostConfigure<TOptions>(Action<TOptions> configureOptions)
      where TOptions : class;

    /// <summary>
    /// Registers an action used to configure a particular type of options.
    /// Note: These are run after all <seealso cref="Configure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configure.</typeparam>
    /// <param name="name">The name of the options instance.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer PostConfigure<TOptions>(string name, Action<TOptions> configureOptions)
        where TOptions : class;

    /// <summary>
    /// Registers an action used to post configure all instances of a particular type of options.
    /// Note: These are run after all <seealso cref="Configure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
      where TOptions : class;

    /// <summary>
    /// Registers a type that will have all of its I[Post]ConfigureOptions registered.
    /// </summary>
    /// <typeparam name="TConfigureOptions">The type that will configure options.</typeparam>
    /// <param name="container">The container.</param>
    /// <returns>
    /// The <see cref="IWindsorContainer" /> so that additional calls can be chained.
    /// </returns>
    IIocRegisterer ConfigureOptions<TConfigureOptions>()
      where TConfigureOptions : class;

    /// <summary>
    /// Registers a type that will have all of its I[Post]ConfigureOptions registered.
    /// </summary>
    /// <param name="configureType">The type that will configure options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer ConfigureOptions(Type configureType);

    /// <summary>
    /// Registers an object that will have all of its I[Post]ConfigureOptions registered.
    /// </summary>
    /// <param name="configureInstance">The instance that will configure options.</param>
    /// <returns>The <see cref="IIocRegisterer"/> so that additional calls can be chained.</returns>
    IIocRegisterer ConfigureOptions(object configureInstance);

    #endregion Options
  }
}