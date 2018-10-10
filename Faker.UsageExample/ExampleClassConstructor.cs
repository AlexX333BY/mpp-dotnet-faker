using System.Runtime.Serialization;

namespace Faker.UsageExample
{
    [DataContract]
    public class ExampleClassConstructor : ExampleClassProperties
    {
        public ExampleClassConstructor(int intValue, bool boolValue, int customGeneratorCheckProperty)
        {
            PublicIntSetter = intValue;
            publicBoolField = boolValue;
            customGeneratorCheckField = customGeneratorCheckProperty;
            CustomGeneratorCheckProperty = customGeneratorCheckProperty;
        }
    }
}
