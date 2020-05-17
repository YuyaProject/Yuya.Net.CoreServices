using AutoMapper;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// Single Type Mapper Interface
  /// </summary>
  /// <typeparam name="TFirst">The type of the first.</typeparam>
  /// <typeparam name="TSecond">The type of the second.</typeparam>
  /// <seealso cref="TurkuazGO.Mappings.IMapper" />
  public interface ISingleTypeMapper<TFirst, TSecond> : IMapper
    where TFirst : class
    where TSecond : class
  {
    /// <summary>
    /// Gets the automatic mapper.
    /// </summary>
    /// <value>
    /// The automatic mapper.
    /// </value>
    AutoMapper.IMapper AutoMapper { get; }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    MapperConfiguration Configuration { get; }
  }
}
