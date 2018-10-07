using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class ULongValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (ulong)random.NextDouble();
        }

        public ULongValueGenerator()
        {
            GeneratedType = typeof(ulong);
            random = new Random();
        }
    }
}
