using System;
using System.Collections.Generic;
using Faker.ValueGenerators.BaseTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators;
using Faker.ValueGenerators.GenericTypesGenerators.ArraysGenerators;

namespace Faker.ValueGenerators
{
    public static class GeneratorsSetCreator
    {
        public static Dictionary<Type, IBaseTypeGenerator> CreateBaseTypesGeneratorsDictionary()
        {
            var dictionary = new Dictionary<Type, IBaseTypeGenerator>();
            IBaseTypeGenerator generator;

            generator = new BoolValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new ByteValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new DateTimeValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new DecimalValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new DoubleValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new FloatValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new IntValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new LongValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new SByteValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new ShortValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new UIntValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new ULongValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);
            generator = new UShortValueGenerator();
            dictionary.Add(generator.GeneratedType, generator);

            return dictionary;
        }

        public static Dictionary<Type, IGenericTypeGenerator> CreateGenericTypesGeneratorsDictionary(Dictionary<Type, IBaseTypeGenerator> baseTypesGenerators)
        {
            var dictionary = new Dictionary<Type, IGenericTypeGenerator>();
            IGenericTypeGenerator generator;

            generator = new ListGenerator(baseTypesGenerators);
            dictionary.Add(generator.GeneratedType, generator);

            return dictionary;
        }

        public static Dictionary<int, IArrayGenerator> CreateArraysGeneratorsDictionary(Dictionary<Type, IBaseTypeGenerator> baseTypesGenerators)
        {
            var dictionary = new Dictionary<int, IArrayGenerator>();
            IArrayGenerator generator;

            generator = new SingleRankArrayGenerator(baseTypesGenerators);
            dictionary.Add(generator.ArrayRank, generator);

            return dictionary;
        }
    }
}
