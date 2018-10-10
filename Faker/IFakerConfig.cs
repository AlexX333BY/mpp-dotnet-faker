using System;
using System.Linq.Expressions;
using Faker.ValueGenerators.BaseTypesGenerators;

namespace Faker
{
    public interface IFakerConfig
    {
        void Add<T, U, W>(Expression<Func<T, U>> expression)
            where W : IBaseTypeGenerator;
    }
}
