using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WebObjectMapping.Attributes;
using System.Diagnostics;

namespace WebObjectMapping.Services
{
    public static class Mapping
    {
        public static W ToWeb<P, W>(P pocoObject)
        {
            if (pocoObject == null) { return default(W); }
            
            //this is still the web object since the mapping is on the web objects
            PropertyInfo[] allmappedProps = Mapping.GetAllMappedProps(typeof(W));
            //result object instance creation
            W webObject = Activator.CreateInstance<W>();
            
            Array.ForEach(allmappedProps, p =>
            {
                //getting the target propperty -property on the web object
                PropertyInfo sourceProp = typeof(P).GetProperty(p.GetCustomAttribute<MapsToAttribute>().PropertyName);
                
                //gets the value from the property 
                var sourceVal =
                    Expression.Lambda(delegateType: Mapping.GetDelegateOf(sourceProp.PropertyType, false),
                        body: Expression.Call(
                            instance: Expression.Constant(pocoObject),
                            method: sourceProp.GetGetMethod()),
                        parameters: new ParameterExpression[] { }).Compile().DynamicInvoke();

                ParameterExpression param = Expression.Parameter(sourceProp.PropertyType);

                //setting the value of the property in the target property expression
                Expression.Lambda(
                   delegateType: Mapping.GetDelegateOf(sourceProp.PropertyType, true),
                   body: Expression.Call(
                       instance: Expression.Constant(webObject),
                       method: p.GetSetMethod(),
                       arguments: new Expression[] { param }),
                   parameters: new ParameterExpression[] { param })
                   .Compile()
                   .DynamicInvoke(sourceVal);

            });
            return webObject;
        }
        
        public static P ToPoco<P, W>(W webObject) {
            if (webObject == null) { return default(P); }
            
            //properties on W that need mapping
            PropertyInfo[] allmappedProps = Mapping.GetAllMappedProps(typeof(W));
            P pocoObject = Activator.CreateInstance<P>();//result object instance creation
            
            Array.ForEach(allmappedProps, p => { 
                //getting the target propperty
                PropertyInfo targetProp = typeof(P).GetProperty(p.GetCustomAttribute<MapsToAttribute>().PropertyName);
                
                //gets the value from the property 
                var sourceVal=
                    Expression.Lambda(delegateType: Mapping.GetDelegateOf(targetProp.PropertyType, false),
                        body: Expression.Call(
                            instance: Expression.Constant(webObject), 
                            method: p.GetGetMethod()),
                        parameters: new ParameterExpression[] { }).Compile().DynamicInvoke();

                 ParameterExpression param = Expression.Parameter(targetProp.PropertyType);

                //setting the value of the property in the target property expression
                 Expression.Lambda(
                    delegateType: Mapping.GetDelegateOf(targetProp.PropertyType, true),
                    body: Expression.Call(
                        instance: Expression.Constant(pocoObject), 
                        method:targetProp.GetSetMethod(),
                        arguments: new Expression[]{param}), 
                    parameters: new ParameterExpression[]{param})
                    .Compile()
                    .DynamicInvoke(sourceVal);
                
            });
            return pocoObject;
        }

        public static Type GetDelegateOf(Type t, bool isAction)
        {
            switch (t.Name.ToLower())
            {
                case "string":
                    return isAction == false ? typeof(Func<string>) : typeof(Action<string>);
                case "int32":
                    return isAction == false ? typeof(Func<Int32>) : typeof(Action<Int32>);
                case "int":
                    return isAction == false ? typeof(Func<int>) : typeof(Action<int>);
                case "int16":
                    return isAction == false ? typeof(Func<short>) : typeof(Action<short>);
                case "datetime":
                    return isAction == false ? typeof(Func<DateTime>) : typeof(Action<DateTime>);
                default:
                    break;

            } return null;
        }
        
        private static PropertyInfo[] GetAllMappedProps(Type t)
        {
            return t
                .GetProperties()
                .Where(p => p.GetCustomAttribute<MapsToAttribute>() != null)
                .ToArray<PropertyInfo>();
        }

      
    }
}

