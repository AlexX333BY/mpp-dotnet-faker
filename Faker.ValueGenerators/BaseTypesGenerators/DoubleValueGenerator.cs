using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class DoubleValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return random.NextDouble();
        }

        public DoubleValueGenerator()
        {
            GeneratedType = typeof(double);
            random = new Random();
        }
    }
}
