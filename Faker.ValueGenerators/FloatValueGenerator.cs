using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class FloatValueGenerator : IBaseTypeGenerator
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
