using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Yuya.Net.CoreServices
{
  /// <summary>
  /// String Extensions
  /// </summary>
  public static class StringExtensions
  {
    /// <summary>
    /// The culture of English(USA)
    /// </summary>
    public static readonly CultureInfo CultureEn = new CultureInfo("en-US");

    /// <summary>
    /// returns null or trimmed string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>Null or Trimmed string</returns>
    public static string NullOrTrim(this string str)
    {
      return string.IsNullOrWhiteSpace(str) ? null : str.Trim();
    }

    /// <summary>
    /// Converts string to kebab-case.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="cultureInfo">The culture information.</param>
    /// <returns>The kebab case string</returns>
    public static string ToKebabCase(this string source, CultureInfo cultureInfo = null)
    {
      if (source is null) return null;

      if (source.Length == 0) return string.Empty;

      if (cultureInfo == null) cultureInfo = Thread.CurrentThread.CurrentCulture;

      if (source.Length == 1) return source.ToLower(cultureInfo);

      var newList = new List<char>(source.Length + source.Length / 2) { char.ToLower(source[0], cultureInfo) };

      for (var i = 1; i < source.Length; i++)
      {
        var s = source[i];
        var s2 = char.ToLower(s, cultureInfo);
        if (char.IsLower(s) || char.IsPunctuation(s)) // if current char is already lowercase
        {
          newList.Add(s);
        }
        else if (char.IsPunctuation(source[i - 1])) // if current char is upper and previous char is lower
        {
          newList.Add(s2);
        }
        else if (char.IsLower(source[i - 1])) // if current char is upper and previous char is lower
        {
          Add(s2, newList);
        }
        else if (char.IsUpper(source[i - 1]) && char.IsUpper(source[i]))
        {
          newList.Add(s2);
        }
        else if (i + 1 == source.Length || char.IsUpper(source[i + 1])) // if current char is upper and next char doesn't exist or is upper
        {
          newList.Add(s2);
        }
        else
        {
          Add(s2, newList);
        }
      }
      return new string(newList.ToArray());
    }

    private static void Add(char source, List<char> newList)
    {
      newList.Add('-');
      newList.Add(source);
    }

    /// <summary>
    /// Change from Pascal or Camel Case to Upper case with underscore(_) character.
    /// </summary>
    /// <param name="source">The input string.</param>
    /// <param name="seperator">The seperator. default value is underscore(_) character</param>
    /// <param name="culture">The culture.</param>
    /// <returns>the result of Upper case with underscore(_) character</returns>
    public static string UCase(this string source, string seperator = "_", CultureInfo culture = null)
    {
      return GetSeperatedString(source, char.ToUpper, seperator, culture);
    }

    /// <summary>
    /// Change from Pascal or Camel Case to Lower case with underscore(_) character.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="seperator">The seperator. default value is underscore(_) character</param>
    /// <param name="culture">The culture.</param>
    /// <returns>the result of Lower case with underscore(_) character</returns>
    public static string LCase(this string source, string seperator = "_", CultureInfo culture = null)
    {
      return GetSeperatedString(source, char.ToLower, seperator, culture);
    }

    /// <summary>Gets the seperated string.</summary>
    /// <param name="source">The source.</param>
    /// <param name="action">The action.</param>
    /// <param name="seperator">The seperator.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    private static string GetSeperatedString(string source, Func<char, CultureInfo, char> action, string seperator = "_", CultureInfo culture = null)
    {
      if (source is null) return null;

      if (source.Length == 0) return string.Empty;

      if (culture == null) culture = Thread.CurrentThread.CurrentCulture;

      var stringBuilder = new StringBuilder(action(source[0], culture).ToString(), source.Length * 2);
      for (var i = 1; i < source.Length; i++)
      {
        var s = source[i];
        var s2 = action(s, culture);
        WorkForChar(source, seperator, stringBuilder, i, s, s2);
      }
      return stringBuilder.ToString();
    }

    private static void WorkForChar(string source, string seperator, StringBuilder stringBuilder, int i, char s, char s2)
    {
      if (char.IsLower(s) || char.IsPunctuation(s)) // if current char is already lowercase
      {
        stringBuilder.Append(s2);
        return;
      }
      if (char.IsPunctuation(source[i - 1])) // if current char is upper and previous char is punctuation(noktalama)
      {
        stringBuilder.Append(s2);
        return;
      }
      if (char.IsLower(source[i - 1])) // if current char is upper and previous char is lower
      {
        stringBuilder.Append(seperator);
        stringBuilder.Append(s2);
        return;
      }
      if (i + 1 < source.Length && char.IsUpper(source[i - 1]) && char.IsUpper(source[i]) && (char.IsUpper(source[i + 1]) || char.IsPunctuation(source[i + 1]))) // if current char is upper and next char doesn't exist or is upper
      {
        stringBuilder.Append(s2);
        return;
      }
      if (i + 1 < source.Length && char.IsUpper(source[i - 1]) && char.IsUpper(source[i]) && char.IsLower(source[i + 1])) // if current char is upper and next char doesn't exist or is upper
      {
        stringBuilder.Append(seperator);
        stringBuilder.Append(s2);
        return;
      }
      if (i + 1 == source.Length || char.IsUpper(source[i + 1])) // if current char is upper and next char doesn't exist or is upper
      {
        stringBuilder.Append(s2);
        return;
      }
      stringBuilder.Append(seperator);
      stringBuilder.Append(s2);
    }
  }
}