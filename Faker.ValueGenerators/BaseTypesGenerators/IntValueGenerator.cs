﻿using System;

namespace Faker.ValueGenerators.BaseTypesGenerators
{
    public class IntValueGenerator : IBaseTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        public object Generate()
        {
            return new Random().Next();
        }

        public IntValueGenerator()
        {
            GeneratedType = typeof(int);
        }
    }
}
