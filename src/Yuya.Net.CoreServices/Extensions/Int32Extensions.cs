namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Int32 object extensions
  /// </summary>
  public static class Int32Extensions
  {
    /// <summary>
    /// The given value is check between lower and upper boundary.
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="minValue">Lower boundary</param>
    /// <param name="maxValue">Upper boundary</param>
    /// <returns>If the given value is between bounderies or equals to boundaries, return true, otherwise false</returns>
    public static bool Between(this int value, int minValue, int maxValue)
    {
      return minValue <= value && maxValue >= value;
    }
  }
}