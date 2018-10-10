using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Faker.UsageExample
{
    [DataContract]
    public class ExampleClassProperties
    {
        [DataMember]
        public bool publicBoolField;
        [DataMember]
        protected bool nonPublicBoolField;

        [DataMember]
        public int PublicIntSetter
        { get; set; }

        [DataMember]
        public int NonPublicIntSetter
        { get; protected set; }

        [DataMember]
        private readonly int nonPublicIntField;

        [DataMember]
        public List<int> publicList;

        [DataMember]
        public ExampleClassProperties nestedObject;

        [DataMember]
        protected int customGeneratorCheckField;

        [DataMember]
        public int CustomGeneratorCheckProperty
        { get; set; }

        public ExampleClassProperties()
        { }
    }
}
