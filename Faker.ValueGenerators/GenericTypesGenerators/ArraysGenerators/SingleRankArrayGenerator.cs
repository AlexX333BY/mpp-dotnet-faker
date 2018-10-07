using System;
using System.Collections.Generic;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker.ValueGenerators.GenericTypesGenerators.ArraysGenerators
{
    public class SingleRankArrayGenerator : IArrayGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly ByteValueGenerator byteValueGenerator;

        protected IDictionary<Type, IBaseTypeGenerator> BaseTypesGenerators
        { get; set; }

        public int ArrayRank
        { get; protected set; }

        public object Generate(Type baseType)
        {
            if (BaseTypesGenerators.TryGetValue(baseType, out IBaseTypeGenerator baseTypeGenerator))
            {
                Array result = Array.CreateInstance(baseType, (byte)byteValueGenerator.Generate());

                for (int i = 0; i < result.Length; i++)
                {
                    result.SetValue(baseTypeGenerator.Generate(), i);
                }
                return result;
            }
            else
            {
                return Array.CreateInstance(baseType, 0);
            }
        }

        public SingleRankArrayGenerator(IDictionary<Type, IBaseTypeGenerator> baseTypeGenerators)
        {
            GeneratedType = typeof(Array);
            BaseTypesGenerators = baseTypeGenerators;
            byteValueGenerator = new ByteValueGenerator();
            ArrayRank = 1;
        }
    }
}
