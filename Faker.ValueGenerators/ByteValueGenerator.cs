using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class ByteValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (byte)new Random().Next();
        }

        public ByteValueGenerator()
        {
            GeneratedType = typeof(byte);
        }
    }
}
