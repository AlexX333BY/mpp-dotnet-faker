using System;

namespace Faker.ValueGenerators
{
    public class UShortValueGenerator : IValueGenerator
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
