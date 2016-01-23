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
                if ((i + 1) % 10 == 0) txt += val + Environment.NewLine;
                else if (i + 1 == data.Length) txt += val;
                else txt += val.PadRight(6);
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
                    else tmp = val[i].ToString();
                    tmp = tmp.PadLeft(3);
                    if ((i + 1) % 10 == 0) txt += tmp + Environment.NewLine;
                    else if (i + 1 == data.Length) txt += tmp;
                    else txt += tmp.PadRight(6);
                }
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
