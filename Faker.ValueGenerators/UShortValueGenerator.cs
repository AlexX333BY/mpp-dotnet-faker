using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class UShortValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (ushort)new Random().Next();
        }

        public UShortValueGenerator()
        {
            GeneratedType = typeof(ushort);
        }
    }
}
