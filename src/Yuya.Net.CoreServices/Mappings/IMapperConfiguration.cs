using AutoMapper;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// The Mapper Configuration Generic Interface
  /// </summary>
  /// <typeparam name="TProfile">The type of the automapper profile.</typeparam>
  public interface IMapperConfiguration<out TProfile>
    where TProfile : Profile
  {
    /// <summary>
    /// Gets the AutoMapper configuration instance. This value genereted in constructor.
    /// </summary>
    /// <value>
    /// The configuration instance.
    /// </value>
    MapperConfiguration Configuration { get; }

    /// <summary>
    /// Gets the profile instance. this value filled in constuctor from DI.
    /// </summary>
    /// <value>
    /// The profile instance.
    /// </value>
    TProfile Profile { get; }

    /// <summary>
    /// Creates the mapper factory method.
    /// </summary>
    /// <returns>The <see cref="IMapper{TProfile}"/> instance</returns>
    IMapper<TProfile> CreateMapper();
  }
}
