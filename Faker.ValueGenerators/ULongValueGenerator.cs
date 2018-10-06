using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class ULongValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (ulong)new Random().NextDouble();
        }

        public ULongValueGenerator()
        {
            GeneratedType = typeof(ulong);
        }
    }
}
