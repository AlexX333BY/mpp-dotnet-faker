using System;

namespace Faker.ValueGenerators.StringGeneratorPlugin
{
    public class StringValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            Random random = new Random();
            byte[] stringBytes = new byte[random.Next(0, byte.MaxValue)]; // max length of output string will be max value of byte (255) * 4 / 3

            random.NextBytes(stringBytes);
            return Convert.ToBase64String(stringBytes);
        }

        public StringValueGenerator()
        {
            GeneratedType = typeof(string);
        }
    }
}
