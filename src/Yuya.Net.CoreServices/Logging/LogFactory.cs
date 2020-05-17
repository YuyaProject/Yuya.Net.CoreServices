using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Yuya.Net.CoreServices.Windsors;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  /// The Log Factory
  /// </summary>
  public class LogFactory
  {
    private ILoggerRepository _loggerRepository;
    private LoggerFactory _loggerFactory;

    internal IIocRegisterer InternalApplication = null;

    /// <summary>Configures the logger.</summary>
    /// <param name="path">The path of log4net.config.xml .</param>
    /// <param name="configFileName">Name of the configuration file. if you set this value, this method read only this filename. Otherwise this method uses the default filename. Default file names are log4net.config.xml and log4net.config</param>
    /// <param name="repositoryAssembly">The repository assembly.</param>
    public LogFactory(string path = null, string configFileName = null, Assembly repositoryAssembly = null)
    {
      if (string.IsNullOrWhiteSpace(path)) path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      var fns = string.IsNullOrWhiteSpace(configFileName)
        ? new[]{
          Path.Combine(path, "config", "log4net.config.xml"),
          Path.Combine(path, "log4net.config.xml"),
          Path.Combine(path, "config", "log4net.config"),
          Path.Combine(path, "log4net.config"),
        }
        : new[]{
          Path.Combine(path, "config", configFileName),
          Path.Combine(path, configFileName),
        };

      var fn = fns.FirstOrDefault(x => File.Exists(x));

      if (!string.IsNullOrWhiteSpace(fn))
      {
        _loggerRepository = LogManager.CreateRepository(repositoryAssembly ?? Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly(),
                 typeof(log4net.Repository.Hierarchy.Hierarchy));
        XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo(fn));

        var log4NetProvider = new Log4NetProvider(new Log4NetProviderOptions { ExternalConfigurationSetup = true });
        _loggerFactory = new LoggerFactory(new[] { log4NetProvider });
      }
    }

    /// <summary>Configures the logger.</summary>
    internal LogFactory(ILoggerRepository loggerRepository)
    {
      _loggerRepository = loggerRepository;

      var log4NetProvider = new Log4NetProvider(new Log4NetProviderOptions { ExternalConfigurationSetup = true });
      _loggerFactory = new LoggerFactory(new[] { log4NetProvider });
    }

    /// <summary>Gets the turkuaz logger factory.</summary>
    /// <value>The turkuaz logger factory.</value>
    public ITurkuazLoggerFactory TurkuazLoggerFactory { get; } = new TurkuazLoggerFactory();

    /// <summary>Configures the turkuaz logging.</summary>
    /// <param name="registerer">The application.</param>
    /// <param name="path">The path.</param>
    /// <param name="configFileName">Name of the configuration file.</param>
    /// <param name="repositoryAssembly">The repository assembly.</param>
    /// <returns></returns>
    public void Configure(IIocRegisterer registerer)
    {
      //application.Container.RegisterTurkuazLogging();

      //return application;
      //this.InternalApplication = application;
      RegisterYuyaLogging(registerer);
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>The log instance</returns>
    internal ILog GetInternalLogger(string name)
    {
      return LogManager.GetLogger(_loggerRepository.Name, name);
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="type">The type for logger.</param>
    /// <returns>The log instance</returns>
    internal ILog GetInternalLogger(Type type)
    {
      return LogManager.GetLogger(_loggerRepository.Name, type);
    }

    /// <summary>Gets the logger.</summary>
    /// <typeparam name="T">The log name</typeparam>
    /// <returns>The log instance</returns>
    internal ILog GetIntenalLogger<T>()
    {
      return LogManager.GetLogger(_loggerRepository.Name, typeof(T));
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>The log instance</returns>
    public ILogger GetLogger(string name)
    {
      return TurkuazLoggerFactory.GetLogger(name);
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The log instance</returns>
    public ILogger GetLogger(Type type)
    {
      return TurkuazLoggerFactory.GetLogger(type);
    }

    /// <summary>Gets the logger.</summary>
    /// <typeparam name="T">The log name</typeparam>
    /// <returns>The log instance</returns>
    public ILogger GetLogger<T>()
    {
      return TurkuazLoggerFactory.GetLogger<T>();
    }

    /// <summary>
    /// Sets the logging builder.
    /// </summary>
    /// <param name="loggingBuilder">The logging builder.</param>
    public void SetLog4Net(ILoggingBuilder loggingBuilder)
    {
      loggingBuilder.ClearProviders();
      loggingBuilder.AddLog4Net(new Log4NetProviderOptions() { ExternalConfigurationSetup = true, });
    }

    /// <summary>
    /// Registers the turkuaz logging.
    /// </summary>
    /// <param name="container">The container.</param>
    public void RegisterYuyaLogging(IIocRegisterer registerer)
    {
      this.InternalApplication = registerer;
      registerer.RegisterInstance<Microsoft.Extensions.Logging.ILoggerFactory>(_loggerFactory);
      registerer.AddFacility<LoggingFacility>();
    }
  }
}