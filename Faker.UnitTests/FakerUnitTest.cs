using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker.UnitTests.TestClasses;

namespace Faker.UnitTests
{
    [TestClass]
    public class FakerUnitTest
    {
        [TestMethod]
        public void NullableFieldsTest()
        {
            Faker faker = new Faker();

            NullableFieldsNoConstructor noConstructorObject = faker.Create<NullableFieldsNoConstructor>();
            Assert.AreNotEqual(null, noConstructorObject.dateTimeField);
            Assert.AreNotEqual(null, noConstructorObject.stringField);
            Assert.AreNotEqual(null, noConstructorObject.objectField);

            NullableFieldsWithConstructor constructorObject = faker.Create<NullableFieldsWithConstructor>();
            Assert.AreNotEqual(null, constructorObject.stringField);
            Assert.AreEqual(default(DateTime), constructorObject.dateTimeField);
            Assert.AreEqual(default(object), constructorObject.objectField);
        }

        [TestMethod]
        public void NullablePropertiesTest()
        {
            Faker faker = new Faker();

            NullablePropertiesNoConstructor noConstructorObject = faker.Create<NullablePropertiesNoConstructor>();
            Assert.AreNotEqual(null, noConstructorObject.ObjectProperty);
            Assert.AreNotEqual(null, noConstructorObject.StringProperty);
            Assert.AreNotEqual(null, noConstructorObject.DateTimeProperty);

            NullablePropertiesWithConstructor constructorObject = faker.Create<NullablePropertiesWithConstructor>();
            Assert.AreNotEqual(null, constructorObject.DateTimeProperty);
            Assert.AreEqual(default(string), constructorObject.StringProperty);
            Assert.AreEqual(default(object), constructorObject.ObjectProperty);
        }

        [TestMethod]
        public void SelfRecursionTest()
        {
            Faker faker = new Faker();

            //if declare class with no constructor, default one will be created
            SelfRecursiveClassNoConstructor noConstructor = faker.Create<SelfRecursiveClassNoConstructor>();
            Assert.AreEqual(null, noConstructor.innerObject);
            SelfRecursiveClassWithConstructor selfRecursive = faker.Create<SelfRecursiveClassWithConstructor>();
            Assert.AreEqual(null, selfRecursive.innerObject);
        }

        [TestMethod]
        public void Test()
        {

        }
    }
}
