using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Linq;
using System.Reflection;
using Yuya.Net.CoreServices.Reflection;
using Yuya.Net.CoreServices.Windsors;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// The extension methods of Configuration for Windsor Container.
  /// </summary>
  public static class AutomapperWindsorContainerExtensions
  {
    /// <summary>
    /// Adds the configuration instance to windsor container.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <returns>The configuration instance</returns>
    public static IIocRegisterer AddAutomapper(this IIocRegisterer container)
    {
      container.RegisterSingleton(typeof(IMapperConfiguration<>), typeof(MapperConfiguration<>));
      container.RegisterTransient(typeof(IMapper<>), typeof(Mapper<>));

      return container;
    }

    /// <summary>
    /// Registers the automapper profiles.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container">The container.</param>
    /// <returns></returns>
    public static IIocRegisterer RegisterAutomapperProfilesFrom<T>(this IIocRegisterer container)
    {
      return RegisterAutomapperProfilesFromAssembly(container, typeof(T).Assembly);
    }

    /// <summary>
    /// Registers the automapper profiles.
    /// </summary>
    /// <param name="registerer">The container.</param>
    /// <param name="assembly">The assembly.</param>
    /// <returns></returns>
    public static IIocRegisterer RegisterAutomapperProfilesFromAssembly(this IIocRegisterer registerer, Assembly assembly)
    {
      var mapperConfigurationInterfaceType = typeof(IMapperConfiguration<>);
      var mapperInterfaceType = typeof(IMapper<>);
      var mapperConfigurationType = typeof(MapperConfiguration<>);
      var mapperType = typeof(Mapper<>);

      var reflectionService = registerer.Resolver.Resolve<IReflectionService>();

      var profileTypes = reflectionService.GetTypesFromBaseTypeInAssembly<Profile>(assembly);

      foreach (var profileType in profileTypes)
      {
        if (!registerer.HasComponent(profileType))
        {
          registerer.RegisterScoped(profileType, profileType);
        }

        var mapperConfigurationInterface = mapperConfigurationInterfaceType.MakeGenericType(profileType);
        if (!registerer.HasComponent(mapperConfigurationInterface))
        {
          var mapperConfiguration = mapperConfigurationType.MakeGenericType(profileType);
          registerer.RegisterSingleton(mapperConfigurationInterface, mapperConfiguration);
        }

        var mapperInterface = mapperInterfaceType.MakeGenericType(profileType);
        if (!registerer.HasComponent(mapperInterface))
        {
          var mapper = mapperType.MakeGenericType(profileType);
          registerer.RegisterScoped(mapperInterface, mapper);
        }
      }

      return registerer;
    }

    /// <summary>
    /// Ignores the destination unmapped properties.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="mapping">The mapping.</param>
    /// <returns></returns>
    public static IMappingExpression<TSource, TDestination> IgnoreDestinationUnmappedProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
    {
      var ignorableProperties = GetIgnorableProperties<TSource, TDestination>();

      foreach (var ignorableProperty in ignorableProperties)
      {
        mapping.ForMember(ignorableProperty, m => m.Ignore());
      }

      return mapping;
    }

    /// <summary>
    /// Ignores the destination unmapped properties.
    /// </summary>
    /// <param name="mapping">The mapping.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    public static IMappingExpression IgnoreDestinationUnmappedProperties(this IMappingExpression mapping, Type sourceType, Type destinationType)
    {
      var ignorableProperties = GetIgnorableProperties(sourceType, destinationType);

      foreach (var ignorableProperty in ignorableProperties)
      {
        mapping.ForMember(ignorableProperty, m => m.Ignore());
      }

      return mapping;
    }


    /// <summary>
    /// Gets the ignorable properties.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <returns></returns>
    private static string[] GetIgnorableProperties<TSource, TDestination>()
    {
      var mapperConfiguration = new MapperConfiguration(c =>
      {
        c.CreateMap<TSource, TDestination>();
      });

      mapperConfiguration.CompileMappings();
      var mapp = mapperConfiguration.FindTypeMapFor(typeof(TSource), typeof(TDestination));
      return mapp.GetUnmappedPropertyNames()
        .Where(x => mapp.DestinationType.GetProperty(x) != null)
        .ToArray();
    }

    /// <summary>
    /// Gets the ignorable properties.
    /// </summary>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    private static string[] GetIgnorableProperties(Type sourceType, Type destinationType)
    {
      var mapperConfiguration = new MapperConfiguration(c =>
      {
        c.CreateMap(sourceType, destinationType);
      });

      mapperConfiguration.CompileMappings();
      var mapp = mapperConfiguration.FindTypeMapFor(sourceType, destinationType);
      return mapp.GetUnmappedPropertyNames()
        .Where(x => mapp.DestinationType.GetProperty(x) != null)
        .ToArray();
    }
  }
}
