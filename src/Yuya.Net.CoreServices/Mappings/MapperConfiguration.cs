using AutoMapper;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// Mapper Configuration
  /// </summary>
  /// <typeparam name="TProfile">The type of the profile.</typeparam>
  /// <seealso cref="TurkuazGO.Mappings.IMapperConfiguration{TProfile}" />
  public class MapperConfiguration<TProfile> : IMapperConfiguration<TProfile>
    where TProfile : Profile
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MapperConfiguration{TProfile}" /> class.
    /// </summary>
    /// <param name="profile">The profile.</param>
    public MapperConfiguration(TProfile profile)
    {
      Profile = profile;
      Configuration = new MapperConfiguration(cfg => cfg.AddProfile(Profile));
      Configuration.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Gets the AutoMapper configuration instance. This value genereted in constructor.
    /// </summary>
    /// <value>
    /// The configuration instance.
    /// </value>
    public MapperConfiguration Configuration { get; }

    /// <summary>
    /// Gets the profile instance. this value filled in constuctor from DI.
    /// </summary>
    /// <value>
    /// The profile instance.
    /// </value>
    public TProfile Profile { get; }

    /// <summary>
    /// Creates the mapper factory method.
    /// </summary>
    /// <returns>
    ///   The <see cref="IMapper{TProfile}" /> instance
    /// </returns>
    public IMapper<TProfile> CreateMapper() => new Mapper<TProfile>(this);
  }
}
