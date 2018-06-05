using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingChallenges.DataAcess.Extensions
{
    public static class TypeExtensions
    {
        private static Type[] _complexType = new Type[]
        {
            typeof (String),
            typeof (string),
            typeof (Decimal),
            typeof (decimal),
            typeof (DateTime),
            typeof (DateTimeOffset),
            typeof (TimeSpan),
            typeof (Guid)
        };

        public static bool IsPrimitiveOrComplex(this Type type)
        {
            return
             type.IsValueType ||
             type.IsPrimitive ||
             _complexType.Contains(type) ||
             Convert.GetTypeCode(type) != TypeCode.Object;
        }


        public static string ExtractError(this Exception ex)
        {
            if (ex == null) return string.Empty;

            var temp = ex.Message;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                temp += ex.Message + " ";
            }
            return temp;
        }

        /// <summary>
        /// Determines if a given type is of the expected struct type or a nullable of the expected struct type
        /// </summary>
        public static bool IsStructOrNullable(this Type type, Type expectedStructType)
        {
            return type == expectedStructType || type == typeof(Nullable<>).MakeGenericType(new[] { expectedStructType });
        }

        /// <summary>
        /// Determines if a given type is a T or a nullable of T
        /// </summary>
        public static bool IsStructOrNullable<T>(this Type type) where T : struct
        {
            return type.IsStructOrNullable(typeof(T));
        }

       

        /// <summary>
        /// Determines if a given type is a T, a nullable of T or a value type of T
        /// </summary>
        //public static bool IsStructOrValueType<T>(this Type type) where T : struct
        //{
        //    return type.IsStructOrValueType(typeof(T));
        //}

        /// <summary>
        /// Determines if a given type is a DateTime, including a nullable of DateTime or a value type of DateTime
        /// </summary>
        //public static bool IsDate(this Type type)
        ////{
        ////    return type.IsStructOrValueType<DateTime>();
        ////}

        /// <summary>
        /// Determines if a given type is a Guid, including a nullable of Guid or a value type of Guid
        /// </summary>
        //public static bool IsGuid(this Type type)
        //{
        //    return type.IsStructOrValueType<Guid>();
        //}

        /// <summary>
        /// Determines if a given type is an integer of any size, a nullable integer of any size or a value type of integer of any size
        /// </summary>
        //public static bool IsInteger(this Type type)
        //{
        //    return
        //        type.IsStructOrValueType<byte>() ||
        //        type.IsStructOrValueType<short>() ||
        //        type.IsStructOrValueType<int>() ||
        //        type.IsStructOrValueType<long>();
        //}

        /// <summary>
        /// Determines if a given type is numeric, a nullable numeric or a numeric value type
        /// </summary>
        //public static bool IsNumeric(this Type type)
        //{
        //    return
        //        type.IsInteger() ||
        //        type.IsStructOrValueType<float>() ||
        //        type.IsStructOrValueType<double>() ||
        //        type.IsStructOrValueType<decimal>();
        //}

        public static bool ClosesGenericType(this Type closedType, Type genericType)
        {
            return closedType != null && genericType != null &&
                genericType.IsGenericTypeDefinition &&
                closedType.IsGenericType &&
                closedType.GetGenericTypeDefinition() == genericType;
        }

        /// <summary>
        /// Checks that a type implements the specified interface type
        /// </summary>
        public static bool Implements(this Type type, Type interfaceType)
        {
            return type != null &&
                interfaceType != null &&
                interfaceType.IsInterface &&
                type.IsAbstract == false && (
                    type == interfaceType ||
                    type.ClosesGenericType(interfaceType) ||
                    type.GetInterfaces().Any(i =>
                        i == interfaceType ||
                        i.ClosesGenericType(interfaceType))
                );
        }

        /// <summary>
        /// Checks that a type inherits/implements the given base/interface type
        /// </summary>
        public static bool IsA(this Type type, Type baseOrInterfaceType)
        {
            return type.Implements(baseOrInterfaceType) || (
                    type != null && baseOrInterfaceType != null && !type.IsAbstract &&
                    type.IsSubclassOf(baseOrInterfaceType)
                ) || (
                    type != null && type.BaseType != null &&
                    type.BaseType.IsA(baseOrInterfaceType)
                );
        }

        /// <summary>
        /// Checks that a type inherits/implements the given base/interface T
        /// </summary>
        public static bool IsA<T>(this Type type) where T : class
        {
            return type.IsA(typeof(T));
        }

        /// <summary>
        /// Checks that a type implements the specified interface T
        /// </summary>
        public static bool Implements<T>(this Type type) where T : class
        {
            return type.Implements(typeof(T));
        }

      

        public static bool ImplementsWithGenericParameter(this Type type, Type genericInterfaceType, Type parameterType)
        {
            return type.Implements(genericInterfaceType) &&
                type.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == genericInterfaceType &&
                        i.GetGenericArguments().Any(t => t == parameterType));
        }

        public static Type GetPrimaryInterface(this Type type)
        {
            return type.GetInterfaces()
                .First();
        }

        public static Type GetAbsoluteInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                .First(t =>
                    t.IsGenericType ?
                        interfaceType == t.GetGenericTypeDefinition() :
                        interfaceType == t);
        }

        public static IEnumerable<Type> GetTopLevelInterfaces(this Type type)
        {
            var interfaces = type.GetInterfaces();
            var topLevel = new List<Type>(interfaces);

            foreach (Type @interface in interfaces)
                foreach (Type parent in @interface.GetInterfaces())
                    topLevel.Remove(parent);

            return topLevel.AsEnumerable();
        }

        public static bool IsValueType(this Type type)
        {
            return type.IsValueType || (
                type.IsNullable() &&
                type.GetGenericArguments()
                    .Any(a => a.IsValueType));
        }
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsEnum(this Type type)
        {
            return type.IsEnum || (
                type.IsNullable() &&
                type.GetGenericArguments()
                    .Any(a => a.IsEnum));
        }

        public static bool FromAssemblyOfType<T>(this Type type)
        {
            return type.Assembly == typeof(T).Assembly;
        }

        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>().FirstOrDefault();
        }

       

        /// <summary>
        /// Finds the argument at the specified index that is used to close the generic type implemented.
        /// For example IFoo&lt;string&gt; would return the type for string.
        /// </summary>
        /// <param name="type">The type to scan</param>
        /// <param name="genericType">The generic type to scan for</param>
        /// <param name="argumentIndex">The index of the generic argument used to close the generic type</param>
        /// <returns></returns>
       

       

 
    }
}
