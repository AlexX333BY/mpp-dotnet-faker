using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public class Faker : IFaker
    {
        public T Create<T>()
        {
            Type objectType = typeof(T);
            int maxConstructorFieldsCount = 0, curConstructorFieldsCount;
            ConstructorInfo constructorToUse = null;

            foreach (ConstructorInfo constructor in objectType.GetConstructors())
            {
                curConstructorFieldsCount = constructor.GetParameters().Length;
                if (curConstructorFieldsCount > maxConstructorFieldsCount)
                {
                    maxConstructorFieldsCount = curConstructorFieldsCount;
                    constructorToUse = constructor;
                }
            }

            if (constructorToUse == null)
            {
                return CreateByProperties<T>();
            }
            else
            {
                return CreateByConstructor<T>(constructorToUse);
            }
        }

        protected T CreateByProperties<T>()
        {
            // TO BE IMPLEMENTED
            return default(T);
        }

        protected T CreateByConstructor<T>(ConstructorInfo constructor)
        {
            // TO BE IMPLEMENTED
            return default(T);
        }

        public Faker()
        { }
    }
}
