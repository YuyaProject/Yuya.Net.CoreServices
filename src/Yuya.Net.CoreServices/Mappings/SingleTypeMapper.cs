namespace Yuya.Net.CoreServices.Mappings
{
  /// <summary>
  /// Single Type Mapper
  /// </summary>
  /// <typeparam name="TFirst">The type of the first.</typeparam>
  /// <typeparam name="TSecond">The type of the second.</typeparam>
  /// <seealso cref="TurkuazGO.Mappings.Mapper{TProfile}" />
  /// <seealso cref="TurkuazGO.Mappings.ISingleTypeMapper{TFirst, TSecond}" />
  public class SingleTypeMapper<TFirst, TSecond> : Mapper<SingleTypeMappingProfile<TFirst, TSecond>>, ISingleTypeMapper<TFirst, TSecond>
    where TFirst : class
    where TSecond : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SingleTypeMapper{TFirst, TSecond}"/> class.
    /// </summary>
    /// <param name="mapperConfiguration">The mapper configuration.</param>
    public SingleTypeMapper(IMapperConfiguration<SingleTypeMappingProfile<TFirst, TSecond>> mapperConfiguration) : base(mapperConfiguration)
    {
    }
  }
}
