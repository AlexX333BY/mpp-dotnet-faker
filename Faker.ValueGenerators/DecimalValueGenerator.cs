using System;

namespace Faker.ValueGenerators
{
    public class DecimalValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (decimal)new Random().NextDouble();
        }

        public DecimalValueGenerator()
        {
            GeneratedType = typeof(decimal);
        }
    }
}
