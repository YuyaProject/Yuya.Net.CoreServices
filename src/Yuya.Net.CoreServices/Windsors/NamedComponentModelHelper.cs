using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using System.Linq;
using Yuya.Net.CoreServices.Reflection;

namespace Yuya.Net.CoreServices.Windsors
{
  /// <summary>
  /// Named Component Model Helper
  /// </summary>
  /// <seealso cref="Castle.MicroKernel.ModelBuilder.IContributeComponentModelConstruction" />
  public class NamedComponentModelHelper : IContributeComponentModelConstruction
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
      var reflectionService = kernel.Resolve<IReflectionService>();

      ProcessForClass(reflectionService, model);

      ProcessForProperties(reflectionService, model);

      ProcessForParameters(reflectionService, model);
    }

    private static void ProcessForClass(IReflectionService reflectionService, ComponentModel model)
    {
      var classAttribute = reflectionService.GetCustomAttribute<NamedAttribute>(model.Implementation);
      if (classAttribute != null)
      {
        model.Name = classAttribute.Name;
      }
    }

    private static void ProcessForProperties(IReflectionService reflectionService, ComponentModel model)
    {
      var properties = model.Properties
              .Select(property => (property: property, attribute: reflectionService.GetCustomAttribute<NamedAttribute>(property.Property)))
              .Where(x => x.attribute != null);

      foreach (var property in properties)
      {
        property.property.Dependency.DependencyKey = property.attribute.Name;
      }
    }

    private static void ProcessForParameters(IReflectionService reflectionService, ComponentModel model)
    {
      var parameters = model.Dependencies
              .OfType<ConstructorDependencyModel>()
              .Where(x => !x.IsPrimitiveTypeDependency)
              .ToList();

      foreach (var parameter in parameters)
      {
        var parameterName = parameter.DependencyKey;
        var parameterInfo = parameter.Constructor.Constructor.GetParameters().First(x => x.Name == parameterName);
        var attribute = reflectionService.GetCustomAttribute<NamedAttribute>(parameterInfo);
        if (attribute != null)
        {
          parameter.DependencyKey = attribute.Name;
        }
      }
    }
  }
}
