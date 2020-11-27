﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Expro.Utils
{  
    //public static class ViewModelToModel
    //{
    //    public static void Copy<TDest, TSource>(TDest destination, TSource source)
    //        where TSource : class
    //        where TDest : class
    //    {
    //        var destProperties = destination.GetType().GetProperties()
    //            .Where(x => !x.CustomAttributes.Any(y => y.AttributeType.Name == Name) && x.CanRead && x.CanWrite && !x.GetGetMethod().IsVirtual);
    //        var sourceProperties = source.GetType().GetProperties()
    //            .Where(x => !x.CustomAttributes.Any(y => y.AttributeType.Name == PropertyCopyIgnoreAttribute.Name) && x.CanRead && x.CanWrite && !x.GetGetMethod().IsVirtual);
    //        var copyProperties = sourceProperties.Join(destProperties, x => x.Name, y => y.Name, (x, y) => x);
    //        foreach (var sourceProperty in copyProperties)
    //        {
    //            var prop = destProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
    //            prop.SetValue(destination, sourceProperty.GetValue(source));
    //        }
    //    }
    //}

    public static class PropertyCopier
    {
        public static T CopyTo<T>(object src, T dest)
        {
            if (src == null || dest == null) return dest;

            var srcProperties = GetCanReadPublicProperties(src);
            var destProperties = GetCanWritePublicProperties(dest);
            var properties = srcProperties.Join(destProperties, p => p.Name, p => p.Name, (p1, p2) => new { p1, p2 });
            foreach (var property in properties)
            {
                var srcValue = property.p1.GetValue(src);
                if (srcValue == null)
                {
                    property.p2.SetValue(dest, null);
                    continue;
                }
                if (property.p1.PropertyType == property.p2.PropertyType)
                {
                    if (srcValue is ICloneable c)
                    {
                        property.p2.SetValue(dest, c.Clone());
                        continue;
                    }
                    property.p2.SetValue(dest, srcValue);
                    continue;
                }
                CopyUnmatchValue(property.p2, dest, srcValue);
            }
            return dest;
        }

        private static void CopyUnmatchValue(PropertyInfo property, object dest, object srcValue)
        {
            try { property.SetValue(dest, ConvertUnmatchTypeValue(property.PropertyType, srcValue)); } catch { property.SetValue(dest, null); }
        }

        private static object ConvertUnmatchTypeValue(Type destType, object srcValue)
        {
            if (srcValue == null) return null;

            if (destType == typeof(string)) return Convert.ToString(srcValue);
            if (destType == typeof(bool) || destType == typeof(bool?)) return Convert.ToBoolean(srcValue);
            if (destType == typeof(short) || destType == typeof(short?)) return Convert.ToInt16(srcValue);
            if (destType == typeof(int) || destType == typeof(int?)) return Convert.ToInt32(srcValue);
            if (destType == typeof(long) || destType == typeof(long?)) return Convert.ToInt64(srcValue);
            if (destType == typeof(double) || destType == typeof(double?)) return Convert.ToDouble(srcValue);
            if (destType == typeof(decimal) || destType == typeof(decimal?)) return Convert.ToDecimal(srcValue);
            if (destType == typeof(DateTime) || destType == typeof(DateTime?)) return Convert.ToDateTime(srcValue);
            if (destType == typeof(byte) || destType == typeof(byte?)) return Convert.ToByte(srcValue);
            if (destType == typeof(char) || destType == typeof(char?)) return Convert.ToChar(srcValue);
            if (destType == typeof(sbyte) || destType == typeof(sbyte?)) return Convert.ToSByte(srcValue);
            if (destType == typeof(byte) || destType == typeof(byte?)) return Convert.ToSingle(srcValue);

            if (srcValue.GetType().IsSubclassOf(destType))
            {
                return srcValue is ICloneable c ? c.Clone() : srcValue;
            }

            if (srcValue.GetType().GetInterfaces().Any(m => m == destType))
            {
                return srcValue is ICloneable c ? c.Clone() : srcValue;
            }

            if (destType.IsArray)
            {
                var src = (srcValue as System.Collections.IEnumerable)?.OfType<object>().ToArray();
                if (src != null)
                {
                    var dest = Array.CreateInstance(destType.GetElementType(), src.Length);
                    foreach (var i in Enumerable.Range(0, src.Length))
                        dest.SetValue(ConvertUnmatchTypeValue(destType, src[i]), i);
                    return dest;
                }
            }
            if (destType.IsClass)
            {
                var destGenericArgs = destType.GetGenericArguments();
                if (destGenericArgs.Any())
                {
                    var collectionType = typeof(ICollection<>);
                    var gt = collectionType.MakeGenericType(destGenericArgs[0]);
                    if (destType.GetInterfaces().Any(m => m == gt))
                    {
                        var src = (srcValue as System.Collections.IEnumerable);
                        if (src == null) src = new object[] { srcValue };
                        {
                            var dest = Activator.CreateInstance(destType);
                            var add = dest.GetType().GetMethod("Add");
                            foreach (var srcMember in src)
                            {
                                add.Invoke(dest, new[] { ConvertUnmatchTypeValue(destGenericArgs[0], srcMember) });
                            }
                            return dest;
                        }
                    }
                }
            }
            if (destType.IsInterface)
            {
                var destGenericArgs = destType.GetGenericArguments();
                if (destGenericArgs.Any())
                {
                    var listType = typeof(List<>);
                    var listGenericType = listType.MakeGenericType(destGenericArgs[0]);
                    if (listGenericType.GetInterfaces().Any(m => m == destType))
                    {
                        var src = (srcValue as System.Collections.IEnumerable);
                        if (src == null) src = new object[] { srcValue };
                        {
                            var dest = Activator.CreateInstance(listGenericType);
                            var add = dest.GetType().GetMethod("Add");
                            foreach (var srcMember in src)
                            {
                                add.Invoke(dest, new[] { ConvertUnmatchTypeValue(destGenericArgs[0], srcMember) });
                            }
                            return dest;
                        }
                    }
                }
            }
            { return srcValue is ICloneable c ? c.Clone() : srcValue; }
        }

        private static IEnumerable<PropertyInfo> GetPublicProperties(object obj)
        {
            if (obj == null) return Enumerable.Empty<PropertyInfo>();
            return obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        private static IEnumerable<PropertyInfo> GetCanReadPublicProperties(object obj)
        {
            return GetPublicProperties(obj).Where(p => p.CanRead);
        }

        private static IEnumerable<PropertyInfo> GetCanWritePublicProperties(object obj)
        {
            return GetPublicProperties(obj).Where(p => p.CanWrite);
        }
    }
}
