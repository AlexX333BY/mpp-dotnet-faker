using System;

namespace Faker.ValueGenerators
{
    public class LongValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (long)new Random().NextDouble();
        }

        public LongValueGenerator()
        {
            GeneratedType = typeof(long);
        }
    }
}
