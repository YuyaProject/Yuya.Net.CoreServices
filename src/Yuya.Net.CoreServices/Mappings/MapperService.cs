using System;
using AutoMapper;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// Mapper Service
  /// </summary>
  /// <seealso cref="TurkuazGO.Mappings.IMapperService" />
  public class MapperService : IMapperService
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MapperService"/> class.
    /// </summary>
    public MapperService()
    {
      var mapperConfiguration = new AutoMapper.MapperConfiguration(c=> { });

      SimpleMapper = mapperConfiguration.CreateMapper();
    }

    /// <summary>
    /// Gets the simple mapper.
    /// </summary>
    /// <value>
    /// The simple mapper.
    /// </value>
    public AutoMapper.IMapper SimpleMapper { get; }


    /// <summary>
    /// Creates the mapper.
    /// </summary>
    /// <param name="configure">The configure.</param>
    /// <returns></returns>
    public AutoMapper.IMapper CreateMapper(Action<IMapperConfigurationExpression> configure)
    {
      var mapperConfiguration = new AutoMapper.MapperConfiguration(configure);

      return mapperConfiguration.CreateMapper();
    }

    /// <summary>
    /// Creates the single mapper.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <returns></returns>
    public AutoMapper.IMapper CreateSingleMapper<TSource, TDestination>()
    {
      var mapper = CreateMapper(c => {
        c.CreateMap<TSource, TDestination>()
          .IgnoreDestinationUnmappedProperties();
      });

      return mapper;
    }

    /// <summary>
    /// Creates the single mapper.
    /// </summary>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    public AutoMapper.IMapper CreateSingleMapper(Type sourceType, Type destinationType)
    {
      var mapper = CreateMapper(c => {
        c.CreateMap(sourceType, destinationType)
          .IgnoreDestinationUnmappedProperties(sourceType, destinationType);
      });

      return mapper;
    }


    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    public TDestination SingleMap<TSource, TDestination>(TSource source)
    {
      return (TDestination)SingleMap(source, typeof(TSource), typeof(TDestination));
    }

    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    public TDestination SingleMap<TDestination>(object source)
    {
      return (TDestination)SingleMap(source, source.GetType(), typeof(TDestination));
    }

    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    public object SingleMap(object source, Type destinationType)
    {
      Type sourceType = source.GetType();

      return SingleMap(source, sourceType, destinationType);
    }

    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    public object SingleMap(object source, Type sourceType, Type destinationType)
    {
      var mapper = CreateMapper(c => {
        c.CreateMap(sourceType, destinationType)
          .IgnoreDestinationUnmappedProperties(sourceType, destinationType);
      });

      return mapper.Map(source, sourceType, destinationType);
    }
  }
}
