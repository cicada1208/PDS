using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lib
{
    public class ReflectionUtil
    {
        /// <summary>
        /// 取得 Attribute Display(Name) 值
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="propertyName">Model PropertyName</param>
        ///<remarks>[Display(Name = "標題")]</remarks>
        public static string GetPropertyDisplayName<T>(string propertyName)
        {
            // Example: ReflectionUtil.GetPropertyDisplayName<MvvmViewModel>(nameof(MvvmViewModel.Title))
            string result = string.Empty;
            var type = typeof(T);
            var pi = type.GetProperty(propertyName);
            result = pi.GetPropertyDisplayName();
            return result;
        }

        /// <summary>
        /// 取得 Attribute Display(Name) 值
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="propertyExpression">Model PropertyExpression</param>
        ///<remarks>[Display(Name = "標題")]</remarks>
        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            // Example: ReflectionUtil.GetPropertyDisplayName<MvvmViewModel>(i => i.Title)
            var memberInfo = GetPropertyInfo(propertyExpression.Body);
            if (memberInfo == null)
                return string.Empty;

            var attr = memberInfo.GetAttribute<DisplayAttribute>();
            return (attr != null) ? attr.GetName() : memberInfo.Name;
        }

        public static MemberInfo GetPropertyInfo(Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            MemberExpression memberExpr = propertyExpression as MemberExpression;

            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
                return memberExpr.Member;

            return null;
        }

    }

    public static class ReflectionExUtil
    {
        /// <summary>
        /// 取得 Attribute Display(Name) 值
        /// </summary>
        ///<remarks>[Display(Name = "標題")]</remarks>
        public static string GetPropertyDisplayName(this PropertyInfo pi)
        {
            string result = string.Empty;
            if (pi != null)
                result = pi.IsDefined(typeof(DisplayAttribute)) ? pi.GetCustomAttribute<DisplayAttribute>().GetName() : pi.Name;
            return result;
        }

        /// <summary>
        /// 取得 Attribute Display(Name) 值
        /// </summary>
        /// <param name="entity">Model Entity</param>
        ///<remarks>[Display(Name = "標題")]</remarks>
        public static string GetPropertyDisplayName(this object entity, string propertyName) =>
             entity?.GetType().GetProperty(propertyName).GetPropertyDisplayName() ?? string.Empty;

        /// <summary>
        /// 取得 Attribute MaxLength 值
        /// </summary>
        ///<remarks>[MaxLength(1)]</remarks>
        public static int GetPropertyMaxLength(this PropertyInfo pi)
        {
            int result = 8000;
            if (pi != null)
                result = pi.IsDefined(typeof(MaxLengthAttribute)) ? pi.GetCustomAttribute<MaxLengthAttribute>().Length : result;
            return result;
        }

        /// <summary>
        /// 取得 Attribute MaxLength 值
        /// </summary>
        /// <param name="entity">Model Entity</param>
        ///<remarks>[MaxLength(1)]</remarks>
        public static int GetPropertyMaxLength(this object entity, string propertyName) =>
             entity?.GetType().GetProperty(propertyName).GetPropertyMaxLength() ?? 8000;

        /// <summary>
        /// 取得 Attribute
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired = false)
            where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }

            return (T)attribute;
        }

        /// <summary>
        /// 設定 Property Value
        /// </summary>
        public static void SetPropertyValue(this object entity, string propertyName, object newValue)
        {
            if (entity == null) return;
            PropertyInfo prop = entity.GetType().GetProperty(propertyName);
            if (prop == null) return;
            prop.SetValue(entity, newValue);
        }

        /// <summary>
        /// 取得 Property Value
        /// </summary>
        public static object GetPropertyValue(this object entity, string propertyName)
        {
            if (entity == null) return null;
            PropertyInfo prop = entity.GetType().GetProperty(propertyName);
            if (prop == null) return null;
            return prop.GetValue(entity);
        }

    }

}
