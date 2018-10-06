using System;

namespace Faker.ValueGenerators
{
    public class ULongValueGenerator : IValueGenerator
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
