using System;
using System.Collections;
using System.Collections.Generic;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker.ValueGenerators.GenericTypesGenerators
{
    public class ListGenerator : IGenericTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly ByteValueGenerator byteValueGenerator;

        protected IDictionary<Type, IBaseTypeGenerator> BaseTypesGenerators
        { get; set; }

        public object Generate(Type baseType)
        {
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(baseType));

            if (BaseTypesGenerators.TryGetValue(baseType, out IBaseTypeGenerator baseTypeGenerator))
            {
                byte listSize = (byte)byteValueGenerator.Generate();

                for (int i = 0; i < listSize; i++)
                {
                    result.Add(baseTypeGenerator.Generate());
                }
            }
            return result;
        }

        public ListGenerator(IDictionary<Type, IBaseTypeGenerator> baseTypeGenerators)
        {
            GeneratedType = typeof(List<>);
            BaseTypesGenerators = baseTypeGenerators;
            byteValueGenerator = new ByteValueGenerator();
        }
    }
}
