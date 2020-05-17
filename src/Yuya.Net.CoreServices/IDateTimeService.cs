using System;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Date Time Service Interface
  /// </summary>
  public interface IDateTimeService
  {
    /// <summary>
    /// Nows this instance.
    /// </summary>
    /// <returns></returns>
    DateTimeOffset Now();

    /// <summary>
    /// Converts to day.
    /// </summary>
    /// <returns></returns>
    DateTimeOffset Today();

    /// <summary>
    /// UTCs the now.
    /// </summary>
    /// <returns></returns>
    DateTimeOffset UtcNow();

    /// <summary>
    /// UTCs the today.
    /// </summary>
    /// <returns></returns>
    DateTimeOffset UtcToday();
  }
}
