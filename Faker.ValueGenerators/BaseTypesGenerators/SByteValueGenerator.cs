using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class SByteValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (sbyte)random.Next();
        }

        public SByteValueGenerator()
        {
            GeneratedType = typeof(sbyte);
            random = new Random();
        }
    }
}
