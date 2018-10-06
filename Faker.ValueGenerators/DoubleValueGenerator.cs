using System;

namespace Faker.ValueGenerators
{
    public class DoubleValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return new Random().NextDouble();
        }

        public DoubleValueGenerator()
        {
            GeneratedType = typeof(double);
        }
    }
}
