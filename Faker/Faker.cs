using System;
using System.Collections.Generic;
using System.Reflection;
using Faker.ValueGenerators.BaseTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators.ArraysGenerators;
using Faker.ValueGenerators;
using System.IO;

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
            IArrayGenerator arrayGenerator;
            Type propertyType, fieldType;

            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                fieldType = fieldInfo.FieldType;
                if (baseTypesGenerators.TryGetValue(fieldType, out baseTypeGenerator))
                {
                    fieldInfo.SetValue(generated, baseTypeGenerator.Generate());
                }
                else if (fieldType.IsGenericType && genericTypesGenerators.TryGetValue(fieldType, out genericTypeGenerator))
                {
                    fieldInfo.SetValue(generated, genericTypeGenerator.Generate(fieldType.GenericTypeArguments[0]));
                }
                else if (fieldType.IsArray && arraysGenerators.TryGetValue(fieldType.GetArrayRank(), out arrayGenerator))
                {
                    fieldInfo.SetValue(generated, arrayGenerator.Generate(fieldType.GetElementType()));
                }
                else if (fieldType.IsClass && !fieldType.IsGenericType && !fieldType.IsArray && !generatedTypes.Contains(fieldType))
                {
                    generatedTypes.Push(fieldType);
                    fieldInfo.SetValue(generated, Create(fieldType));
                    generatedTypes.Pop();
                }
                else
                {
                    fieldInfo.SetValue(generated, Activator.CreateInstance(fieldType));
                }
            }

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.SetProperty))
            {
                propertyType = propertyInfo.PropertyType;
                if (baseTypesGenerators.TryGetValue(propertyType, out baseTypeGenerator))
                {
                    propertyInfo.SetValue(generated, baseTypeGenerator.Generate());
                }
                else if (propertyType.IsGenericType && genericTypesGenerators.TryGetValue(propertyType.GetGenericTypeDefinition(), out genericTypeGenerator))
                {
                    propertyInfo.SetValue(generated, genericTypeGenerator.Generate(propertyType.GenericTypeArguments[0]));
                }
                else if (propertyType.IsArray && arraysGenerators.TryGetValue(propertyType.GetArrayRank(), out arrayGenerator))
                {
                    propertyInfo.SetValue(generated, arrayGenerator.Generate(propertyType.GetElementType()));
                }
                else if (propertyType.IsClass && !propertyType.IsGenericType && !propertyType.IsArray && !generatedTypes.Contains(propertyType))
                {
                    generatedTypes.Push(propertyType);
                    propertyInfo.SetValue(generated, Create(propertyType));
                    generatedTypes.Pop();
                }
                else
                {
                    propertyInfo.SetValue(generated, Activator.CreateInstance(propertyType));
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
                else if (parameterType.IsGenericType && genericTypesGenerators.TryGetValue(parameterType, out IGenericTypeGenerator genericTypeGenerator))
                {
                    parametersValues.Add(genericTypeGenerator.Generate(parameterType.GenericTypeArguments[0]));
                }
                else if (parameterType.IsArray && arraysGenerators.TryGetValue(parameterType.GetArrayRank(), out IArrayGenerator arrayGenerator))
                {
                    parametersValues.Add(arrayGenerator.Generate(parameterType.GetElementType()));
                }
                else if (parameterType.IsClass && !parameterType.IsGenericType && !parameterType.IsArray && !generatedTypes.Contains(parameterType))
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
