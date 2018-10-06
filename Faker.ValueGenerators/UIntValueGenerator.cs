using System;

namespace Faker.ValueGenerators
{
    public class UIntValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (uint)new Random().NextDouble();
        }

        public UIntValueGenerator()
        {
            GeneratedType = typeof(uint);
        }
    }
}
