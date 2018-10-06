using System;

namespace Faker.ValueGenerators
{
    public class FloatValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (float)new Random().NextDouble();
        }

        public FloatValueGenerator()
        {
            GeneratedType = typeof(float);
        }
    }
}
