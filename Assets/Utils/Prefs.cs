using System;
using UnityEngine;

public static class Prefs {

    public static string GetCryptoString(string key, string defaultValue = default(string)) {
        var result = PlayerPrefs.GetString(Crypto.Encrypt(key));
        return string.IsNullOrEmpty(result) ? defaultValue : Crypto.Decrypt(result);
    }

    public static void SetCryptoString(string key, string value) {
        PlayerPrefs.SetString(Crypto.Encrypt(key), Crypto.Encrypt(value));
    }

    public static string GetString(string key, string defaultValue = default(string)) {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static void SetString(string key, string value) {
        PlayerPrefs.SetString(key, value);
    }

    public static int GetInt(string key, int defaultValue = default(int)) {
        return int.Parse(GetString(key, defaultValue.ToString()));
    }

    public static void SetInt(string key, int value) {
        SetString(key, value.ToString());
    }

    public static float GetFloat(string key, float defaultValue = default(float)) {
        return float.Parse(GetString(key, defaultValue.ToString()));
    }

    public static void SetFloat(string key, float value) {
        SetString(key, value.ToString());
    }

    public static bool GetBool(string key, bool defaultValue = default(bool)) {
        return GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    public static void SetBool(string key, bool value) {
        SetInt(key, value ? 1 : 0);
    }

    public static DateTime GetDate(string key, DateTime defaultValue = default(DateTime)) {
        var result = GetString(key, defaultValue.ToBinary().ToString());
        return DateTime.FromBinary(Convert.ToInt64(result));
    }

    public static void SetDate(string key, DateTime value) {
        SetString(key, value.ToBinary().ToString());
    }

    public static void save() {
        PlayerPrefs.Save();
    }

}