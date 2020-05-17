using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// IoC Resolver
  /// </summary>
  public class IocManager : IIocResolver, IIocRegisterer
  {
    private readonly IWindsorContainer _windsorContainer;

    /// <summary>
    /// Initializes a new instance of the <see cref="IocManager"/> class.
    /// </summary>
    /// <param name="windsorContainer">The windsor container.</param>
    public IocManager(IWindsorContainer windsorContainer)
    {
      _windsorContainer = windsorContainer;
    }

    /// <summary>
    /// Determines whether the specified service type has component.
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <returns>
    ///   <c>true</c> if the specified service type has component; otherwise, <c>false</c>.
    /// </returns>
    public bool HasComponent(Type serviceType)
    {
      return _windsorContainer.Kernel.HasComponent(serviceType);
    }

    /// <summary>
    /// Determines whether this instance has component.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <returns>
    ///   <c>true</c> if this instance has component; otherwise, <c>false</c>.
    /// </returns>
    public bool HasComponent<TService>()
    {
      return _windsorContainer.Kernel.HasComponent(typeof(TService));
    }

    /// <summary>
    /// Determines whether the specified name has component.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>
    ///   <c>true</c> if the specified name has component; otherwise, <c>false</c>.
    /// </returns>
    public bool HasComponent(string name)
    {
      return _windsorContainer.Kernel.HasComponent(name);
    }

    #region IIocResolver

    public IIocRegisterer Registerer => this;


    /// <summary>
    /// Releases a component instance
    /// </summary>
    /// <param name="instance">The instance.</param>
    public void Release(object instance)
    {
      _windsorContainer.Release(instance);
    }

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="service">The service.</param>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns></returns>
    public object Resolve(string key, Type service, Arguments arguments)
    {
      return _windsorContainer.Resolve(key, service, arguments);
    }

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    /// <param name="key">Component's key</param>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns>The Component instance</returns>
    public T Resolve<T>(string key, Arguments arguments)
    {
      return _windsorContainer.Resolve<T>(key, arguments);
    }

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns>The component instance</returns>
    public T Resolve<T>(Arguments arguments)
    {
      return _windsorContainer.Resolve<T>(arguments);
    }

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <typeparam name="T">Service Type</typeparam>
    /// <returns>The component instance</returns>
    public T Resolve<T>()
    {
      return _windsorContainer.Resolve<T>();
    }

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns></returns>
    public object Resolve(Type service)
    {
      return _windsorContainer.Resolve(service);
    }

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="service">The service.</param>
    /// <returns></returns>
    public object Resolve(string key, Type service)
    {
      return _windsorContainer.Resolve(key, service);
    }

    /// <summary>
    /// Returns a component instance by the key
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    /// <param name="key">Component's key</param>
    /// <returns>The Component instance</returns>
    public T Resolve<T>(string key)
    {
      return _windsorContainer.Resolve<T>(key);
    }

    /// <summary>
    /// Returns a component instance by the service
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="arguments">Arguments to resolve the service.</param>
    /// <returns></returns>
    public object Resolve(Type service, Arguments arguments)
    {
      return _windsorContainer.Resolve(service, arguments);
    }

    /// <summary>
    /// Resolve all valid components that match this type.
    /// </summary>
    /// <typeparam name="T">The service type</typeparam>
    /// <returns></returns>
    public T[] ResolveAll<T>()
    {
      return _windsorContainer.ResolveAll<T>();
    }

    /// <summary>
    /// Resolve all valid components that match this service the service to match
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns></returns>
    public Array ResolveAll(Type service)
    {
      return _windsorContainer.ResolveAll(service);
    }

    /// <summary>
    /// Resolve all valid components that match this service the service to match Arguments
    /// to resolve the service.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="arguments">The arguments.</param>
    /// <returns></returns>
    public Array ResolveAll(Type service, Arguments arguments)
    {
      return _windsorContainer.ResolveAll(service, arguments);
    }

    /// <summary>
    /// Resolve all valid components that match this type. The service type Arguments to resolve the service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arguments">The arguments.</param>
    /// <returns></returns>
    public T[] ResolveAll<T>(Arguments arguments)
    {
      return _windsorContainer.ResolveAll<T>(arguments);
    }

    #endregion IIocResolver

    #region IIocRegisterer

    public IIocResolver Resolver => this;

    /// <summary>
    /// Creates and add an IFacility facility to the container.
    /// </summary>
    /// <typeparam name="TFacility">the facility type parameter</typeparam>
    /// <param name="onCreate">on create action</param>
    /// <returns>the <see cref="IIocRegisterer"/> instance</returns>
    public IIocRegisterer AddFacility<TFacility>(Action<TFacility> onCreate = null)
      where TFacility : IFacility, new()
    {
      if (onCreate == null)
        _windsorContainer.AddFacility<TFacility>();
      else
        _windsorContainer.AddFacility<TFacility>(onCreate);
      return this;
    }

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
    public IIocRegisterer RegisterTypeInstance<TService, TComponent>(
        string name = null,
        bool isDefault = false,
        bool isFallback = false,
        Action<ComponentRegistration<TService>> configuration = null)
        where TService : class
        where TComponent : TService, new()
    {
      var conf = Component
                .For<TService>()
                .Instance(new TComponent());

      Configure(conf, name, isDefault, isFallback, configuration);

      _windsorContainer.Register(conf);
      return this;
    }

    private void Configure<TService>(ComponentRegistration<TService> conf, string name, bool isDefault, bool isFallback, Action<ComponentRegistration<TService>> configuration)
      where TService : class
    {
      if (isDefault) conf.IsDefault();
      if (isFallback) conf.IsFallback();

      if (!string.IsNullOrWhiteSpace(name)) conf.Named(name);

      configuration?.Invoke(conf);
    }

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
    public IIocRegisterer RegisterInstance(
      Type serviceType,
      object instance,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false)
    {
      if (ifNotExists && _windsorContainer.Kernel.HasComponent(serviceType))
      {
        return this;
      }

      var conf = Component
          .For(serviceType)
          .Instance(instance);

      Configure(conf, name, isDefault, isFallback, configuration);

      _windsorContainer.Register(conf);
      return this;
    }

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
    public IIocRegisterer RegisterInstance<TService>(
      TService instance,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
      where TService : class
    {
      if (ifNotExists && _windsorContainer.Kernel.HasComponent(typeof(TService)))
      {
        return this;
      }

      var conf = Component
          .For<TService>()
          .Instance(instance);

      Configure(conf, name, isDefault, isFallback, configuration);

      _windsorContainer.Register(conf);
      return this;
    }

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
    public IIocRegisterer RegisterInstance<TService1, TService2>(
      TService1 instance,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService1>> configuration = null,
      bool ifNotExists = false)
      where TService1 : class
    {
      if (ifNotExists && _windsorContainer.Kernel.HasComponent(typeof(TService1)))
      {
        return this;
      }

      var conf = Component
          .For<TService1, TService2>()
          .Instance(instance);

      Configure(conf, name, isDefault, isFallback, configuration);

      _windsorContainer.Register(conf);
      return this;
    }

    private IIocRegisterer RegisterComponent<TService, TComponent>(
      Func<ComponentRegistration<TService>, ComponentRegistration<TService>> confFunc = null,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
      where TService : class
      where TComponent : TService
    {
      if (ifNotExists && _windsorContainer.Kernel.HasComponent(typeof(TService)))
      {
        return this;
      }
      var conf = Component
                .For<TService>()
                .ImplementedBy<TComponent>();

      if (confFunc != null)
      {
        conf = confFunc(conf);
      }

      Configure(conf, name, isDefault, isFallback, configuration);

      _windsorContainer.Register(conf);

      return this;
    }

    private IIocRegisterer RegisterComponent(
      Type serviceType,
      Type componentType,
      Func<ComponentRegistration<object>, ComponentRegistration<object>> confFunc = null,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false)
    {
      if (ifNotExists && _windsorContainer.Kernel.HasComponent(serviceType))
      {
        return this;
      }

      var conf = Component
          .For(serviceType)
          .ImplementedBy(componentType);

      if (confFunc != null)
      {
        conf = confFunc(conf);
      }

      Configure(conf, name, isDefault, isFallback, configuration);

      _windsorContainer.Register(conf);
      return this;
    }

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
    public IIocRegisterer RegisterSingleton<TService, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
      where TService : class
      where TComponent : TService
    {
      return RegisterComponent<TService, TComponent>(c => c.LifestyleSingleton(), name, isDefault, isFallback, configuration, ifNotExists);
    }

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
    public IIocRegisterer RegisterSingleton(
      Type serviceType,
      Type componentType,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false)
    {
      return RegisterComponent(serviceType, componentType, c => c.LifestyleSingleton(), name, isDefault, isFallback, configuration, ifNotExists);
    }

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
    public IIocRegisterer RegisterScoped<TService, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
     where TService : class
     where TComponent : TService
    {
      return RegisterComponent<TService, TComponent>(c => c.LifestyleScoped(), name, isDefault, isFallback, configuration, ifNotExists);
    }

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
    public IIocRegisterer RegisterScoped(
      Type serviceType,
      Type componentType,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false)
    {
      return RegisterComponent(serviceType, componentType, c => c.LifestyleScoped(), name, isDefault, isFallback, configuration, ifNotExists);
    }

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
    public IIocRegisterer RegisterTransient<TService, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService>> configuration = null,
      bool ifNotExists = false)
      where TService : class
      where TComponent : TService
    {
      return RegisterComponent<TService, TComponent>(c => c.LifestyleTransient(), name, isDefault, isFallback, configuration, ifNotExists);
    }

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
    public IIocRegisterer RegisterTransient<TService1, TService2, TComponent>(
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<TService1>> configuration = null)
      where TService1 : class
      where TService2 : class
      where TComponent : TService1, TService2
    {
      var conf =
        Component
          .For<TService1, TService2>()
          .ImplementedBy<TComponent>()
          .LifestyleTransient();

      Configure(conf, name, isDefault, isFallback, configuration);

      _windsorContainer.Register(conf);
      return this;
    }

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
    public IIocRegisterer RegisterTransient(
      Type serviceType,
      Type componentType,
      string name = null,
      bool isDefault = false,
      bool isFallback = false,
      Action<ComponentRegistration<object>> configuration = null,
      bool ifNotExists = false)
    {
      return RegisterComponent(serviceType, componentType, c => c.LifestyleTransient(), name, isDefault, isFallback, configuration, ifNotExists);
    }

    #region Options

    /// <summary>
    /// Adds services required for using options.
    /// </summary>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer AddOptions()
    {
      RegisterSingleton(typeof(IOptions<>), typeof(OptionsManager<>), name: typeof(IOptions<>).FullName + Guid.NewGuid().ToString(), isFallback: true);
      RegisterScoped(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>), name: typeof(IOptionsSnapshot<>).FullName + Guid.NewGuid().ToString(), isFallback: true);
      RegisterSingleton(typeof(IOptionsMonitor<>), typeof(OptionsMonitor<>), isFallback: true);
      RegisterTransient(typeof(IOptionsFactory<>), typeof(OptionsFactory<>), isFallback: true);
      RegisterSingleton(typeof(IOptionsMonitorCache<>), typeof(OptionsCache<>), isFallback: true);
      return this;
    }

    /// <summary>
    /// Registers an action used to configure a particular type of options.
    /// Note: These are run before all <seealso cref="PostConfigure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class
        => Configure(Options.DefaultName, configureOptions);

    /// <summary>
    /// Registers an action used to configure a particular type of options.
    /// Note: These are run before all <seealso cref="PostConfigure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="name">The name of the options instance.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer Configure<TOptions>(string name, Action<TOptions> configureOptions)
        where TOptions : class
    {
      if (configureOptions == null)
      {
        throw new ArgumentNullException(nameof(configureOptions));
      }

      AddOptions();
      RegisterInstance<IConfigureOptions<TOptions>>(new ConfigureNamedOptions<TOptions>(name, configureOptions));
      return this;
    }

    /// <summary>
    /// Registers an action used to configure all instances of a particular type of options.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer ConfigureAll<TOptions>(Action<TOptions> configureOptions) where TOptions : class
        => Configure(name: null, configureOptions: configureOptions);

    /// <summary>
    /// Registers an action used to initialize a particular type of options.
    /// Note: These are run after all <seealso cref="Configure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer PostConfigure<TOptions>(Action<TOptions> configureOptions) where TOptions : class
        => PostConfigure(Options.DefaultName, configureOptions);

    /// <summary>
    /// Registers an action used to configure a particular type of options.
    /// Note: These are run after all <seealso cref="Configure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configure.</typeparam>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="name">The name of the options instance.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer PostConfigure<TOptions>(string name, Action<TOptions> configureOptions)
        where TOptions : class
    {
      if (configureOptions == null)
      {
        throw new ArgumentNullException(nameof(configureOptions));
      }

      AddOptions();
      RegisterInstance<IPostConfigureOptions<TOptions>>(new PostConfigureOptions<TOptions>(name, configureOptions));
      return this;
    }

    /// <summary>
    /// Registers an action used to post configure all instances of a particular type of options.
    /// Note: These are run after all <seealso cref="Configure{TOptions}(IWindsorContainer, Action{TOptions})"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="configureOptions">The action used to configure the options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer PostConfigureAll<TOptions>(Action<TOptions> configureOptions) where TOptions : class
        => PostConfigure(name: null, configureOptions: configureOptions);

    /// <summary>
    /// Registers a type that will have all of its I[Post]ConfigureOptions registered.
    /// </summary>
    /// <typeparam name="TConfigureOptions">The type that will configure options.</typeparam>
    /// <param name="container">The container.</param>
    /// <returns>
    /// The <see cref="IWindsorContainer" /> so that additional calls can be chained.
    /// </returns>
    public IIocRegisterer ConfigureOptions<TConfigureOptions>() where TConfigureOptions : class
        => ConfigureOptions(typeof(TConfigureOptions));

    /// <summary>
    /// Registers a type that will have all of its I[Post]ConfigureOptions registered.
    /// </summary>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="configureType">The type that will configure options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer ConfigureOptions(Type configureType)
    {
      AddOptions();
      var serviceTypes = FindIConfigureOptions(configureType);
      foreach (var serviceType in serviceTypes)
      {
        RegisterTransient(serviceType, configureType);
      }
      return this;
    }

    /// <summary>
    /// Registers an object that will have all of its I[Post]ConfigureOptions registered.
    /// </summary>
    /// <param name="services">The <see cref="IWindsorContainer"/> to add the services to.</param>
    /// <param name="configureInstance">The instance that will configure options.</param>
    /// <returns>The <see cref="IWindsorContainer"/> so that additional calls can be chained.</returns>
    public IIocRegisterer ConfigureOptions(object configureInstance)
    {
      AddOptions();
      var serviceTypes = FindIConfigureOptions(configureInstance.GetType());
      foreach (var serviceType in serviceTypes)
      {
        RegisterInstance(serviceType, configureInstance);
      }
      return this;
    }

    private static bool IsAction(Type type)
        => (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Action<>));

    private static IEnumerable<Type> FindIConfigureOptions(Type type)
    {
      var serviceTypes = type.GetTypeInfo().ImplementedInterfaces
          .Where(t => t.GetTypeInfo().IsGenericType &&
          (t.GetGenericTypeDefinition() == typeof(IConfigureOptions<>)
          || t.GetGenericTypeDefinition() == typeof(IPostConfigureOptions<>)));
      if (!serviceTypes.Any())
      {
        throw new InvalidOperationException(
            IsAction(type)
            ? Resources.Error_NoIConfigureOptionsAndAction
            : Resources.Error_NoIConfigureOptions);
      }
      return serviceTypes;
    }

    #endregion Options

    #endregion IIocRegisterer
  }
}