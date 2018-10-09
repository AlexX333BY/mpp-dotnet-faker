﻿using System.Collections.Generic;
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
        public List<int> publicList;

        [DataMember]
        public ExampleClassProperties nestedObject;

        public ExampleClassProperties()
        { }
    }
}
