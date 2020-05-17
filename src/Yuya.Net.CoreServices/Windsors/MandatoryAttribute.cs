using System;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Mandatory Attribute
  /// </summary>
  /// <seealso cref="System.Attribute" />
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class MandatoryAttribute : Attribute { }
}
