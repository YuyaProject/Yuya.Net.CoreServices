using System;
using System.Collections.Generic;
using System.Linq;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  /// Error Message Base Class
  /// </summary>
  public class ErrorMessage : ICloneable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
    /// </summary>
    public ErrorMessage()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorMessage"/> class from exist <see cref="ErrorMessage"/> instance.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    public ErrorMessage(ErrorMessage errorMessage)
    {
      if (errorMessage == null) return;
      this.RawMessage = errorMessage.RawMessage;
      this.Url = errorMessage.Url != null ? new Uri(errorMessage.Url.ToString()) : null;
      this.UrlReferrer = errorMessage.UrlReferrer != null ? new Uri(errorMessage.UrlReferrer.ToString()) : null;
      this.FilePath = errorMessage.FilePath;
      this.HttpMethod = errorMessage.HttpMethod;
      this.RequestId = errorMessage.RequestId;
      this.SessionId = errorMessage.SessionId;
      this.AdditionalData = errorMessage.AdditionalData.ToDictionary(x => x.Key, x => x.Value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorMessage"/> class with raw message.
    /// </summary>
    /// <param name="rawMessage">The raw message.</param>
    public ErrorMessage(string rawMessage)
    {
      RawMessage = rawMessage;
    }

    /// <summary>Gets or sets the raw message.</summary>
    /// <value>The raw message.</value>
    public string RawMessage { get; protected internal set; }

    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    public Uri Url { get; protected internal set; }

    /// <summary>Gets or sets the URL referrer.</summary>
    /// <value>The URL referrer.</value>
    public Uri UrlReferrer { get; protected internal set; }

    /// <summary>Gets or sets the file path.</summary>
    /// <value>The file path.</value>
    public string FilePath { get; protected internal set; }

    /// <summary>Gets or sets the HTTP method.</summary>
    /// <value>The HTTP method.</value>
    public string HttpMethod { get; protected internal set; }

    /// <summary>Gets or sets the request identifier.</summary>
    /// <value>The request identifier.</value>
    public string RequestId { get; protected internal set; }

    /// <summary>Gets or sets the session identifier.</summary>
    /// <value>The session identifier.</value>
    public string SessionId { get; protected internal set; }

    /// <summary>Gets or sets the additional data.</summary>
    /// <value>The additional data.</value>
    public Dictionary<string, object> AdditionalData { get; protected internal set; } = new Dictionary<string, object>();

    /// <summary>Creates a new object that is a copy of the current instance.</summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual object Clone()
    {
      return new ErrorMessage()
      {
        RawMessage = RawMessage,
        FilePath = FilePath,
        HttpMethod = HttpMethod,
        RequestId = RequestId,
        Url = Url != null ? new Uri(Url.ToString()) : null,
        UrlReferrer = UrlReferrer != null ? new Uri(UrlReferrer.ToString()) : null,
        AdditionalData = AdditionalData?.ToDictionary(x => x.Key, x => x.Value)
      };
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return (RawMessage != null) ? RawMessage : base.ToString();
    }
  }
}
