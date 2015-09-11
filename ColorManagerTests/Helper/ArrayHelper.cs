using System.Linq;

namespace ColorManagerTests
{
    public static class ArrayHelper
    {
        public static T[] Concat<T>(params T[][] arrs)
        {
            var result = new T[arrs.Sum(t => t.Length)];
            int offset = 0;
            for (int i = 0; i < arrs.Length; i++)
            {
                arrs[i].CopyTo(result, offset);
                offset += arrs[i].Length;
            }
            return result;
        }
        
        public static T[] Fill<T>(T value, int length)
        {
            var result = new T[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = value;
            }
            return result;
        }

        public static string Fill(char value, int length)
        {
            return "".PadRight(length, value);
        }
    }
}
