using System;

namespace Faker.ValueGenerators
{
    public class ShortValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return (short)new Random().Next();
        }

        public ShortValueGenerator()
        {
            GeneratedType = typeof(short);
        }
    }
}
