using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class UIntValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (uint)random.NextDouble();
        }

        public UIntValueGenerator()
        {
            GeneratedType = typeof(uint);
            random = new Random();
        }
    }
}
