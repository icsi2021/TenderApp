using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ICSIMemberPaymentLibrary.Helper.Utils
{
    public static class UriUtil
    {
        public static bool areSameDomainUrls(string url1, string url2)
        {
            return UriUtil.areSameDomainUrls(new Uri(url1), new Uri(url2));
        }

        public static bool areSameDomainUrls(Uri uri1, Uri uri2)
        {
            if (!uri1.IsAbsoluteUri)
            {
                return false;
            }
            if (!uri2.IsAbsoluteUri)
            {
                return false;
            }
            if (uri1.Host.Equals(uri2.Host, StringComparison.InvariantCultureIgnoreCase) && uri1.Port == uri2.Port && !(uri1.Scheme != uri2.Scheme))
            {
                return true;
            }
            return false;
        }

        public static string resolveAbsoluteUrl(string url)
        {
            if (url.ToLower().StartsWith("http"))
            {
                return url;
            }
            HttpContext current = HttpContext.Current;
            if (url[0] == '~')
            {
                url = url.Substring(1);
            }
            if (url.Length > 0 && url[0] != '/')
            {
                url = string.Concat('/', url);
            }
            if (current.Request.ApplicationPath != "/")
            {
                url = string.Concat(current.Request.ApplicationPath, url);
            }
            object[] host = new object[] { (current.Request.IsSecureConnection ? "https" : "http"), current.Request.Url.Host, (current.Request.Url.IsDefaultPort ? string.Empty : string.Concat(":", current.Request.Url.Port)), url };
            return string.Format("{0}://{1}{2}{3}", host);
        }

        public static string urlAppend(string url, params string[] args)
        {
            if ((int)args.Length % 2 != 0)
            {
                throw new ArgumentException("args should have an even length.");
            }
            if (url.IndexOf('?') == -1)
            {
                url = string.Concat(url, '?');
            }
            for (int i = 0; i < (int)args.Length; i = i + 2)
            {
                url = string.Concat(url, string.Format("&{0}={1}", HttpUtility.UrlEncode(args[i]), HttpUtility.UrlEncode(args[i + 1])));
            }
            return url;
        }

        public static List<KeyValuePair<string, string>> urlDecode(string string_0)
        {
            string str;
            char[] chrArray = new char[] { '&' };
            string[] strArrays = string_0.Split(chrArray, StringSplitOptions.None);
            List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string[] strArrays1 = strArrays[i].Split(new char[] { '=' });
                for (int j = 0; j < (int)strArrays1.Length; j++)
                {
                    strArrays1[j] = HttpUtility.UrlDecode(strArrays1[j]);
                }
                List<KeyValuePair<string, string>> keyValuePairs1 = keyValuePairs;
                string str1 = strArrays1[0];
                if ((int)strArrays1.Length > 1)
                {
                    str = strArrays1[1];
                }
                else
                {
                    str = null;
                }
                keyValuePairs1.Add(new KeyValuePair<string, string>(str1, str));
            }
            return keyValuePairs;
        }

    }
}
