using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class ByteValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }
        protected readonly Random random;

        public object Generate()
        {
            return (byte)random.Next();
        }

        public ByteValueGenerator()
        {
            GeneratedType = typeof(byte);
            random = new Random();
        }
    }
}
