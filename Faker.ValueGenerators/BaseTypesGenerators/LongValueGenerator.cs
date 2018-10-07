using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class LongValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (long)random.NextDouble();
        }

        public LongValueGenerator()
        {
            GeneratedType = typeof(long);
            random = new Random();
        }
    }
}
