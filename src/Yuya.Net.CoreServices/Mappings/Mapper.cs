using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TurkuazGO.Errors;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// Mappe Generic Class
  /// </summary>
  /// <typeparam name="TProfile">The type of the profile.</typeparam>
  /// <seealso cref="TurkuazGO.Mappings.IMapper{TProfile}" />
  public class Mapper<TProfile> : IMapper<TProfile>
    where TProfile : Profile
  {
    private readonly IMapperConfiguration<TProfile> _mapperConfiguration;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mapper{TProfile}" /> class.
    /// </summary>
    /// <param name="mapperConfiguration">The mapper configuration.</param>
    public Mapper(IMapperConfiguration<TProfile> mapperConfiguration)
    {
      _mapperConfiguration = mapperConfiguration;
      AutoMapper = mapperConfiguration.Configuration.CreateMapper();
    }

    /// <summary>
    /// Gets the automatic mapper.
    /// </summary>
    /// <value>
    /// The automatic mapper.
    /// </value>
    public AutoMapper.IMapper AutoMapper { get; }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public MapperConfiguration Configuration => _mapperConfiguration.Configuration;

    /// <summary>
    /// Gets the profile.
    /// </summary>
    /// <value>
    /// The profile.
    /// </value>
    public TProfile Profile => _mapperConfiguration.Profile;

    /// <summary>
    /// Execute a mapping from the source object to a new destination object. The source
    /// type is inferred from the source object.
    /// </summary>
    /// <typeparam name="TDestination">Destination type to create.</typeparam>
    /// <param name="source">Source object to map from.</param>
    /// <returns>Mapped destination object</returns>
    public TDestination Map<TDestination>(object source)
      => AutoMapper.Map<TDestination>(source);

    /// <summary>
    /// Execute a mapping from the source object to the existing destination object.
    /// </summary>
    /// <typeparam name="TSource">Source type to use</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <param name="source">Source object to map from</param>
    /// <param name="destination">Destination object to map into</param>
    /// <returns>
    /// The mapped destination object, same instance as the destination object
    /// </returns>
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
      => AutoMapper.Map(source, destination);

    /// <summary>
    /// Execute a mapping from the source object to a new destination object with supplied mapping options.
    /// </summary>
    /// <typeparam name="TDestination">Destination type to create.</typeparam>
    /// <param name="source">Source object to map from.</param>
    /// <param name="opts">Mapping options.</param>
    /// <returns>
    /// Mapped destination object
    /// </returns>
    public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
      => AutoMapper.Map<TDestination>(source, opts);

    /// <summary>
    /// Execute a mapping from the source object to a new destination object.
    /// </summary>
    /// <typeparam name="TSource">Source type to use, regardless of the runtime type</typeparam>
    /// <typeparam name="TDestination">Destination type to create</typeparam>
    /// <param name="source">Source object to map from</param>
    /// <returns>
    /// Mapped destination object
    /// </returns>
    public TDestination Map<TSource, TDestination>(TSource source)
      => AutoMapper.Map<TSource, TDestination>(source);

    /// <summary>
    /// Execute a mapping from the source object to a new destination object with supplied mapping options.
    /// </summary>
    /// <typeparam name="TSource">Source type to use</typeparam>
    /// <typeparam name="TDestination">Destination type to create</typeparam>
    /// <param name="source">Source object to map from</param>
    /// <param name="opts">Mapping options</param>
    /// <returns>
    /// Mapped destination object
    /// </returns>
    public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
      => AutoMapper.Map(source, opts);

    /// <summary>
    /// Execute a mapping from the source object to the existing destination object with supplied mapping options.
    /// </summary>
    /// <typeparam name="TSource">Source type to use</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <param name="source">Source object to map from</param>
    /// <param name="destination">Destination object to map into</param>
    /// <param name="opts">Mapping options</param>
    /// <returns>
    /// The mapped destination object, same instance as the destination object
    /// </returns>
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
      => AutoMapper.Map(source, destination, opts);

    /// <summary>
    /// Execute a mapping from the source object to a new destination object with explicit
    /// System.Type objects
    /// </summary>
    /// <param name="source">Source object to map from</param>
    /// <param name="sourceType">Source type to use</param>
    /// <param name="destinationType">Destination type to create</param>
    /// <returns>
    /// Mapped destination object
    /// </returns>
    public object Map(object source, Type sourceType, Type destinationType)
      => AutoMapper.Map(source, sourceType, destinationType);

    /// <summary>
    /// Execute a mapping from the source object to a new destination object with explicit
    /// System.Type objects and supplied mapping options.
    /// </summary>
    /// <param name="source">Source object to map from</param>
    /// <param name="sourceType">Source type to use</param>
    /// <param name="destinationType">Destination type to create</param>
    /// <param name="opts">Mapping options</param>
    /// <returns>
    /// Mapped destination object, same instance as the destination object
    /// </returns>
    public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
      => AutoMapper.Map(source, sourceType, destinationType, opts);

    /// <summary>
    /// Execute a mapping from the source object to existing destination object with
    /// explicit System.Type objects
    /// </summary>
    /// <param name="source">Source object to map from</param>
    /// <param name="destination">Destination object to map into</param>
    /// <param name="sourceType">Source type to use</param>
    /// <param name="destinationType">Destination type to use</param>
    /// <returns>
    /// Mapped destination object, same instance as the destination object
    /// </returns>
    public object Map(object source, object destination, Type sourceType, Type destinationType)
      => AutoMapper.Map(source, destination, sourceType, destinationType);

    /// <summary>
    /// Execute a mapping from the source object to existing destination object with
    /// supplied mapping options and explicit System.Type objects
    /// </summary>
    /// <param name="source">Source object to map from</param>
    /// <param name="destination">Destination object to map into</param>
    /// <param name="sourceType">Source type to use</param>
    /// <param name="destinationType">Destination type to use</param>
    /// <param name="opts">Mapping options</param>
    /// <returns>
    /// Mapped destination object, same instance as the destination object
    /// </returns>
    public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
      => AutoMapper.Map(source, destination, sourceType, destinationType, opts);

    /// <summary>
    /// Project the input queryable.
    /// </summary>
    /// <typeparam name="TDestination">Destination type to map to</typeparam>
    /// <param name="source">Queryable source</param>
    /// <param name="parameters">Optional parameter object for parameterized mapping expressions</param>
    /// <param name="membersToExpand">Explicit members to expand</param>
    /// <returns>
    /// Queryable result, use queryable extension methods to project and execute result
    /// </returns>
    /// <remarks>
    /// Projections are only calculated once and cached
    /// </remarks>
    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
      => AutoMapper.ProjectTo(source, parameters, membersToExpand);

    /// <summary>
    /// Project the input queryable.
    /// </summary>
    /// <typeparam name="TDestination">Destination type to map to</typeparam>
    /// <param name="source">Queryable source</param>
    /// <param name="parameters">Optional parameter object for parameterized mapping expressions</param>
    /// <param name="membersToExpand">Explicit members to expand</param>
    /// <returns>
    /// Queryable result, use queryable extension methods to project and execute result
    /// </returns>
    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
      => AutoMapper.ProjectTo<TDestination>(source, parameters, membersToExpand);

    /// <summary>
    /// Gets the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public IResult<TValue> GetResult<TValue>(object value)
      => new Result<TValue>(AutoMapper.Map<TValue>(value));

    /// <summary>
    /// Gets the result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error.</param>
    /// <returns></returns>
    public IResult<TValue> GetResult<TValue>(Error error)
      => new Result<TValue>(error);
  }
}
