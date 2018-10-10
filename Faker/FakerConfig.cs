﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker
{
    public class FakerConfig : IFakerConfig
    {
        protected Dictionary<PropertyInfo, IBaseTypeGenerator> generators;

        public void Add<T, U, W>(Expression<Func<T, U>> expression)
            where T : class
            where W : IBaseTypeGenerator, new()
        { }

        public FakerConfig()
        {
            generators = new Dictionary<PropertyInfo, IBaseTypeGenerator>();
        }
    }
}
