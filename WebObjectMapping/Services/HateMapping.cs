using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WebObjectMapping.Attributes;
using System.Web.Http.Routing;
using System.Diagnostics;
using System.Net.Http;

namespace WebObjectMapping.Services
{
    public static class HateMapping
    {

        /// <summary>
        /// discovers and maps all the hateoas properties on the web model objects
        /// </summary>
        /// <typeparam name="T">Web model type</typeparam>
        /// <param name="obj">this is the object of the webmodel</param>
        /// <returns>web object model again but with all the hateoas properties filled</returns>
        public static T Map<T>(T obj, HttpRequestMessage request)
        {
            T result = obj;
            if (obj != null && request != null)
            {
                //we are good to go. .. 
                UrlHelper urlHelper = new UrlHelper(request);
                IEnumerable<PropertyInfo> urlProps = HateMapping.DiscoverUrlProps(typeof(T));
                Array.ForEach<PropertyInfo>(urlProps.ToArray<PropertyInfo>(), p =>
                {
                    //assign the values 
                    Dictionary<string, object> routeDictionary = new Dictionary<string, object>();
                    routeDictionary[HateMapping.DiscoverTokenPropName(typeof(T))] = HateMapping.TokenPropValue(typeof(T), result);
                    try
                    {
                        string routeName = p.GetCustomAttribute<HateoasRouteAttribute>().RouteName;
                        try
                        {
                            string url = urlHelper.Link(routeName, routeDictionary);
                            try
                            {
                                ParameterExpression param = Expression.Parameter(typeof(string));
                                Expression.Lambda<Action<string>>(
                                                 body: Expression.Call(
                                                     Expression.Constant(result),
                                                     p.GetSetMethod(),
                                                     new Expression[] { param }),
                                                 parameters: new ParameterExpression[] { param }).Compile()(url);
                            }
                            catch (Exception)
                            {
                                Trace.WriteLine(String.Format("Could not write values into the url property , failed to excute expression for :{0} ", p.Name));
                                throw;
                            }
                        }
                        catch (Exception)
                        {
                            Trace.WriteLine(String.Format("Missing route : {0}", routeName));
                            throw;
                        }

                    }
                    catch (Exception)
                    {
                        //did not get the route name ?
                        Trace.WriteLine(String.Format("Failed to get the route name , check if {0} has HateoasRouteAttribute on property: {1}", typeof(T).ToString(), p.Name));
                    }

                });
                return result;
            }
            return result;
        }

        private static string DiscoverTokenPropName(Type type)
        {
            string result = "";

            PropertyInfo p = type.GetProperties().Where(x =>
                x.GetCustomAttribute<HatesoasRouteToken>() != null)
                .FirstOrDefault();
            result = p != null ? p.Name.ToLower() : ""; //tokens are all in the lower case
            return result;
        }

        private static object TokenPropValue(Type type, object onObj)
        {
            object result = null;
            PropertyInfo p = type.GetProperties().Where(x =>
                x.GetCustomAttribute<HatesoasRouteToken>() != null)
                .FirstOrDefault();
            if (p != null)
            {
                result =
                Expression.Lambda(delegateType: Mapping.GetDelegateOf(p.PropertyType, false),
                    body: Expression.Call(instance: Expression.Constant(onObj), method: p.GetGetMethod()),
                    parameters: new ParameterExpression[] { }).Compile().DynamicInvoke();
            }
            return result;//anyway the token value can be string
        }

        private static IEnumerable<PropertyInfo> DiscoverUrlProps(Type type)
        {
            List<PropertyInfo> result = new List<PropertyInfo>();
            result = type.GetProperties().Where(p =>
                p.GetCustomAttribute<HateoasRouteAttribute>() != null)
                .ToList<PropertyInfo>();
            return result;
        }

    }
}
