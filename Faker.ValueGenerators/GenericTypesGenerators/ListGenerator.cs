﻿using System;
using System.Collections.Generic;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker.ValueGenerators.GenericTypesGenerators
{
    public class ListGenerator : IGenericTypeGenerator
    {
        public Type GeneratedType
        { get; protected set; }

        protected IDictionary<Type, IBaseTypeGenerator> BaseTypesGenerators
        { get; set; }

        public object Generate(Type baseType)
        {
            var result = new List<object>();

            if (BaseTypesGenerators.TryGetValue(baseType, out IBaseTypeGenerator baseTypeGenerator))
            {
                byte listSize = (byte)new ByteValueGenerator().Generate();

                for (int i = 0; i < listSize; i++)
                {
                    result.Add(baseTypeGenerator.Generate());
                }
            }
            return result;
        }

        public ListGenerator(IDictionary<Type, IBaseTypeGenerator> baseTypeGenerators)
        {
            GeneratedType = typeof(List<>);
            BaseTypesGenerators = baseTypeGenerators;
        }
    }
}
