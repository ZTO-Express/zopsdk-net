using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace zopsdk_net
{
    public class ZopClient
    {
        private ZopProperties properties;

        public ZopClient(ZopProperties properties)
        {
            this.properties = properties;
        }

        public string execute(ZopRequest request)
        {
            string url = request.url;
            NameValueCollection requestParams = request.requestParams;
            int i = 0;
            StringBuilder queryStringBuilder = new StringBuilder();
            StringBuilder digeStringBuilder = new StringBuilder();
            foreach (string key in requestParams.Keys)
            {
                if (i > 0)
                {
                    queryStringBuilder.AppendFormat("&{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(requestParams[key],Encoding.UTF8));
                    digeStringBuilder.AppendFormat("&{0}={1}", key, requestParams[key]);
                }
                else
                {
                    queryStringBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(requestParams[key],Encoding.UTF8));
                    digeStringBuilder.AppendFormat("{0}={1}", key, requestParams[key]);
                }
                i++;
            }
            
            NameValueCollection headers = new NameValueCollection();
            headers.Add("x-companyid", properties.companyid);
            string strToDigest = digeStringBuilder + properties.key;
            headers.Add("x-datadigest", EncryptMD5Base64(strToDigest));
            return HttpUtil.post(url, headers, queryStringBuilder.ToString(), request.timeout);
        }


        /// <summary>
        /// 编码方式
        /// </summary>
        /// <param name="Pars"></param>
        /// <returns></returns>
        private static byte[] EncodePars(NameValueCollection Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }
        /// <summary>
        /// 参数转为字符串
        /// </summary>
        /// <param name="Pars"></param>
        /// <returns></returns>
        private static String ParsToString(NameValueCollection Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }



        /// <summary>
        /// 开放平台签名[MD5 + BASE64]
        /// </summary>
        /// <param name="encryptStr">加密的字符串(请求数据[data] + 消息密钥[key])</param>
        /// <param name="charset">编码方式, 默认UTF-8</param>
        /// <returns></returns>
        public static string EncryptMD5Base64(string encryptStr, string charset = "UTF-8")
        {
            string rValue = "";
            var m5 = new MD5CryptoServiceProvider();

            byte[] inputBye;
            byte[] outputBye;
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encryptStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.UTF8.GetBytes(encryptStr);
            }
            outputBye = m5.ComputeHash(inputBye);
            rValue = Convert.ToBase64String(outputBye, 0, outputBye.Length);
            return rValue;
        }
    }
}
