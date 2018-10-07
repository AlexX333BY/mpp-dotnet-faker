using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class IntValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return random.Next();
        }

        public IntValueGenerator()
        {
            GeneratedType = typeof(int);
            random = new Random();
        }
    }
}
