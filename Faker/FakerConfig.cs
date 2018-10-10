using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker
{
    public class FakerConfig : IFakerConfig
    {
        protected Dictionary<PropertyInfo, IBaseTypeGenerator> generators;

        public void Add<T, U, W>(Expression<Func<T, U>> expression) where W : IBaseTypeGenerator
        {
            if (!typeof(W).IsClass)
            {
                throw new ArgumentException("W should be class type");
            }
        }

        public FakerConfig()
        {
            generators = new Dictionary<PropertyInfo, IBaseTypeGenerator>();
        }
    }
}
