using System;
using Moq;
using Shouldly;
using Xunit;
using Yuya.Net.CoreServices.Reflection;

namespace Yuya.Net.CoreServices.Tests
{

  public class ReflectionServiceTests
  {
    [Fact]
    public void InvokeGenericMethod_ObjectTypeIsNull_ThrowException()
    {
      var service = new ReflectionService();

      var exception = Assert.Throws<ArgumentNullException>(() => service.InvokeGenericMethod(null, null, null, null));
      exception.ShouldNotBeNull();
      exception.ParamName.ShouldBe("objectType");
    }

    [Fact]
    public void InvokeGenericMethod_MethodNameIsNullOrEmptyOrWhitespace_ThrowException()
    {
      var service = new ReflectionService();

      var exception = Assert.Throws<ArgumentNullException>(() => service.InvokeGenericMethod(typeof(Demo), null, null, null));
      exception.ShouldNotBeNull();
      exception.ParamName.ShouldBe("methodName");

      exception = Assert.Throws<ArgumentNullException>(() => service.InvokeGenericMethod(typeof(Demo), null, "", null));
      exception.ShouldNotBeNull();
      exception.ParamName.ShouldBe("methodName");

      exception = Assert.Throws<ArgumentNullException>(() => service.InvokeGenericMethod(typeof(Demo), null, "    ", null));
      exception.ShouldNotBeNull();
      exception.ParamName.ShouldBe("methodName");
    }

    [Fact]
    public void InvokeGenericMethod_GenericTypesIsNullOrEmpty_ThrowException()
    {
      var service = new ReflectionService();

      var exception = Assert.Throws<ArgumentNullException>(() => service.InvokeGenericMethod(typeof(Demo), null, "GenericStaticMethodDemo", null));
      exception.ShouldNotBeNull();
      exception.ParamName.ShouldBe("genericTypes");

      exception = Assert.Throws<ArgumentNullException>(() => service.InvokeGenericMethod(typeof(Demo), null, "GenericStaticMethodDemo", new Type[0]));
      exception.ShouldNotBeNull();
      exception.ParamName.ShouldBe("genericTypes");
    }

    [Fact]
    public void InvokeGenericMethod_NonStaticNoopMethod_Success()
    {
      var demoMock = new Mock<Demo>();

      var service = new ReflectionService();

      var result = service.InvokeGenericMethod(typeof(Demo), demoMock.Object, "NoopGeneric", new[] { typeof(int) });

      result.ShouldBeNull();
      demoMock.Verify(m => m.NoopGeneric<int>(), Times.Once);
    }

    [Fact]
    public void InvokeGenericMethod_StaticNoopMethod_Success()
    {
      var service = new ReflectionService();

      var result = service.InvokeGenericMethod(typeof(Demo), null, "NoopStaticGeneric", new[] { typeof(int) });

      result.ShouldBeNull();
    }

    [Fact]
    public void InvokeGenericMethod_NonGenericMethod_ThrowException()
    {
      var demo = new Demo();
      var service = new ReflectionService();

      var exception = Assert.Throws<MethodNotFoundException>(() => service.InvokeGenericMethod(typeof(Demo), demo, "NonGenericMethodDemo", new[] { typeof(int) }));
      exception.ShouldNotBeNull();
      exception.MethodName.ShouldBe("NonGenericMethodDemo");
    }

    [Fact]
    public void InvokeGenericMethod_NonGenericStaticMethod_ThrowException()
    {
      var service = new ReflectionService();

      var exception = Assert.Throws<MethodNotFoundException>(() => service.InvokeGenericMethod(typeof(Demo), null, "NonGenericStaticMethodDemo", new[] { typeof(int) }));
      exception.ShouldNotBeNull();
      exception.MethodName.ShouldBe("NonGenericStaticMethodDemo");
    }

    public class Demo
    {
      public virtual void NoopGeneric<T>()
      {
        // noop
      }


#pragma warning disable S2326 // Unused type parameters should be removed
      public static void NoopStaticGeneric<T>()
#pragma warning restore S2326 // Unused type parameters should be removed
      {
        // noop
      }

      public virtual void NoopNonGeneric()
      {
        // noop
      }

      public static void NoopStaticNonGeneric()
      {
        // noop
      }

      public static string GenericStaticMethodDemo<T>(T arg1)
      {
        return string.Format("{0}", arg1);
      }

      public string GenericMethodDemo<T>(T arg1)
      {
        return string.Format("{0}", arg1);
      }

      public static string NonGenericStaticMethodDemo(object arg1)
      {
        return string.Format("{0}", arg1);
      }

      public string NonGenericMethodDemo(object arg1)
      {
        return string.Format("{0}", arg1);
      }
    }
  }
}
