using System;
using System.Reflection;
using UmbracoCloudFlareManager.Utils;

namespace UmbracoCloudFlareManager.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// 
        /// Thanks to http://weblogs.asp.net/stefansedich/archive/2008/03/12/enum-with-string-values-in-c.aspx
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            var type = value.GetType();

            // Get fieldinfo for this type
            var fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            var attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs != null && attribs.Length > 0 ? attribs[0].Value : null;
        }

        /// <summary>
        /// Gets the T enum value from the given String.
        /// </summary>
        /// <typeparam name="T">Enum Type to convert</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="source">The source.</param>
        /// <param name="ignoreCase">sets the "ignore case" flag.</param>
        /// <returns></returns>
        /// 9/13/2013 by Sergi
        public static T GetValue<T>(String source, Boolean ignoreCase = true)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), source, ignoreCase);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

    }
}