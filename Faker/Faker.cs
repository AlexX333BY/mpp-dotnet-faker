using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Faker.ValueGenerators;
using Faker.ValueGenerators.BaseTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators.ArraysGenerators;

namespace Faker
{
    public class Faker : IFaker
    {
        protected Dictionary<Type, IBaseTypeGenerator> baseTypesGenerators;
        protected Dictionary<Type, IGenericTypeGenerator> genericTypesGenerators;
        protected Dictionary<int, IArrayGenerator> arraysGenerators;
        protected Stack<Type> generatedTypes;

        protected const string defaultPluginsPath = "Plugins";

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        protected object Create(Type type)
        {
            if (baseTypesGenerators.TryGetValue(type, out IBaseTypeGenerator baseTypeGenerator))
            {
                return baseTypeGenerator.Generate();
            }
            else if (type.IsGenericType && genericTypesGenerators.TryGetValue(type.GetGenericTypeDefinition(), out IGenericTypeGenerator genericTypeGenerator))
            {
                return genericTypeGenerator.Generate(type.GenericTypeArguments[0]);
            }
            else if (type.IsArray && arraysGenerators.TryGetValue(type.GetArrayRank(), out IArrayGenerator arrayGenerator))
            {
                return arrayGenerator.Generate(type.GetElementType());
            }
            else if (type.IsClass && !type.IsGenericType && !type.IsArray && !generatedTypes.Contains(type))
            {
                int maxConstructorFieldsCount = 0, curConstructorFieldsCount;
                ConstructorInfo constructorToUse = null;
                object generated;

                foreach (ConstructorInfo constructor in type.GetConstructors())
                {
                    curConstructorFieldsCount = constructor.GetParameters().Length;
                    if (curConstructorFieldsCount > maxConstructorFieldsCount)
                    {
                        maxConstructorFieldsCount = curConstructorFieldsCount;
                        constructorToUse = constructor;
                    }
                }

                generatedTypes.Push(type);
                if (constructorToUse == null)
                {
                    generated = CreateByProperties(type);
                }
                else
                {
                    generated = CreateByConstructor(type, constructorToUse);
                }
                generatedTypes.Pop();
                return generated;
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }

        protected object CreateByProperties(Type type)
        {
            object generated = Activator.CreateInstance(type);

            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                fieldInfo.SetValue(generated, Create(fieldInfo.FieldType));
            }

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.SetProperty))
            {
                propertyInfo.SetValue(generated, Create(propertyInfo.PropertyType));
            }

            return generated;
        }

        protected object CreateByConstructor(Type type, ConstructorInfo constructor)
        {
            var parametersValues = new List<object>();

            foreach (ParameterInfo parameterInfo in constructor.GetParameters())
            {
                parametersValues.Add(Create(parameterInfo.ParameterType));
            }

            return constructor.Invoke(parametersValues.ToArray());
        }

        public Faker()
            : this(defaultPluginsPath)
        { }

        public Faker(string pluginsPath)
        {
            IBaseTypeGenerator pluginGenerator;

            generatedTypes = new Stack<Type>();
            baseTypesGenerators = GeneratorsSetCreator.CreateBaseTypesGeneratorsDictionary();
            genericTypesGenerators = GeneratorsSetCreator.CreateGenericTypesGeneratorsDictionary(baseTypesGenerators);
            arraysGenerators = GeneratorsSetCreator.CreateArraysGeneratorsDictionary(baseTypesGenerators);

            List<Assembly> assemblies = new List<Assembly>();
            try
            {
                foreach (string file in Directory.GetFiles(pluginsPath, "*.dll"))
                {
                    try
                    {
                        assemblies.Add(Assembly.LoadFile(new FileInfo(file).FullName));
                    }
                    catch (BadImageFormatException)
                    { }
                    catch (FileLoadException)
                    { }
                }
            }
            catch (DirectoryNotFoundException)
            { }

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    foreach (Type typeInterface in type.GetInterfaces())
                    {
                        if (typeInterface.Equals(typeof(IBaseTypeGenerator)))
                        {
                            pluginGenerator = (IBaseTypeGenerator)Activator.CreateInstance(type);
                            baseTypesGenerators.Add(pluginGenerator.GeneratedType, pluginGenerator);
                        }
                    }
                }
            }
        }
    }
}
