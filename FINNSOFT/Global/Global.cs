using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Net;
using System.Windows.Forms;

namespace FINNSOFT
{
    class Global
    {
        public static string usertype = "";
        public static string username = "";
        public static string newuser = "";
        public static string company = "";
        public static string finyr = "";
        public static string branch = "";
        public static string userId = "";
        public static DateTime gFromDt;
        public static DateTime gToDt;
        public static int ledgyear = 0;
        public static int ledgmon = 0;
        public static string userrole="";
        public static DateTime trlfrmdt;
        public static DateTime trltodt;
    

        public static bool IsOpenForm(Type formType)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == formType.Name)
                    return true;

            return false;
        }

        public static string FetchHost()
        {
            string host = "";
            try
            {
                host = Dns.GetHostName();
            }
            catch (Exception)
            {
                host = "Unknown";
            }

            return host;
        }

        public static string FetchIP()
        {
            string ip = "Unknown";
            try
            {
                string host = Dns.GetHostName();
                IPHostEntry ip1 = Dns.GetHostEntry(host);
                ip = ip1.AddressList[1].ToString();
            }
            catch (Exception)
            {
                
            }

            return ip;
        }

        public static string getip()
        {
            string ip = "Unknown";
            try
            {
                string host = Dns.GetHostName();
                IPHostEntry ip1 = Dns.GetHostEntry(host);
                ip = ip1.AddressList[1].ToString();
                ip = host + " ->" + ip;
            }
            catch (Exception)
            { }
            return ip;
        }

        
        public static string Encrypt_String(string plaintext)
        {
            string outstr = null;

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(plaintext);
            System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();
            // Get the key from config file
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //keyArray = UTF8Encoding.UTF8.GetBytes(key);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            outstr = Convert.ToBase64String(resultArray, 0, resultArray.Length);

            return outstr;
        }

        public static string Decrypt_String(string cipherString)
        {
            string outstr = null;

            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            //keyArray = UTF8Encoding.UTF8.GetBytes(key);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            outstr = UTF8Encoding.UTF8.GetString(resultArray);
            return outstr;
        }
    }
}
