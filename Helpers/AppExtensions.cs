using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace DaysGoneModManager.Helpers
{
    public static class AppExtensions
    {
        #region IEnumerable Extensions

            public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
            {
                return new ObservableCollection<T>(enumerable);
            }

            public static void AddRange<T>(this ObservableCollection<T> collection, params T[] items)
            {
                foreach (var item in items)
                {
                    if(!collection.Contains(item))
                        collection.Add(item);
                }
            }

            public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
            {
                foreach (var item in items)
                {
                    if(!collection.Contains(item))
                        collection.Add(item);
                }
            }

            public static int DeepCount(this IEnumerable<IEnumerable<object>> collection)
            {
                var count = 0;
                foreach (var IenumCol in collection)
                {
                    count += IenumCol.Count();
                }
                return count;
            }

        #endregion

        #region String Extensions

            public static T LoadFromRes<T>(this string value) => (T)Application.Current.Resources[value];

            public static bool In<T>(this object value, params T[] comparisonArray) => comparisonArray.Contains((T)value);

            public static bool NullOrEmpty(this string value) => string.IsNullOrEmpty(value);

            public static string Format(this string target, params object[] values) => string.Format(target, values);

        #endregion

        #region LinqExtensions

        public static IEnumerable<TSource> LocalDistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        #endregion

        #region XmlExtensions

        public static string FormatXml(this string xml, bool indent = true, bool newLineOnAttributes = false, string indentChars = "  ", ConformanceLevel conformanceLevel = ConformanceLevel.Document) =>
            xml.FormatXml( new() { Indent = indent, NewLineOnAttributes = newLineOnAttributes, IndentChars = indentChars, ConformanceLevel = conformanceLevel });

        public static string FormatXml(this string xml, XmlWriterSettings settings)
        {
            using (StringReader textReader = new(xml))
            using (XmlReader xmlReader = XmlReader.Create(textReader, new() { ConformanceLevel = settings.ConformanceLevel } ))
            using (StringWriter textWriter = new())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                    xmlWriter.WriteNode(xmlReader, true);
                return textWriter.ToString();
            }
        }

        #endregion

        #region Reflection Extensions

            public static List<Type> GetTypesAssignableFrom<T1, T2>(this Assembly assembly)
            {
                return assembly.GetTypesAssignableFrom(typeof(T1), typeof(T2));
            }

            public static List<Type> GetTypesAssignableFrom(this Assembly assembly, Type compareType, Type excludeType)
            {
                List<Type> ret = new List<Type>();
                foreach (var type in assembly.DefinedTypes)
                {
                    if (compareType.IsAssignableFrom(type) && compareType != type && excludeType != type)
                    {
                        ret.Add(type);
                    }
                }
                return ret;
            }


        #endregion

        #region Xaml Extensions

        public static UIElement FindUid(this DependencyObject parent, string uid)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                var el = VisualTreeHelper.GetChild(parent, i) as UIElement;
                if (el == null) continue;

                if (el.Uid == uid) return el;

                el = el.FindUid(uid);
                if (el != null) return el;
            }

            if (parent is ContentControl)
            {
                UIElement content = (parent as ContentControl).Content as UIElement;
                if (content != null)
                {
                    if (content.Uid == uid) return content;

                    var el = content.FindUid(uid);
                    if (el != null) return el;
                }
            }
            return null;
        }

        #endregion

        #region Enum Extensions
            public static string ToSpacedString(this Enum enu)
            {
                return Regex.Replace(enu.ToString(), "([A-Z])", " $1").Trim();
            }

            public static T EnumFromString<T>(string value) where T : struct
            {
                string noSpace = value.Replace(" ", "");
                if (Enum.GetNames(typeof(T)).Any(x => x.ToString().Equals(noSpace)))
                {
                    return (T)Enum.Parse(typeof(T), noSpace);
                }
                return default(T);
            }
        #endregion




        /// <summary>
        /// Convert Media Color (WPF) to Drawing Color (WinForm)
        /// </summary>
        /// <param name="mediaColor"></param>
        /// <returns></returns>
        public static System.Drawing.Color ToDrawingColor(this System.Windows.Media.Color mediaColor)
        {
            return System.Drawing.Color.FromArgb(mediaColor.A, mediaColor.R, mediaColor.G, mediaColor.B);
        }

        /// <summary>
        /// Convert Drawing Color (WPF) to Media Color (WinForm)
        /// </summary>
        /// <param name="drawingColor"></param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color drawingColor)
        {
            return System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }

    }
}