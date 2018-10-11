namespace Faker.UnitTests.TestClasses
{
    public class CustomGenerationConstructorClass : CustomGenerationPropertyClass
    {
        public CustomGenerationConstructorClass(int someValue)
        {
            SomeValue = someValue;
            this.someValue = someValue;
        }
    }
}
