using System;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// Mapper Service Interface
  /// </summary>
  public interface IMapperService
  {
    /// <summary>
    /// Gets the simple mapper.
    /// </summary>
    /// <value>
    /// The simple mapper.
    /// </value>
    AutoMapper.IMapper SimpleMapper { get; }

    /// <summary>
    /// Creates the mapper.
    /// </summary>
    /// <param name="configure">The configure.</param>
    /// <returns></returns>
    AutoMapper.IMapper CreateMapper(Action<AutoMapper.IMapperConfigurationExpression> configure);

    /// <summary>
    /// Creates the single mapper.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <returns></returns>
    AutoMapper.IMapper CreateSingleMapper<TSource, TDestination>();

    /// <summary>
    /// Creates the single mapper.
    /// </summary>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    AutoMapper.IMapper CreateSingleMapper(Type sourceType, Type destinationType);

    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    TDestination SingleMap<TSource, TDestination>(TSource source);

    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    TDestination SingleMap<TDestination>(object source);

    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    object SingleMap(object source, Type destinationType);

    /// <summary>
    /// Singles the map.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns></returns>
    object SingleMap(object source, Type sourceType, Type destinationType);
  }
}
