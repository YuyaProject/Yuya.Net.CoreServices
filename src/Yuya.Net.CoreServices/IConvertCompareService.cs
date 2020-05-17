namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Convert &amp; Compare Service Interface
  /// </summary>
  public interface IConvertCompareService
  {
    /// <summary>
    /// Greater than
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    bool GreaterThan<T>(T first, T second);

    /// <summary>
    /// Greater than and equals
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    bool GreaterThanEquals<T>(T first, T second);

    /// <summary>
    /// Less than
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    bool LessThan<T>(T first, T second);

    /// <summary>
    /// Less than and equal
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    bool LessThanEquals<T>(T first, T second);

    /// <summary>
    /// Equalses the specified first.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    bool Equals<T>(T first, T second);

    /// <summary>
    /// To the specified source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    T To<T>(object source);
  }
}
