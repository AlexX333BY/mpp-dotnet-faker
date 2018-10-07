using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class ShortValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (short)random.Next();
        }

        public ShortValueGenerator()
        {
            GeneratedType = typeof(short);
            random = new Random();
        }
    }
}
