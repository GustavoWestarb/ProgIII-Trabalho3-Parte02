using CryptSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_Programacao_3.Helper_Code
{
    public static class Encryption
    {

        public static string Encode(string password)
        {
            return Crypter.MD5.Crypt(password);
        }

        public static bool Compare(string password, string hash)
        {
            return Crypter.CheckPassword(password, hash);
        }

    }
}