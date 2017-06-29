using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.IO;

namespace NeuroSpeech.UIAtoms.Web
{

    public class FormData : ByteArrayContent
    {

        List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();


        public FormData(params KeyValuePair<string, string>[] items) : base(EncodeItems(items))
        {
            this.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }

        private static byte[] EncodeItems(KeyValuePair<string, string>[] values)
        {
            using (StringWriter sw = new StringWriter())
            {
                foreach (var item in values)
                {
                    sw.Write(item.Key);
                    sw.Write("=");
                    sw.Write(Encode(item.Value));
                    sw.Write("&");
                }

                return System.Text.Encoding.UTF8.GetBytes(sw.ToString());
            }
        }

        private static string Encode(string value)
        {
            int limit = 2000;
            StringBuilder sb = new StringBuilder(value.Length);
            int loops = value.Length / limit;

            for (int i = 0; i <= loops; i++)
            {
                if (i < loops)
                {
                    sb.Append(Uri.EscapeDataString(value.Substring(limit * i, limit)));
                }
                else
                {
                    sb.Append(Uri.EscapeDataString(value.Substring(limit * i)));
                }
            }

            return sb.ToString();
        }

    }

}