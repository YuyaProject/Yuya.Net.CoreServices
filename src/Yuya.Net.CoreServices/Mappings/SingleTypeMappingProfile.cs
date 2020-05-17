using AutoMapper;

namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// Single Type Mapping Profile
  /// </summary>
  /// <typeparam name="TFirst">The type of the first.</typeparam>
  /// <typeparam name="TSecond">The type of the second.</typeparam>
  /// <seealso cref="AutoMapper.Profile" />
  public class SingleTypeMappingProfile<TFirst, TSecond> : Profile
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SingleTypeMappingProfile{TFirst, TSecond}" /> class.
    /// </summary>
    public SingleTypeMappingProfile()
    {
      CreateMap<TFirst, TSecond>()
        .IgnoreDestinationUnmappedProperties();

      if (typeof(TFirst) != typeof(TSecond))
      {
        CreateMap<TSecond, TFirst>()
          .IgnoreDestinationUnmappedProperties();
      }
    }
  }
}
