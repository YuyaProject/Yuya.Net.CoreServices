using System;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Date Time Service
  /// </summary>
  /// <seealso cref="TurkuazGO.IDateTimeService" />
  public class DateTimeService : IDateTimeService
  {
    /// <summary>
    /// Nows this instance.
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset Now()
    {
      return DateTimeOffset.Now;
    }

    /// <summary>
    /// UTCs the now.
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset UtcNow()
    {
      return DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Converts to day.
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset Today()
    {
      return DateTimeOffset.Now.Date;
    }

    /// <summary>
    /// UTCs the today.
    /// </summary>
    /// <returns></returns>
    public DateTimeOffset UtcToday()
    {
      return DateTimeOffset.UtcNow.Date;
    }
  }
}
