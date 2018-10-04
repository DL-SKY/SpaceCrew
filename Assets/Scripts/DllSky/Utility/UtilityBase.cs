/*
© Alexander Danilovsky, 2017
//------------------------------------------------
= Скрипт различных утилит и функций =

 * public static void OnVibrate(int _count, float _pause, bool _bVibrateEnable)
 * public static string GetMD5(string _string)
 * public static Color ColorStringHexToColorRGB(string _hex)
 * public static string GetStringFormatAmount3(int _amount)
 * public static string GetStringFormatAmount5(int _amount)
 * public static string GetStringFormatAmount6(int _amount)
*/

using DllSky.Extensions;
using System.Collections;
using System.Security.Cryptography;							//MD5
using System.Text;                                          //Encoding
using UnityEngine;

namespace DllSky.Utility
{
    public static class UtilityBase
    {
        //------------------------------------------------
        //Вызов вибрации
        /// <summary>
        /// Сопрограмма вибрации.
        /// _count - кол-во/продолжительность, _pause - пауза между вибрацией, _bVibrateEnable - настройка в конфиге.
        /// </summary>	
        public static IEnumerator VibrateCoroutine(int _count, float _pause = 0.7f, bool _bVibrateEnable = true)
        {
            if (!_bVibrateEnable)
                yield break;

            //#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
#if UNITY_ANDROID
            for (int i = 0; i < _count; i++)
            {
                Handheld.Vibrate();
                yield return new WaitForSeconds(_pause);
            }            
#endif
            yield return null;
        }
        //------------------------------------------------
        //MD5 текста
        /// <summary>
        /// Вычисляет md5. Кириллица корректна для UTF8.
        /// </summary>
        public static string GetMD5(string _string)
        {
            string result = "";
            byte[] hash = Encoding.UTF8.GetBytes(_string);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);

            foreach (var b in hashenc)
                result += b.ToString("x2");

            return result;
        }
        //------------------------------------------------
        //String to Color
        /// <summary>
        /// Конвертирует строку в цвет (БЕЗ АЛЬФА-КАНАЛА!)
        /// </summary>
        /// <param name="_hex"></param>
        /// <returns></returns>
        public static Color ColorStringHexToColorRGB(string _hex)
        {
            byte r = byte.Parse(_hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(_hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(_hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = 255;
            return new Color32(r, g, b, a);
        }
        //------------------------------------------------
        //Formating amount
        /// <summary>
        /// Конвертирует количество в 1К
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string GetStringFormatAmount3(int _amount)
        {
            string result = ToStringFormatAmount(_amount, 3);
            return result;
        }

        /// <summary>
        /// Конвертирует количество в 100К
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string GetStringFormatAmount5(int _amount)
        {
            string result = ToStringFormatAmount(_amount, 5);
            return result;
        }

        /// <summary>
        /// Конвертирует количество в 1КК
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string GetStringFormatAmount6(int _amount)
        {
            string result = ToStringFormatAmount(_amount, 6);
            return result;
        }

        private static string ToStringFormatAmount(int _amount, int _format)
        {
            int divider = Mathf.Pow(10,_format).ToInt();
            int amount = _amount;
            string result = "";

            if (amount >= divider)
            {
                do
                {
                    result += "K";
                    amount /= 1000;
                }
                while (amount >= divider);

                result = amount.ToString() + result;
            }
            else
                result = _amount.ToString();

            return result;
        }
        //------------------------------------------------
    }

    public static class ResourcesManager
    {
        //------------------------------------------------
        //Загрузка префабов
        public static GameObject LoadPrefab(string _path, string _name)
        {
            return Resources.Load<GameObject>(_path + _name);
        }
        //------------------------------------------------
        public static T Load<T>(string _path, string _name) where T : Object
        {
            return Resources.Load<T>(_path + _name);
        }
        //------------------------------------------------
    }
}
