using System;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Faker.UsageExample
{
    public class Example
    {
        public static void Main(string[] args)
        {
            Faker faker = new Faker();

            ConsoleJsonSerializer.Serialize(faker.Create<ExampleClassProperties>());
            ConsoleJsonSerializer.Serialize(faker.Create<ExampleClassConstructor>());

            Console.ReadKey();
        }
    }
}
