using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Office.Work.Platform.Update
{
    /// <summary>
    /// 字符串加密类写法
    /// </summary>
    public static class StringCrypt
    {
        private const string KEY_64 = "ChinabCd";
        private const string IV_64 = "ChinabCd";

        /// <summary>
        /// 按指定键值进行加密
        /// </summary>
        /// <param name="strContent">要加密字符</param>
        /// <param name="strKey">自定义键值，如没提供则使用常量"KEY_64"</param>
        /// <returns></returns>

        public static string EnCrypt(string strContent, string strKey)

        {
            if (string.IsNullOrEmpty(strContent)) return string.Empty;
            strKey ??= KEY_64;
            if (strKey.Length > 8) strKey = strKey.Substring(0, 8);
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(strKey);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(strContent);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        /// <summary>
        /// 按指定键值进行解密
        /// </summary>
        /// <param name="strContent">要解密字符</param>
        /// <param name="strKey">加密时使用的键值</param>
        /// <returns></returns>
        public static string DeCrypt(string strContent, string strKey)
        {
            if (string.IsNullOrEmpty(strContent)) return string.Empty;
            strKey ??= KEY_64;
            if (strKey.Length > 8) strKey = strKey.Substring(0, 8);
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(strKey);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(strContent);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
    }
}
