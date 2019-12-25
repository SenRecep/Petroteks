using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.ExtensionMethods
{
    public static class SessionExtensionMethods
    {
        public static void SetObj(this ISession session,string key,object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key,objectString);
        }
        public static T GetObj<T>(this ISession session, string key) where T : class
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrWhiteSpace(objectString))
                return null;
            else
            {
                return JsonConvert.DeserializeObject<T>(objectString);
            }
        }
    }
}
