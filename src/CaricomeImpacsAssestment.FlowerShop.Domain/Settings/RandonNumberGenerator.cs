using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Settings
{
    public class RandonNumberGenerator
    {
        public string RandonCryptoCaraptherAlfa(int setmaxSize)
        {
            int maxSize = setmaxSize;

            char[] chars = new char[62];
            string a;
            a = "1234567890ABCDEFGHIJKLMNOPQRZ";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size - 1 + 1];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
                result.Append(chars[b % (chars.Length - 1)]);
            return result.ToString();
        }

        public string RandonCriptoCaraptherNumber(int setmaxSize)
        {
            int maxSize = setmaxSize;

            char[] chars = new char[62];
            string a;
            a = "1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size - 1 + 1];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
                result.Append(chars[b % (chars.Length - 1)]);
            return result.ToString();
        }

        public int RandomNumber()
        {
            var random = new Random();
            int randomNumber = random.Next(10000, 99999);
            return randomNumber;



        }
    }
}
