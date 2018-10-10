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

        public void Add<TClass, TPropertyType, TGenerator>(Expression<Func<TClass, TPropertyType>> expression)
            where TClass : class
            where TGenerator : IBaseTypeGenerator, new()
        {
            Expression expressionBody = expression.Body;
            if (expressionBody.NodeType == ExpressionType.MemberAccess)
            {
                generators.Add((PropertyInfo)((MemberExpression)expressionBody).Member, (IBaseTypeGenerator)Activator.CreateInstance(typeof(TGenerator)));
            }
            else
            {
                throw new ArgumentException("Illegal expression");
            }
        }

        public FakerConfig()
        {
            generators = new Dictionary<PropertyInfo, IBaseTypeGenerator>();
        }
    }
}
