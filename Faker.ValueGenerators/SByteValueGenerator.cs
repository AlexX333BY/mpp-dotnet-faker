using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class SByteValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (sbyte)new Random().Next();
        }

        public SByteValueGenerator()
        {
            GeneratedType = typeof(sbyte);
        }
    }
}
