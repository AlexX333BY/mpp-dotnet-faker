using System;

namespace Faker.UsageExample
{
    public class Example
    {
        public static void Main(string[] args)
        {
            Faker faker = new Faker();
            ExampleClass exampleObject = faker.Create<ExampleClass>();
            Console.ReadKey();
        }
    }
}
