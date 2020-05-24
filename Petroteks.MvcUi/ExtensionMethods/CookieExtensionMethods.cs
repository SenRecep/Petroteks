using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Petroteks.MvcUi.ExtensionMethods
{
    public static class CookieExtensionMethods
    {
        public static T Get<T>(this IRequestCookieCollection cookie, string key) where T : class
        {
            string objectString = cookie[key];
            if (string.IsNullOrWhiteSpace(objectString))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(objectString);
            }
        }

        public static void Set(this IResponseCookies cookie, string key, object value, int? expireTime)
        {
            string objectString = JsonConvert.SerializeObject(value);
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
            {
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }
            else
            {
                option.Expires = DateTime.Now.AddMilliseconds(10);
            }

            cookie.Append(key, objectString, option);
        }

        public static void Remove(this IResponseCookies cookie, string key)
        {
            cookie.Delete(key);
        }
    }
}
