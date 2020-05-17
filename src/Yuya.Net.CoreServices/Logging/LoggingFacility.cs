using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

namespace Yuya.Net.CoreServices.Logging
{
  /// <summary>
  ///   A facility for logging support.
  /// </summary>
  public class LoggingFacility : AbstractFacility
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="LoggingFacility" /> class.
    /// </summary>
    public LoggingFacility()
    {
    }

    /// <summary>
    /// Factory Initialization method.
    /// </summary>
    protected override void Init()
    {
      RegisterSubResolver();
      Kernel.Register(
        Component
          .For<Castle.Core.Logging.ILoggerFactory>()
          .ImplementedBy<CastleLoggerFactory>()
        ,
        Component
          .For<ITurkuazLoggerFactory>()
          .Instance(LogFactory.TurkuazLoggerFactory)
        );
    }

    private void RegisterSubResolver()
    {
      Kernel.Resolver.AddSubResolver(new LoggerResolver());
    }
  }
}
