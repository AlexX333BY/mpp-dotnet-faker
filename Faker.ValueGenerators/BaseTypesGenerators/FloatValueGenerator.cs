using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class FloatValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (float)random.NextDouble();
        }

        public FloatValueGenerator()
        {
            GeneratedType = typeof(float);
            random = new Random();
        }
    }
}
