using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuAppApiAutomation.Tests
{
    public class BaseTest
    {
        protected T CreateClient<T>() where T : class
        {
            var constructor= typeof(T).GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException($"Type {typeof(T)} must have a parameterless constructor.");

            return (T)constructor.Invoke(null);
        }
    }
}
