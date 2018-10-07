using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class DecimalValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (decimal)random.NextDouble();
        }

        public DecimalValueGenerator()
        {
            GeneratedType = typeof(decimal);
            random = new Random();
        }
    }
}
