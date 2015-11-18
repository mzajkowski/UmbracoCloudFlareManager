using System;

namespace UmbracoCloudFlareManager.Utils
{
    internal class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the string value for an enum member.
        /// </summary>
        /// 9/13/2013 by Sergi
        public string Value { get; protected set; }

        /// <summary>
        /// Inits a new instance of class <see cref="StringValueAttribute"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// 9/13/2013 by Sergi
        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }
}