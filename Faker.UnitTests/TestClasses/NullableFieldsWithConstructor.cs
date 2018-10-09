namespace Faker.UnitTests.TestClasses
{
    public class NullableFieldsWithConstructor : NullableFieldsNoConstructor
    {
        public NullableFieldsWithConstructor(string str)
        {
            stringField = str;
        }
    }
}
