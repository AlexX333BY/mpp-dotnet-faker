using System;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker.ValueGenerators.CustomGenerators
{
    public class IntNonRandomGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; set; }

        public object Generate()
        {
            return 42;
        }
    }
}
