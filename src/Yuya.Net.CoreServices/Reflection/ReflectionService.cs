using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Yuya.Net.CoreServices.Reflection
{
  /// <summary>
  /// Reflection Service
  /// </summary>
  public class ReflectionService : IReflectionService
  {
    /// <summary>The instance</summary>
    public static readonly IReflectionService Instance = new ReflectionService();

    /// <summary>Gets the properties.</summary>
    /// <param name="type">The type.</param>
    /// <param name="flags">The flags.</param>
    /// <returns></returns>
    public virtual PropertyInfo[] GetProperties(Type type, BindingFlags? flags = null)
    {
      if (type.IsInterface)
      {
        var propertyInfos = new List<PropertyInfo>();

        var considered = new List<Type>();
        var queue = new Queue<Type>();
        considered.Add(type);
        queue.Enqueue(type);
        while (queue.Count > 0)
        {
          var subType = queue.Dequeue();
          foreach (var subInterface in subType.GetInterfaces())
          {
            if (considered.Contains(subInterface)) continue;

            considered.Add(subInterface);
            queue.Enqueue(subInterface);
          }

          var typeProperties = flags.HasValue ? subType.GetProperties(flags.Value) : subType.GetProperties();

          var newPropertyInfos = typeProperties
              .Where(x => !propertyInfos.Contains(x));

          propertyInfos.InsertRange(0, newPropertyInfos);
        }
        return propertyInfos.ToArray();
      }

      return flags.HasValue ? type.GetProperties(flags.Value) : type.GetProperties();
    }

    /// <summary>Gets the properties with values.</summary>
    /// <param name="data">The data.</param>
    /// <param name="flags">The flags.</param>
    /// <returns></returns>
    public virtual Dictionary<string, object> GetPropertiesWithValues(object data, BindingFlags? flags = null)
    {
      if (data == null) return new Dictionary<string, object>();
      var props = GetProperties(data.GetType(), flags);
      return props
        .Where(x => x.CanRead)
        .ToDictionary(x => x.Name, x => x.GetValue(data));
    }

    /// <summary>
    /// Gets the types from base type in assembly.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assembly">The assembly.</param>
    /// <returns></returns>
    public virtual Type[] GetTypesFromBaseTypeInAssembly<T>(Assembly assembly)
    {
      var baseType = typeof(T);
      return assembly.GetTypes()
        .Where(x => x.IsClass && x.IsPublic && baseType.IsAssignableFrom(x))
        .ToArray();
    }

    /// <summary>
    /// Makes the type of the generic.
    /// </summary>
    /// <param name="baseType">Type of the base.</param>
    /// <param name="parameterTypes">The parameter types.</param>
    /// <returns></returns>
    public virtual Type MakeGenericType(Type baseType, params Type[] parameterTypes)
    {
      return baseType.MakeGenericType(parameterTypes);
    }

    /// <summary>Makes the generic method.</summary>
    /// <param name="methodType">Type of the method.</param>
    /// <param name="parameterTypes">The parameter types.</param>
    /// <returns></returns>
    public virtual MethodInfo MakeGenericMethod(MethodInfo methodType, params Type[] parameterTypes)
    {
      return methodType.MakeGenericMethod(parameterTypes);
    }

    /// <summary>
    /// Creates the instance.
    /// </summary>
    /// <param name="instanceType">Type of the instance.</param>
    /// <param name="constructorParameters">The constructor parameters.</param>
    /// <returns></returns>
    public virtual object CreateInstance(Type instanceType, params object[] constructorParameters)
    {
      return Activator.CreateInstance(instanceType, constructorParameters);
    }

    /// <summary>
    /// Creates the instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="constructorParameters">The constructor parameters.</param>
    /// <returns></returns>
    public virtual T CreateInstance<T>(params object[] constructorParameters)
    {
      return (T)(Activator.CreateInstance(typeof(T), constructorParameters));
    }

    /// <summary>
    /// Invokes the generic method.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="genericTypes">The generic types.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    public virtual object InvokeGenericMethod(Type objectType, object objectValue, string methodName, Type[] genericTypes, params object[] parameterValues)
    {
      if (objectType == null) throw new ArgumentNullException(nameof(objectType));
      if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentNullException(nameof(methodName));
      if (genericTypes == null || genericTypes.Length == 0) throw new ArgumentNullException(nameof(genericTypes));

      var flag = objectValue == null ? BindingFlags.Static : BindingFlags.Instance;
      var methods = objectType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | flag);
      var method = methods.FirstOrDefault(x => x.Name == methodName
        && x.IsGenericMethodDefinition
        && x.GetGenericArguments().Length == genericTypes.Length
        && x.GetParameters().Length >= parameterValues.Length
        && x.GetParameters().Count(y => !y.IsOptional) <= parameterValues.Length);

      return MakeGenericMethod(method ?? throw new MethodNotFoundException($"Method '{methodName}' not found", methodName), genericTypes)
        .Invoke(objectValue, parameterValues);
    }

    /// <summary>Invokes the generic method asynchronous.</summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="genericTypes">The generic types.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns>the method result</returns>
    /// <exception cref="ArgumentNullException">
    /// objectType
    /// or
    /// methodName
    /// or
    /// genericTypes
    /// </exception>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    public virtual Task InvokeGenericMethodAsync(Type objectType, object objectValue, string methodName, Type[] genericTypes, params object[] parameterValues)
    {
      if (objectType == null) throw new ArgumentNullException(nameof(objectType));
      if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentNullException(nameof(methodName));
      if (genericTypes == null || genericTypes.Length == 0) throw new ArgumentNullException(nameof(genericTypes));

      var flag = objectValue == null ? BindingFlags.Static : BindingFlags.Instance;
      var methods = objectType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | flag);
      var method = methods.FirstOrDefault(x => x.Name == methodName
        && x.IsGenericMethodDefinition
        && x.GetGenericArguments().Length == genericTypes.Length
        && x.GetParameters().Length >= parameterValues.Length
        && x.GetParameters().Count(y => !y.IsOptional) <= parameterValues.Length);

      var result = MakeGenericMethod((method ?? throw new MethodNotFoundException($"Method '{methodName}' not found", methodName)), genericTypes)
        .Invoke(objectValue, parameterValues);
      return result as Task;
    }

    /// <summary>
    /// Invokes the non generic method.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    public virtual object InvokeNonGenericMethod(Type objectType, object objectValue, string methodName, params object[] parameterValues)
    {
      MethodInfo[] methods;
      if (objectValue == null)
      {
        methods = objectType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
      }
      else
      {
        methods = objectType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      }
      var method = methods.FirstOrDefault(x =>
        x.Name == methodName
        && x.GetParameters().Length >= parameterValues.Length
        && x.GetParameters().Count(y => !y.IsOptional) <= parameterValues.Length);

      return (method ?? throw new MethodNotFoundException($"Method '{methodName}' not found", methodName))
          .Invoke(objectValue, parameterValues);
    }

    /// <summary>
    /// Invokes the non generic method.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    public virtual Task InvokeNonGenericMethodAsync(Type objectType, object objectValue, string methodName, params object[] parameterValues)
    {
      MethodInfo[] methods;
      if (objectValue == null)
      {
        methods = objectType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
      }
      else
      {
        methods = objectType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      }
      var method = methods.FirstOrDefault(x =>
        x.Name == methodName
        && x.GetParameters().Length >= parameterValues.Length
        && x.GetParameters().Count(y => !y.IsOptional) <= parameterValues.Length);

      return (method ?? throw new MethodNotFoundException($"Method '{methodName}' not found", methodName))
        .Invoke(objectValue, parameterValues) as Task;
    }

    /// <summary>Invokes the non generic method.</summary>
    /// <param name="objectValue">The object value.</param>
    /// <param name="method">The method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    public virtual object Invoke(object objectValue, MethodInfo method, params object[] parameterValues)
    {
      if (method == null) throw new ArgumentNullException(nameof(method));
      return method.Invoke(objectValue, parameterValues);
    }

    /// <summary>Invokes the non generic method.</summary>
    /// <param name="objectValue">The object value.</param>
    /// <param name="method">The method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    public virtual Task InvokeAsync(object objectValue, MethodInfo method, params object[] parameterValues)
    {
      if (method == null) throw new ArgumentNullException(nameof(method));
      return method.Invoke(objectValue, parameterValues) as Task;
    }

    /// <summary>Gets the properties with values.</summary>
    /// <param name="pocoObject">The poco object.</param>
    /// <returns>List of properties with values</returns>
    public virtual List<(string Name, object Value)> GetPropertiesWithValues(object pocoObject)
    {
      if (pocoObject == null) return new List<(string Name, object Value)>();
      return pocoObject.GetType()
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Select(x => (x.Name, Value: x.GetValue(pocoObject, null)))
        .ToList();
    }

    /// <summary>
    /// Gets the custom attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public virtual TAttribute GetCustomAttribute<TAttribute>(Type type)
      where TAttribute : Attribute
    {
      return type?.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
    }

    /// <summary>
    /// Gets the custom attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="member">The type.</param>
    /// <returns></returns>
    public virtual TAttribute GetCustomAttribute<TAttribute>(MemberInfo member)
      where TAttribute : Attribute
    {
      return member?.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
    }

    /// <summary>
    /// Gets the custom attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="member">The member.</param>
    /// <returns></returns>
    public virtual TAttribute GetCustomAttribute<TAttribute>(ParameterInfo member)
      where TAttribute : Attribute
    {
      return member?.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;
    }

    /// <summary>
    /// Gets the custom attributes.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public virtual TAttribute[] GetCustomAttributes<TAttribute>(Type type)
      where TAttribute : Attribute
    {
      return type?.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().ToArray();
    }

    /// <summary>
    /// Gets the custom attributes.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="member">The member.</param>
    /// <returns></returns>
    public virtual TAttribute[] GetCustomAttributes<TAttribute>(MemberInfo member)
      where TAttribute : Attribute
    {
      return member?.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().ToArray();
    }

    /// <summary>
    /// Gets the generic type for hierarchy.
    /// </summary>
    /// <param name="genericBaseType">Type of the generic base.</param>
    /// <param name="implementation">The implementation.</param>
    /// <returns></returns>
    public virtual Type GetGenericTypeForHierarchy(Type genericBaseType, Type implementation)
    {
      if (implementation == null) return null;
      if (implementation.BaseType == null || implementation.BaseType == typeof(object)) return null;
      if (implementation.BaseType.IsGenericType && implementation.BaseType.GetGenericTypeDefinition() == genericBaseType)
      {
        return implementation.BaseType;
      }
      return GetGenericTypeForHierarchy(genericBaseType, implementation.BaseType);
    }

    /// <summary>
    /// Gets the generic interface for hierarchy.
    /// </summary>
    /// <param name="genericInterfaceType">Type of the generic interface.</param>
    /// <param name="implementation">The implementation.</param>
    /// <returns></returns>
    public virtual Type GetGenericInterfaceForHierarchy(Type genericInterfaceType, Type implementation)
    {
      if (!genericInterfaceType.IsInterface) return null;
      if (implementation == null) return null;
      if (implementation.IsInterface)
      {
        if (implementation.IsGenericType && implementation.GetGenericTypeDefinition() == genericInterfaceType) return implementation;
        return implementation.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterfaceType);
      }
      if (implementation.BaseType == null || implementation.BaseType == typeof(object)) return null;

      var allInterfaces = implementation.GetInterfaces();
      return allInterfaces.FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterfaceType);
    }

    /// <summary>
    /// Gets all public constant values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public virtual List<T> GetAllPublicConstantValues<T>(Type type)
    {
      return type
          .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
          .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
          .Select(x => (T)x.GetRawConstantValue())
          .ToList();
    }

    /// <summary>
    /// Gets all public constants.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public virtual List<FieldInfo> GetAllPublicConstants<T>(Type type)
    {
      return type
          .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
          .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
          .ToList();
    }

    /// <summary>
    /// Determines whether [is generic task].
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if [is generic task] [the specified type]; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsGenericTask(Type type)
    {
      return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>);
    }

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if the specified type is task; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsTask(Type type)
    {
      return (typeof(Task).IsAssignableFrom(type));
    }

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is task; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsTask(object obj)
    {
      if (obj == null) return false;
      if (obj.GetType() == typeof(Task)) return true;
      return obj is Task;
    }

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if the specified type is task; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsTask<T>(Type type)
    {
      return (typeof(Task<T>).IsAssignableFrom(type));
    }

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is task; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool IsTask<T>(object obj)
    {
      if (obj == null) return false;
      if (obj.GetType() == typeof(Task<T>)) return true;
      return obj is Task<T>;
    }

    /// <summary>
    /// Gets the type of the task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public virtual Type GetTaskType<T>(Type type)
    {
      if (!IsTask<T>(type)) { return null; }
      var t = type;
      do
      {
        if (t.IsGenericType && t == typeof(Task<T>)) return t;
        t = t.BaseType;
      } while (t != null);

      return null;
    }

    /// <summary>
    /// Gets the type of the task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public virtual Type GetTaskType<T>(object obj)
    {
      if (obj == null) return null;
      var t = obj.GetType();
      if (!IsTask<T>(t)) { return null; }
      do
      {
        if (t.IsGenericType && t == typeof(Task<T>)) return t;
        t = t.BaseType;
      } while (t != null);

      return null;
    }

    /// <summary>
    /// Gets the type of the task type generic parameter.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public virtual Type GetTaskTypeGenericParameterType(Type type)
    {
      if (!IsTask(type)) { return null; }
      var t = type;
      do
      {
        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Task<>)) return t.GetGenericArguments().First();
        t = t.BaseType;
      } while (t != null);

      return null;
    }

    /// <summary>
    /// Gets the type of the task type generic parameter.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public virtual Type GetTaskTypeGenericParameterType(object obj)
    {
      if (obj == null) return null;
      var t = obj.GetType();
      if (!IsTask(t)) { return null; }
      do
      {
        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Task<>)) return t.GetGenericArguments().First();
        t = t.BaseType;
      } while (t != null);

      return null;
    }
  }
}
