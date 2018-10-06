using System;

namespace Faker.ValueGenerators
{
    public interface IValueGenerator
    {
        object Generate();
        Type GeneratedType { get; }
    }
}
