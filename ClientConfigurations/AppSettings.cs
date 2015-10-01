using System;
using System.Globalization;

namespace ClientConfigurations
{
    public static class AppSettings
    {
        /// <summary>
        /// Method try to get <seealso cref="System.Configuration.ConfigurationManager.AppSetting"/> configuration.
        /// If you settings doesn't exist throw Exception
        /// 
        /// Get Idea from: http://stackoverflow.com/questions/19596233/mvc-3-getting-values-from-appsettings-in-web-config
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Setting<T>(string name)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(string.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}
