using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Yuya.Net.CoreServices.Json;
using Yuya.Net.CoreServices.Mappings;
using Yuya.Net.CoreServices.Reflection;
using Yuya.Net.CoreServices.Windsors;

namespace Yuya.Net.CoreServices
{
  public class YuyaCoreServicesFacility : AbstractFacility
  {
    public YuyaCoreServicesFacility()
    {
    }

    protected override void Init()
    {
      // windsor 
      Kernel.Register(
        Component
          .For<IIocRegisterer, IIocResolver>()
          .ImplementedBy<IocManager>()
          .LifestyleSingleton()
          .IsDefault(),
        Component.For<NamedComponentModelHelper>(),
        Component.For<NamedComponentsSubDependencyResolver>(),
        Component.For<MandatoryPropertyComponentModelHelper>(),
        Component.For<PerformanceLogInterceptor>(),
        Component.For<BeginScopeInterceptor>()

      );

      Kernel.Resolver.AddSubResolver(Kernel.Resolve<NamedComponentsSubDependencyResolver>());

      Kernel.ComponentModelBuilder.AddContributor(Kernel.Resolve<NamedComponentModelHelper>());
      Kernel.ComponentModelBuilder.AddContributor(Kernel.Resolve<MandatoryPropertyComponentModelHelper>());



      Kernel.Register(
        Component
          .For<IReflectionService>()
          .Instance(ReflectionService.Instance)
          .IsFallback(),
        Component
          .For<IConvertCompareService>()
          .ImplementedBy<ConvertCompareService>()
          .LifestyleSingleton(),
        Component
          .For<IDateTimeService>()
          .ImplementedBy<DateTimeService>()
          .LifestyleSingleton(),
        Component
          .For<IJsonConverter>()
          .ImplementedBy<DefaultJsonConverter>()
          .LifestyleSingleton()
          .IsFallback()
      );

      // Mappings
      Kernel.Register(
        Component
          .For<IMapperService>()
          .ImplementedBy<MapperService>()
          .LifestyleSingleton()
          .IsFallback(),

        Component
          .For(typeof(IMapperConfiguration<>))
          .ImplementedBy(typeof(MapperConfiguration<>))
          .LifestyleSingleton()
          .IsFallback(),

        Component
          .For(typeof(IMapper<>))
          .ImplementedBy(typeof(Mapper<>))
          .LifestyleSingleton()
          .IsFallback(),

        Component
          .For(typeof(ISingleTypeMapper<,>))
          .ImplementedBy(typeof(SingleTypeMapper<,>))
          .LifestyleSingleton()
          .IsFallback(),

        Component
          .For(typeof(SingleTypeMappingProfile<,>))
          .ImplementedBy(typeof(SingleTypeMappingProfile<,>))
          .LifestyleSingleton()
          .IsFallback()
        );
    }


    protected override void Dispose()
    {
      base.Dispose();
    }

  }
}