using System;
using System.Collections.Generic;
using System.Reflection;
using Faker.ValueGenerators.BaseTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators;
using Faker.ValueGenerators;

namespace Faker
{
    public class Faker : IFaker
    {
        protected Dictionary<Type, IBaseTypeGenerator> baseTypesGenerators;
        protected Dictionary<Type, IGenericTypeGenerator> genericTypesGenerators;
        protected Stack<Type> generatedTypes;

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
            Type declaringType;

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.SetProperty))
            {
                declaringType = propertyInfo.DeclaringType;
                if (baseTypesGenerators.TryGetValue(declaringType, out baseTypeGenerator))
                {
                    propertyInfo.SetValue(generated, baseTypeGenerator.Generate());
                }
                else if (genericTypesGenerators.TryGetValue(declaringType, out genericTypeGenerator))
                {
                    propertyInfo.SetValue(generated, genericTypeGenerator.Generate(declaringType.GenericTypeArguments[0]));
                }
                else if (declaringType.IsClass && !generatedTypes.Contains(declaringType))
                {
                    generatedTypes.Push(declaringType);
                    propertyInfo.SetValue(generated, Create(type));
                    generatedTypes.Pop();
                }
            }

            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                declaringType = fieldInfo.DeclaringType;
                if (baseTypesGenerators.TryGetValue(declaringType, out baseTypeGenerator))
                {
                    fieldInfo.SetValue(generated, baseTypeGenerator.Generate());
                }
                else if (genericTypesGenerators.TryGetValue(declaringType, out genericTypeGenerator))
                {
                    fieldInfo.SetValue(generated, genericTypeGenerator.Generate(declaringType.GenericTypeArguments[0]));
                }
                else if (declaringType.IsClass && !generatedTypes.Contains(declaringType))
                {
                    generatedTypes.Push(declaringType);
                    fieldInfo.SetValue(generated, Create(type));
                    generatedTypes.Pop();
                }
            }

            return generated;
        }

        protected object CreateByConstructor(Type type, ConstructorInfo constructor)
        {
            // TO BE IMPLEMENTED
            throw new NotImplementedException();
        }

        public Faker()
        {
            generatedTypes = new Stack<Type>();
            baseTypesGenerators = GeneratorsSetCreator.CreateBaseTypesGeneratorsDictionary();
            genericTypesGenerators = GeneratorsSetCreator.CreateGenericTypesGeneratorsDictionary(baseTypesGenerators);
        }
    }
}
