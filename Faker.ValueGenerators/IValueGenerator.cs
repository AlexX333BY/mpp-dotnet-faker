namespace Faker.ValueGenerators
{
    public interface IValueGenerator<T>
    {
        T Generate();
    }
}
