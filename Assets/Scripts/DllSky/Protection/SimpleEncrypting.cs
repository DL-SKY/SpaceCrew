using System;
using System.Text;

namespace DllSky.Protection
{
    //Base64 преобразование
    public static class SimpleEncrypting
    {
        /// <summary>
        /// "Шифрование". Преобразуем в Base64
        /// </summary>
        public static string Encode(string _commonText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(_commonText);

            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// "Дешифровка". Преобразуем обратно из Base64
        /// </summary>
        public static string Decode(string _base64Text)
        {
            var base64EncodedBytes = Convert.FromBase64String(_base64Text);

            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
