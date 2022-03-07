using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class TypeExtension
{
    public static IEnumerable<Type> GetInheritedClasses(this Type baseType)
    {
        return Assembly.GetAssembly(baseType).GetTypes().Where(inherited =>
            inherited.IsClass && !inherited.IsAbstract && inherited.IsSubclassOf(baseType));
    }

    public static dynamic ConstructDefault(this Type type)
    {
        ParameterInfo[] parameters = type.GetConstructors()[0].GetParameters();
        dynamic[] insertionParams = new dynamic[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            if (parameters[i].ParameterType.IsValueType)
            {
                if (parameters[i].HasDefaultValue)
                {
                    insertionParams[i] = parameters[i].DefaultValue;
                }
                else
                {
                    insertionParams[i] = Activator.CreateInstance(parameters[i].ParameterType);
                }
            }
            else
            {
                insertionParams[i] = parameters[i].ParameterType.ConstructDefault();
            }
        }

        return Activator.CreateInstance(type, insertionParams);
    }

    public static IEnumerable<ParameterInfo> GetConstructorParameters(this Type type)
    {
        return type.GetConstructors()[0].GetParameters();
    }
}
