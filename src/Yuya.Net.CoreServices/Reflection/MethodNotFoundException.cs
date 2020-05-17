using System;
using System.Runtime.Serialization;

namespace Yuya.Net.CoreServices.Reflection
{
  /// <summary>
  /// Method Not Found Exception
  /// </summary>
  /// <seealso cref="System.Exception" />
  [Serializable]
  public class MethodNotFoundException : Exception
  {
    /// <summary>
    /// Gets the name of the method.
    /// </summary>
    /// <value>
    /// The name of the method.
    /// </value>
    public string MethodName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodNotFoundException" /> class.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    public MethodNotFoundException(string methodName)
    {
      this.MethodName = methodName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="methodName">Name of the method.</param>
    public MethodNotFoundException(string message, string methodName) : base(message)
    {
      this.MethodName = methodName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodNotFoundException" /> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public MethodNotFoundException(string message, string methodName, Exception innerException) : base(message, innerException)
    {
      this.MethodName = methodName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodNotFoundException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
    protected MethodNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
