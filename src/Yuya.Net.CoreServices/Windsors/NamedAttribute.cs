using System;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Named Attribute
  /// </summary>
  /// <seealso cref="System.Attribute" />
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Class, AllowMultiple = false)]
  public class NamedAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NamedAttribute"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    public NamedAttribute(string name)
    {
      Name = name;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; }
  }
}
