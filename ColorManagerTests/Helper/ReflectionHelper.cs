using System;
using System.Reflection;

namespace ColorManagerTests
{
    public static class ReflectionHelper
    {
        public static MethodInfo GetMethod(string name, Type tp, Type[] args)
        {
            args = args ?? Type.EmptyTypes;
            return tp.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public, null, args, null);
        }
    }
}
