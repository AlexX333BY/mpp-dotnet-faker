using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class DecimalValueGenerator : IBaseTypeGenerator
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
