using System;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker.ValueGenerators.CustomGenerators
{
    public class IntNonRandomGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return 42;
        }

        public IntNonRandomGenerator()
        {
            GeneratedType = typeof(int);
        }
    }
}
