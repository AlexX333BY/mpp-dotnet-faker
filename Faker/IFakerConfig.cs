using System;
using System.Linq.Expressions;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker
{
    public interface IFakerConfig
    {
        void Add<TClass, TPropertyType, TGenerator>(Expression<Func<TClass, TPropertyType>> expression)
            where TClass : class
            where TGenerator : IBaseTypeGenerator, new();
    }
}
