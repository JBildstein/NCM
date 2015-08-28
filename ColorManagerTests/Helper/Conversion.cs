using System;
using System.Linq;
using System.Reflection;
using ColorManager;
using ColorManager.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorManagerTests.Conversions
{
    public unsafe abstract class Conversion<T, U>
        where T : Color
        where U : Color
    {
        T Color1;
        U Color2;

        ConversionDelegate ConvToT;
        ConversionDelegate ConvToU;

        protected const double Margin = 0.00005;
        
        protected abstract double[] Rand_In_T { get; }
        protected abstract double[] Rand_Out_U { get; }
        protected abstract double[] Min_Out_U { get; }
        protected abstract double[] Max_Out_U { get; }

        protected abstract double[] Rand_In_U { get; }
        protected abstract double[] Rand_Out_T { get; }
        protected abstract double[] Min_Out_T { get; }
        protected abstract double[] Max_Out_T { get; }

        protected Conversion(T Color1, U Color2)
        {
            this.Color1 = Color1;
            this.Color2 = Color2;
            
            var types = Assembly.GetAssembly(typeof(ConversionPath)).GetTypes();
            var paths = (from t in types
                        where t.IsSubclassOf(typeof(ConversionPath)) && !t.IsAbstract
                        select t).ToArray();

            int c = 0;
            for (int i = 0; i < paths.Length; i++)
            {
                var args = paths[i].BaseType.GetGenericArguments();
                if (args.Length == 2)
                {
                    if (args[0] == typeof(U) && args[1] == typeof(T))
                    {
                        ConvToT = GetDelegate(paths[i]);
                        c++;
                    }
                    else if (args[0] == typeof(T) && args[1] == typeof(U))
                    {
                        ConvToU = GetDelegate(paths[i]);
                        c++;
                    }

                    if (c == 2) break;
                }
            }

            if (ConvToU == null || ConvToT == null) throw new MissingMethodException();
        }

        #region T -> U
        
        public void T_U_Random()
        {
            Test(Rand_In_T, Rand_Out_U, true);
        }
        
        public void T_U_Min()
        {
            Test(CleanValues(Color1.MinValues, true), Min_Out_U, true);
        }
        
        public void T_U_Max()
        {
            Test(CleanValues(Color1.MaxValues, false), Max_Out_U, true);
        }

        #endregion

        #region U -> T
        
        public void U_T_Random()
        {
            Test(Rand_In_U, Rand_Out_T, false);
        }
        
        public void U_T_Min()
        {
            Test(CleanValues(Color2.MinValues, true), Min_Out_T, false);
        }

        public void U_T_Max()
        {
            Test(CleanValues(Color2.MaxValues, false), Max_Out_T, false);
        }

        #endregion

        private void Test(double[] inValues, double[] outValues, bool toU)
        {
            PathCheckData path;
            if (toU) path = new PathCheckData(Color1, Color2, ConvToU, inValues, outValues);
            else path = new PathCheckData(Color2, Color1, ConvToT, inValues, outValues);

            CheckConversionPath(path);
        }
        
        private static ConversionDelegate GetDelegate(Type tp)
        {
            var m = tp.GetMethod("Convert", new Type[] { typeof(double*), typeof(double*), typeof(ConversionData) });
            return Delegate.CreateDelegate(typeof(ConversionDelegate), m) as ConversionDelegate;
        }

        private static void CheckConversionPath(PathCheckData pathData)
        {
            double* inColor = stackalloc double[pathData.inCount];
            double* outColor = stackalloc double[pathData.outCount];
            var data = new TestConversionData(pathData.inColor, pathData.outColor);

            for (int i = 0; i < pathData.inCount; i++) { inColor[i] = pathData.inValues[i]; }

            pathData.conversion(inColor, outColor, data);

            //Ensure that the input values have not changed
            for (int i = 0; i < pathData.inCount; i++)
            {
                Assert.AreEqual(inColor[i], pathData.inValues[i], 0);
            }

            //Check the output values
            for (int i = 0; i < pathData.outCount; i++)
            {
                Assert.AreEqual(outColor[i], pathData.outValues[i], Margin);
            }
        }

        private static double[] CleanValues(double[] values, bool min)
        {
            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (double.IsNaN(values[i])) result[i] = min ? -255d : 255d;
                else result[i] = values[i];
            }
            return result;
        }
    }

    public sealed class PathCheckData
    {
        public int inCount;
        public int outCount;

        public Color inColor;
        public Color outColor;
        public ConversionDelegate conversion;
        public double[] inValues;
        public double[] outValues;

        public PathCheckData(Color inColor, Color outColor,
            ConversionDelegate conversion,
            double[] inValues, double[] outValues)
        {
            if (inColor == null ||
                outColor == null ||
                conversion == null ||
                inValues == null ||
                outValues == null)
                throw new ArgumentNullException();

            if (inColor.ChannelCount != inValues.Length) throw new ArgumentException();
            if (outColor.ChannelCount != outValues.Length) throw new ArgumentException();
            
            this.inColor = inColor;
            this.outColor = outColor;
            this.inValues = inValues;
            this.outValues = outValues;
            this.conversion = conversion;

            inCount = inValues.Length;
            outCount = outValues.Length;
        }
    }
}
