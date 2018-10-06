using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class DoubleValueGenerator : IBaseTypeGenerator
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
