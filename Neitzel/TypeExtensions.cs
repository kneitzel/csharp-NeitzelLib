using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neitzel
{
    /// <summary>
    /// Extensions to Type.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Creates a new instance of a given class name.
        /// </summary>
        /// <typeparam name="T">The type of the class or (or a super class / interface)</typeparam>
        /// <param name="className">Class name of the class to create an instance of. Must contain a default constructor!</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string className)
        {
            var type = Type.GetType(className, true);
            return (T) Activator.CreateInstance(type);
        }
    }
}
