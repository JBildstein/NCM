using System;
using System.Text;
using ColorManager.ICC;

namespace ICCViewer
{
    public static class Conversion
    {
        public static string FromBytes(byte[] data, bool ASCII)
        {
            string txt = string.Empty;
            string val;
            for (int i = 0; i < data.Length; i++)
            {
                val = data[i].ToString().PadLeft(3, '0');
                txt += val + " ";
                if ((i + 1) % 10 == 0) txt += Environment.NewLine;
            }

            if (ASCII)
            {
                txt += Environment.NewLine + Environment.NewLine + "ASCII:" + Environment.NewLine;
                val = Encoding.ASCII.GetString(data);

                string tmp;
                for (int i = 0; i < val.Length; i++)
                {
                    if (val[i] == '\0') tmp = "\\0";
                    else if (val[i] == '\r') tmp = "\\r";
                    else if (val[i] == '\n') tmp = "\\n";
                    else if (val[i] == ' ') tmp = "spc";
                    else if (char.IsControl(val[i])) tmp = $"\\{(uint)val[i]}";
                    else tmp = val[i].ToString();
                    txt += tmp.PadRight(5);
                    if ((i + 1) % 10 == 0) txt += Environment.NewLine;
                }

                txt += Environment.NewLine + Environment.NewLine;
                txt += $"ASCII Complete: \"{val.Replace("\0", "")}\"";
            }

            return txt;
        }

        public static string FromBytesShort(byte[] data, bool ASCII)
        {
            string txt = string.Empty;
            for (int i = 0; i < data.Length; i++) { txt += $"{data[i]} "; }
            if (ASCII)
            {
                string ascii = GetAsciiString(data, false);
                return $"{txt} ({ascii})";
            }
            else return txt;
        }
        
        public static string FromLocalized(LocalizedString lstring)
        {
            return $"{nameof(lstring.Locale)}: {lstring.Locale.ToString()}{lstring.Text}";
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
    }
}
