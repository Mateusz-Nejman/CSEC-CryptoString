using System;
using System.IO;
using System.Security.Cryptography;

namespace Nejman.CSEC
{
    public class CryptoString
    {
        private byte[] GetKey(string pass)
        {
            byte[] key = new byte[16];

            for (int a = 0; a < key.Length; a++)
            {
                if (a < pass.Length)
                    key[a] = (byte)pass[a];
                else
                    key[a] = (byte)a;
            }


            return key;
        }

        public byte[] Encrypt(string text, string pass)
        {
            RijndaelManaged rm = new RijndaelManaged();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rm.CreateEncryptor(GetKey(pass), GetKey(pass)), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(text);
            sw.Close();
            cs.Close();
            byte[] crypted = ms.ToArray();
            ms.Close();
            return crypted;
        }

        public string Decrypt(byte[] data, string pass)
        {
            RijndaelManaged rm = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(data);
            CryptoStream cs = new CryptoStream(ms, rm.CreateDecryptor(GetKey(pass),GetKey(pass)), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            string text = sr.ReadToEnd();
            sr.Close();
            cs.Close();
            ms.Close();
            return text;
        }
    }
}
