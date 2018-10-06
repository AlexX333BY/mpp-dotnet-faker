using System;

namespace Faker.ValueGenerators
{
    public class BoolValueGenerator : IValueGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return new Random().Next() % 2 == 0;
        }

        public BoolValueGenerator()
        {
            GeneratedType = typeof(bool);
        }
    }
}
