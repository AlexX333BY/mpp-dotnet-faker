﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Faker.ValueGenerators.BaseTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators;
using Faker.ValueGenerators;
using System.IO;

namespace Faker
{
    public class Faker : IFaker
    {
        protected Dictionary<Type, IBaseTypeGenerator> baseTypesGenerators;
        protected Dictionary<Type, IGenericTypeGenerator> genericTypesGenerators;
        protected Stack<Type> generatedTypes;

        protected const string defaultPluginsPath = "Plugins";

        public T Create<T>()
        {
            generatedTypes.Push(typeof(T));
            T generatedObject = (T)Create(typeof(T));
            generatedTypes.Pop();
            return generatedObject;
        }

        protected object Create(Type type)
        {
            int maxConstructorFieldsCount = 0, curConstructorFieldsCount;
            ConstructorInfo constructorToUse = null;

            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                curConstructorFieldsCount = constructor.GetParameters().Length;
                if (curConstructorFieldsCount > maxConstructorFieldsCount)
                {
                    maxConstructorFieldsCount = curConstructorFieldsCount;
                    constructorToUse = constructor;
                }
            }

            if (constructorToUse == null)
            {
                return CreateByProperties(type);
            }
            else
            {
                return CreateByConstructor(type, constructorToUse);
            }
        }

        protected object CreateByProperties(Type type)
        {
            object generated = Activator.CreateInstance(type);
            IBaseTypeGenerator baseTypeGenerator;
            IGenericTypeGenerator genericTypeGenerator;
            Type propertyType;

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.SetProperty))
            {
                propertyType = propertyInfo.PropertyType;
                if (baseTypesGenerators.TryGetValue(propertyType, out baseTypeGenerator))
                {
                    propertyInfo.SetValue(generated, baseTypeGenerator.Generate());
                }
                else if (genericTypesGenerators.TryGetValue(propertyType, out genericTypeGenerator))
                {
                    propertyInfo.SetValue(generated, genericTypeGenerator.Generate(propertyType.GenericTypeArguments[0]));
                }
                else if (propertyType.IsClass && !generatedTypes.Contains(propertyType))
                {
                    generatedTypes.Push(propertyType);
                    propertyInfo.SetValue(generated, Create(type));
                    generatedTypes.Pop();
                }
            }

            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                propertyType = fieldInfo.FieldType;
                if (baseTypesGenerators.TryGetValue(propertyType, out baseTypeGenerator))
                {
                    fieldInfo.SetValue(generated, baseTypeGenerator.Generate());
                }
                else if (genericTypesGenerators.TryGetValue(propertyType, out genericTypeGenerator))
                {
                    fieldInfo.SetValue(generated, genericTypeGenerator.Generate(propertyType.GenericTypeArguments[0]));
                }
                else if (propertyType.IsClass && !generatedTypes.Contains(propertyType))
                {
                    generatedTypes.Push(propertyType);
                    fieldInfo.SetValue(generated, Create(type));
                    generatedTypes.Pop();
                }
            }

            return generated;
        }

        protected object CreateByConstructor(Type type, ConstructorInfo constructor)
        {
            Type parameterType;
            var parametersValues = new List<object>();

            foreach (ParameterInfo parameterInfo in constructor.GetParameters())
            {
                parameterType = parameterInfo.ParameterType;
                if (baseTypesGenerators.TryGetValue(parameterType, out IBaseTypeGenerator baseTypeGenerator))
                {
                    parametersValues.Add(baseTypeGenerator.Generate());
                }
                else if (genericTypesGenerators.TryGetValue(parameterType, out IGenericTypeGenerator genericTypeGenerator))
                {
                    parametersValues.Add(genericTypeGenerator.Generate(parameterType.GenericTypeArguments[0]));
                }
                else if (parameterType.IsClass && !generatedTypes.Contains(parameterType))
                {
                    generatedTypes.Push(parameterType);
                    parametersValues.Add(Create(parameterType));
                    generatedTypes.Pop();
                }
                else
                {
                    parametersValues.Add(Activator.CreateInstance(parameterType));
                }
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
