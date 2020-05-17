#pragma warning disable S1144 // Unused private types or members should be removed
#pragma warning disable IDE0051 // Remove unused private members

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// Convert &amp; Compare Service
  /// </summary>
  /// <seealso cref="TurkuazGO.IConvertCompareService" />
  public class ConvertCompareService : IConvertCompareService
  {
    private static readonly Dictionary<Type, MethodInfo> LT_METHODS = new Dictionary<Type, MethodInfo>();
    private static readonly Dictionary<Type, MethodInfo> LTE_METHODS = new Dictionary<Type, MethodInfo>();
    private static readonly Dictionary<Type, MethodInfo> GT_METHODS = new Dictionary<Type, MethodInfo>();
    private static readonly Dictionary<Type, MethodInfo> GTE_METHODS = new Dictionary<Type, MethodInfo>();
    private static readonly Dictionary<Type, MethodInfo> TO_METHODS = new Dictionary<Type, MethodInfo>();

    /// <summary>
    /// Initializes the <see cref="ConvertCompareService"/> class.
    /// </summary>
    static ConvertCompareService()
    {
      var methods = typeof(ConvertCompareService).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).ToList();

      SetMethods(methods.Where(x => x.Name.StartsWith("LTE")).ToList(), LTE_METHODS, methods);
      SetMethods(methods.Where(x => x.Name.StartsWith("LT")).ToList(), LT_METHODS, methods);
      SetMethods(methods.Where(x => x.Name.StartsWith("GTE")).ToList(), GTE_METHODS, methods);
      SetMethods(methods.Where(x => x.Name.StartsWith("GT")).ToList(), GT_METHODS, methods);

      foreach (var item in typeof(Converters).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(x => x.Name.StartsWith("Get")))
      {
        var rt = item.ReturnType;
        Type t;
        if (rt.IsGenericType)
          t = item.ReturnType.GetGenericArguments()[0];
        else
          t = rt;
        TO_METHODS.Add(t, item);
      }
    }

    private static void SetMethods(List<MethodInfo> from, Dictionary<Type, MethodInfo> to, List<MethodInfo> fullList = null)
    {
      foreach (var item in from)
      {
        to.Add(item.GetParameters()[0].ParameterType, item);
        if (fullList != null) fullList.Remove(item);
      }
    }

    private bool CompareValue<T>(Dictionary<Type, MethodInfo> list, T first, T second)
    {
      var t = typeof(T);
      if (list.TryGetValue(t, out MethodInfo m))
      {
        return (bool)(m.Invoke(null, new object[] { first, second }));
      }
      return false;
    }

    /// <summary>
    /// Lts the specified first.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    public bool LessThan<T>(T first, T second)
    {
      return CompareValue<T>(LT_METHODS, first, second);
    }

    /// <summary>
    /// Ltes the specified first.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    public bool LessThanEquals<T>(T first, T second)
    {
      return CompareValue<T>(LTE_METHODS, first, second);
    }

    /// <summary>
    /// Gts the specified first.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    public bool GreaterThan<T>(T first, T second)
    {
      return CompareValue<T>(GT_METHODS, first, second);
    }

    /// <summary>
    /// Gtes the specified first.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    public bool GreaterThanEquals<T>(T first, T second)
    {
      return CompareValue<T>(GTE_METHODS, first, second);
    }

    /// <summary>
    /// Equalses the specified first.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    public bool Equals<T>(T first, T second)
    {
      return first.Equals(second);
    }

    /// <summary>
    /// To the specified source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">
    /// Generic parameter is unsupported: " + ot.Name
    /// or
    /// Generic parameter is unsupported: " + ot.Name
    /// </exception>
    public T To<T>(object source)
    {
      var ot = typeof(T);
      if ((!ot.IsGenericType || ot.GetGenericTypeDefinition() != typeof(Nullable<>)) && ot != typeof(string))
      {
        throw new ArgumentException("Generic parameter is unsupported: " + ot.Name);
      }

      if (TO_METHODS.TryGetValue(ot, out MethodInfo m))
      {
        return (T)(m.Invoke(null, new object[] { source }));
      }
      throw new ArgumentException("Generic parameter is unsupported: " + ot.Name);
    }

    #region LTE

    private static bool LTEByte(byte first, byte second) => first <= second;

    private static bool LTEInt16(short first, short second) => first <= second;

    private static bool LTEInt32(int first, int second) => first <= second;

    private static bool LTEInt64(long first, long second) => first <= second;

    private static bool LTESingle(float first, float second) => first <= second;

    private static bool LTEDouble(double first, double second) => first <= second;

    private static bool LTEDecimal(decimal first, decimal second) => first <= second;

    private static bool LTEDateTime(DateTime first, DateTime second) => first <= second;

    private static bool LTEDateTimeOffset(DateTimeOffset first, DateTimeOffset second) => first <= second;

    private static bool LTETimeSpan(TimeSpan first, TimeSpan second) => first <= second;

    private static bool LTEString(string first, string second) => String.Compare(first, second) <= 0;

    #endregion LTE

    #region LT

    private static bool LTByte(byte first, byte second) => first < second;

    private static bool LTInt16(short first, short second) => first < second;

    private static bool LTInt32(int first, int second) => first < second;

    private static bool LTInt64(long first, long second) => first < second;

    private static bool LTSingle(float first, float second) => first < second;

    private static bool LTDouble(double first, double second) => first < second;

    private static bool LTDecimal(decimal first, decimal second) => first < second;

    private static bool LTDateTime(DateTime first, DateTime second) => first < second;

    private static bool LTDateTimeOffset(DateTimeOffset first, DateTimeOffset second) => first < second;

    private static bool LTTimeSpan(TimeSpan first, TimeSpan second) => first < second;

    private static bool LTString(string first, string second) => String.Compare(first, second) == -1;

    #endregion LT

    #region GTE

    private static bool GTEByte(byte first, byte second) => first <= second;

    private static bool GTEInt16(short first, short second) => first <= second;

    private static bool GTEInt32(int first, int second) => first <= second;

    private static bool GTEInt64(long first, long second) => first <= second;

    private static bool GTESingle(float first, float second) => first <= second;

    private static bool GTEDouble(double first, double second) => first <= second;

    private static bool GTEDecimal(decimal first, decimal second) => first <= second;

    private static bool GTEDateTime(DateTime first, DateTime second) => first <= second;

    private static bool GTEDateTimeOffset(DateTimeOffset first, DateTimeOffset second) => first <= second;

    private static bool GTETimeSpan(TimeSpan first, TimeSpan second) => first <= second;

    private static bool GTEString(string first, string second) => String.Compare(first, second) <= 0;

    #endregion GTE

    #region GT

    private static bool GTByte(byte first, byte second) => first < second;

    private static bool GTInt16(short first, short second) => first < second;

    private static bool GTInt32(int first, int second) => first < second;

    private static bool GTInt64(long first, long second) => first < second;

    private static bool GTSingle(float first, float second) => first < second;

    private static bool GTDouble(double first, double second) => first < second;

    private static bool GTDecimal(decimal first, decimal second) => first < second;

    private static bool GTDateTime(DateTime first, DateTime second) => first < second;

    private static bool GTDateTimeOffset(DateTimeOffset first, DateTimeOffset second) => first < second;

    private static bool GTTimeSpan(TimeSpan first, TimeSpan second) => first < second;

    private static bool GTString(string first, string second) => String.Compare(first, second) == -1;

    #endregion GT
  }
}

#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore S1144 // Unused private types or members should be removed