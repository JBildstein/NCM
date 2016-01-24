using System;
using System.Collections.Generic;
using System.Text;
using ColorManager.ICC;

namespace ICCViewer
{
    public static class Conversion
    {
        private const int CurveResolution = 100;
        private const double CurveResolutionD = CurveResolution - 1;

        public static string FromBytes(byte[] data, bool ASCII)
        {
            StringBuilder builder = new StringBuilder();            
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString().PadLeft(3, '0'));
                builder.Append(" ");
                if ((i + 1) % 10 == 0) builder.AppendLine();
            }

            if (ASCII)
            {
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine("ASCII:");

                string val = Encoding.ASCII.GetString(data);

                string tmp;
                for (int i = 0; i < val.Length; i++)
                {
                    if (val[i] == '\0') tmp = "\\0";
                    else if (val[i] == '\r') tmp = "\\r";
                    else if (val[i] == '\n') tmp = "\\n";
                    else if (val[i] == ' ') tmp = "spc";
                    else if (char.IsControl(val[i])) tmp = $"\\{(uint)val[i]}";
                    else tmp = val[i].ToString();
                    builder.Append(tmp.PadRight(5));
                    if ((i + 1) % 10 == 0) builder.AppendLine();
                }

                builder.AppendLine();
                builder.AppendLine();
                builder.Append($"ASCII Complete: \"{val.Replace("\0", "")}\"");
            }

            return builder.ToString();
        }

        public static string FromBytesShort(byte[] data, bool ASCII)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++) { builder.Append($"{data[i]} "); }
            if (ASCII)
            {
                string ascii = GetAsciiString(data, false);
                builder.Append($" ({ascii})");
            }
            return builder.ToString();
        }
        
        public static string FromLocalized(LocalizedString lstring)
        {
            return $"{nameof(lstring.Locale)}: {lstring.Locale.ToString()}{lstring.Text}";
        }

        public static string Concat<T>(IEnumerable<T> values)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var val in values) { builder.Append($"{val.ToString()}; "); }
            return builder.ToString();
        }

        public static double[] FromParametricCurve(ParametricCurve curve)
        {
            switch (curve.type)
            {
                case 0: return GetParametricType0(curve);
                case 1: return GetParametricType1(curve);
                case 2: return GetParametricType2(curve);
                case 3: return GetParametricType3(curve);
                case 4: return GetParametricType4(curve);

                //This should never happen:
                default: return new double[] { 0.0 };
            }
        }

        public static double[] FromGamma(double gamma)
        {
            double[] result = new double[CurveResolution];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Math.Pow(i / CurveResolutionD, gamma);
            }
            return result;
        }

        private static string GetAsciiString(byte[] data, bool pad)
        {
            string ascii = Encoding.ASCII.GetString(data);
            string result = string.Empty;
            string tmp;
            for (int i = 0; i < ascii.Length; i++)
            {
                if (ascii[i] == '\0') tmp = "\\0";
                else if (ascii[i] == '\r') tmp = "\\r";
                else if (ascii[i] == '\n') tmp = "\\n";
                else if (ascii[i] == ' ') tmp = "spc";
                else tmp = ascii[i].ToString();

                if (pad) tmp += tmp.PadLeft(3);
                result += $"{tmp} ";
            }
            return result;
        }

        private static double[] GetParametricType0(ParametricCurve curve)
        {
            return FromGamma(curve.g);
        }

        private static double[] GetParametricType1(ParametricCurve curve)
        {
            double[] result = new double[CurveResolution];
            for (int i = 0; i < result.Length; i++)
            {
                double val = i / CurveResolutionD;
                if (val >= -curve.b / curve.a) result[i] = Math.Pow(curve.a * val + curve.b, curve.g);
                else result[i] = 0;
            }
            return result;
        }

        private static double[] GetParametricType2(ParametricCurve curve)
        {
            double[] result = new double[CurveResolution];
            for (int i = 0; i < result.Length; i++)
            {
                double val = i / CurveResolutionD;
                if (val >= -curve.b / curve.a) result[i] = Math.Pow(curve.a * val + curve.b, curve.g) + curve.c;
                else result[i] = curve.c;
            }
            return result;
        }

        private static double[] GetParametricType3(ParametricCurve curve)
        {
            double[] result = new double[CurveResolution];
            for (int i = 0; i < result.Length; i++)
            {
                double val = i / CurveResolutionD;
                if (val >= curve.d) result[i] = Math.Pow(curve.a * val + curve.b, curve.g);
                else result[i] = curve.c * val;
                result[i] = i / CurveResolutionD;
            }
            return result;
        }

        private static double[] GetParametricType4(ParametricCurve curve)
        {
            double[] result = new double[CurveResolution];
            for (int i = 0; i < result.Length; i++)
            {
                double val = i / CurveResolutionD;
                if (val >= curve.d) result[i] = Math.Pow(curve.a * val + curve.b, curve.g) + curve.c;
                else result[i] = curve.c * val + curve.f;
            }
            return result;
        }
    }
}
