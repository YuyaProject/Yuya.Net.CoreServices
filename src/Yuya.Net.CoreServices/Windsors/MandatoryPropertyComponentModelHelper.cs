using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using System.Linq;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Mandatory Property Component Model Helper
  /// </summary>
  /// <seealso cref="Castle.MicroKernel.ModelBuilder.IContributeComponentModelConstruction" />
  public class MandatoryPropertyComponentModelHelper : IContributeComponentModelConstruction
  {
    /// <summary>
    /// Usually the implementation will look in the configuration property
    /// of the model or the service interface, or the implementation looking for
    /// something.
    /// </summary>
    /// <param name="kernel">The kernel instance</param>
    /// <param name="model">The component model</param>
    public void ProcessModel(IKernel kernel, ComponentModel model)
    {
      foreach (var property in model.Properties)
      {
        if (property.Property.GetCustomAttributes(inherit: true).Any(x => x is MandatoryAttribute))
        {
          property.Dependency.IsOptional = false;
        }
      }
    }
  }
}
