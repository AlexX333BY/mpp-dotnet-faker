using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class UShortValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (ushort)random.Next();
        }

        public UShortValueGenerator()
        {
            GeneratedType = typeof(ushort);
            random = new Random();
        }
    }
}
