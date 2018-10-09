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

            using (var jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(Console.OpenStandardOutput(), Encoding.UTF8, ownsStream: true, indent: true))
            {
                new DataContractJsonSerializer(typeof(ExampleClassProperties)).WriteObject(jsonWriter, faker.Create<ExampleClassProperties>());
            }

            using (var jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(Console.OpenStandardOutput(), Encoding.UTF8, ownsStream: true, indent: true))
            {
                new DataContractJsonSerializer(typeof(ExampleClassConstructor)).WriteObject(jsonWriter, faker.Create<ExampleClassConstructor>());
            }
            Console.ReadKey();
        }
    }
}
