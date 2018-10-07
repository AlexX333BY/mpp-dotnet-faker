using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class BoolValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return new Random().Next() % 2 == 0;
        }

        public BoolValueGenerator()
        {
            GeneratedType = typeof(bool);
        }
    }
}
