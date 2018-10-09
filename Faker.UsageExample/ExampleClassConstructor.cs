using System.Runtime.Serialization;

namespace Faker.UsageExample
{
    [DataContract]
    public class ExampleClassConstructor : ExampleClassProperties
    {
        public ExampleClassConstructor(int intValue, bool boolValue)
        {
            PublicIntSetter = intValue;
            publicBoolField = boolValue;
            NonPublicIntSetter = -intValue;
            nonPublicBoolField = boolValue;
        }
    }
}
