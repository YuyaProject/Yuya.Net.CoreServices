using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Yuya.Net.CoreServices.Reflection
{
  /// <summary>Reflection Service Interface</summary>
  public interface IReflectionService
  {
    /// <summary>Gets the properties.</summary>
    /// <param name="type">The type.</param>
    /// <param name="flags">The flags.</param>
    /// <returns></returns>
    PropertyInfo[] GetProperties(Type type, BindingFlags? flags = null);

    /// <summary>Gets the types from base type in assembly.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assembly">The assembly.</param>
    /// <returns></returns>
    Type[] GetTypesFromBaseTypeInAssembly<T>(Assembly assembly);

    /// <summary>Makes the type of the generic.</summary>
    /// <param name="baseType">Type of the base.</param>
    /// <param name="parameterTypes">The parameter types.</param>
    /// <returns>The non-generic type</returns>
    Type MakeGenericType(Type baseType, params Type[] parameterTypes);

    /// <summary>Makes the generic method.</summary>
    /// <param name="methodType">Type of the method.</param>
    /// <param name="parameterTypes">The parameter types.</param>
    /// <returns>The non-generic method info.</returns>
    MethodInfo MakeGenericMethod(MethodInfo methodType, params Type[] parameterTypes);

    /// <summary>Creates the instance.</summary>
    /// <param name="instanceType">Type of the instance.</param>
    /// <param name="constructorParameters">The constructor parameters.</param>
    /// <returns></returns>
    object CreateInstance(Type instanceType, params object[] constructorParameters);

    /// <summary>
    /// Creates the instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="constructorParameters">The constructor parameters.</param>
    /// <returns></returns>
    T CreateInstance<T>(params object[] constructorParameters);

    /// <summary>Invokes the generic method.</summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="genericTypes">The generic types.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    object InvokeGenericMethod(Type objectType, object objectValue, string methodName, Type[] genericTypes, params object[] parameterValues);

    /// <summary>Invokes the generic method asynchronous.</summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="genericTypes">The generic types.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns>the method result</returns>
    /// <exception cref="ArgumentNullException">objectType
    /// or
    /// methodName
    /// or
    /// genericTypes</exception>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    Task InvokeGenericMethodAsync(Type objectType, object objectValue, string methodName, Type[] genericTypes, params object[] parameterValues);

    /// <summary>Invokes the non generic method.</summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    object InvokeNonGenericMethod(Type objectType, object objectValue, string methodName, params object[] parameterValues);

    /// <summary>
    /// Invokes the non generic method.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="objectValue">The object value.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    Task InvokeNonGenericMethodAsync(Type objectType, object objectValue, string methodName, params object[] parameterValues);

    /// <summary>Invokes the non generic method.</summary>
    /// <param name="objectValue">The object value.</param>
    /// <param name="method">The method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    object Invoke(object objectValue, MethodInfo method, params object[] parameterValues);

    /// <summary>Invokes the non generic method.</summary>
    /// <param name="objectValue">The object value.</param>
    /// <param name="method">The method.</param>
    /// <param name="parameterValues">The parameter values.</param>
    /// <returns></returns>
    /// <exception cref="MethodNotFoundException">Method '{methodName}' not found</exception>
    Task InvokeAsync(object objectValue, MethodInfo method, params object[] parameterValues);





    /// <summary>Gets the properties with values.</summary>
    /// <param name="pocoObject">The poco object.</param>
    /// <returns></returns>
    List<(string Name, object Value)> GetPropertiesWithValues(object pocoObject);










    /// <summary>
    /// Gets the custom attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    TAttribute GetCustomAttribute<TAttribute>(Type type)
      where TAttribute : Attribute;

    /// <summary>
    /// Gets the custom attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="member">The type.</param>
    /// <returns></returns>
    TAttribute GetCustomAttribute<TAttribute>(MemberInfo member)
      where TAttribute : Attribute;

    /// <summary>
    /// Gets the custom attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="member">The member.</param>
    /// <returns></returns>
    TAttribute GetCustomAttribute<TAttribute>(ParameterInfo member)
      where TAttribute : Attribute;

    /// <summary>
    /// Gets the custom attributes.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    TAttribute[] GetCustomAttributes<TAttribute>(Type type)
      where TAttribute : Attribute;

    /// <summary>
    /// Gets the custom attributes.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="member">The member.</param>
    /// <returns></returns>
    TAttribute[] GetCustomAttributes<TAttribute>(MemberInfo member)
      where TAttribute : Attribute;

    /// <summary>
    /// Gets the generic type for hierarchy.
    /// </summary>
    /// <param name="genericBaseType">Type of the generic base.</param>
    /// <param name="implementation">The implementation.</param>
    /// <returns></returns>
    Type GetGenericTypeForHierarchy(Type genericBaseType, Type implementation);

    /// <summary>
    /// Gets the generic interface for hierarchy.
    /// </summary>
    /// <param name="genericInterfaceType">Type of the generic interface.</param>
    /// <param name="implementation">The implementation.</param>
    /// <returns></returns>
    Type GetGenericInterfaceForHierarchy(Type genericInterfaceType, Type implementation);

    /// <summary>
    /// Gets all public constant values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    List<T> GetAllPublicConstantValues<T>(Type type);

    /// <summary>
    /// Gets all public constants.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    List<FieldInfo> GetAllPublicConstants<T>(Type type);

    /// <summary>
    /// Determines whether [is generic task].
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if [is generic task] [the specified type]; otherwise, <c>false</c>.
    /// </returns>
    bool IsGenericTask(Type type);

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if the specified type is task; otherwise, <c>false</c>.
    /// </returns>
    bool IsTask(Type type);

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is task; otherwise, <c>false</c>.
    /// </returns>
    bool IsTask(object obj);

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if the specified type is task; otherwise, <c>false</c>.
    /// </returns>
    bool IsTask<T>(Type type);

    /// <summary>
    /// Determines whether this instance is task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is task; otherwise, <c>false</c>.
    /// </returns>
    bool IsTask<T>(object obj);

    /// <summary>
    /// Gets the type of the task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    Type GetTaskType<T>(Type type);

    /// <summary>
    /// Gets the type of the task.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    Type GetTaskType<T>(object obj);

    /// <summary>
    /// Gets the type of the task type generic parameter.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    Type GetTaskTypeGenericParameterType(Type type);

    /// <summary>
    /// Gets the type of the task type generic parameter.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    Type GetTaskTypeGenericParameterType(object obj);
  }
}
