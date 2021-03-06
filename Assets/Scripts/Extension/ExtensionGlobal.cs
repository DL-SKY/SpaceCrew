﻿using DllSky.Managers;
using DllSky.Protection;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ExtensionGlobal
{
    #region GameSettings
    public static GameSettings LoadSettings()
    {
        var startTime = DateTime.UtcNow;
        string settingsPath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_SETTINGS);
        Debug.Log("[GLOBAL.SETTINGS] Starting load settings: " + settingsPath);        

        if (File.Exists(settingsPath + ".json"))
        {
            string json = File.ReadAllText(settingsPath + ".json");

            Debug.Log("[GLOBAL.SETTINGS] Load settings complete");
            Debug.Log("[GLOBAL.SETTINGS] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);

            return JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            GameSettings settings = new GameSettings();
            settings.ApplyDefaultSettings();

            Debug.LogWarning("<color=#FF0000>[GLOBAL.SETTINGS] File not found. Apply default settings</color>");
            Debug.Log("[GLOBAL.SETTINGS] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);

            return settings;
        }
    }

    public static void SaveSettings(this GameSettings _gs)
    {
        var startTime = DateTime.UtcNow;
        string settingsPath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_SETTINGS);
        Debug.Log("[GLOBAL.SETTINGS] Starting save settings: " + settingsPath);

        //Обновляем версию
        _gs.version = Application.version;

        //Создаем текст в формате JSON
        string json = JsonUtility.ToJson((GameSettings)_gs, true);

        if (!File.Exists(settingsPath + ".json"))
            File.Create(settingsPath + ".json").Dispose();

        File.WriteAllText(settingsPath + ".json", json);

        Debug.Log("<color=#FFD800>[GLOBAL.SETTINGS] Save settings complete</color>");
        Debug.Log("[GLOBAL.SETTINGS] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);
    }

    public static void DeleteSettings(this GameSettings _gs)
    {
        string settingsPath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_SETTINGS);

        if (File.Exists(settingsPath + ".json"))
        {
            File.Delete(settingsPath + ".json");

            Debug.LogWarning("<color=#FF0000>[GLOBAL.SETTINGS] Settings is delete!</color>");
        }
    }

    public static void ApplyDefaultSettings(this GameSettings _gs)
    {
        var settingsConfig = Global.Instance.CONFIGS.settings;

        _gs.version = Application.version;  //settingsConfig.Find(x => x.id == "version").value;
        _gs.language = _gs.GetCurrentSystemLanguage();  //settingsConfig.Find(x => x.id == "language").value;
        _gs.volumeSound = float.Parse(settingsConfig.Find(x => x.id == "volume_sound").value);
        _gs.volumeMusic = float.Parse(settingsConfig.Find(x => x.id == "volume_music").value);
        _gs.muteSound = bool.Parse(settingsConfig.Find(x => x.id == "mute_sound").value);
        _gs.muteMusic = bool.Parse(settingsConfig.Find(x => x.id == "mute_music").value);
        _gs.vibration = bool.Parse(settingsConfig.Find(x => x.id == "vibration").value);

        _gs.console = bool.Parse(settingsConfig.Find(x => x.id == "console").value);
        _gs.debug = bool.Parse(settingsConfig.Find(x => x.id == "debug").value);
    }

    public static string GetCurrentSystemLanguage(this GameSettings _gs)
    {
        var lang = Application.systemLanguage;
        string result = "";

        switch (lang)
        {
            case SystemLanguage.Russian:
                result = ConstantsLanguage.RUSSIAN;
                break;
            default:
                result = ConstantsLanguage.ENGLISH;
                break;
        }

        return result;
    }
    #endregion

    #region Profile
    public static void ApplyDefaultSettings(this Profile _pr)
    {
        _pr.Credits = 1000;
        _pr.Tokens = 10;

        _pr.spaceships = new List<SpaceshipData>();
        _pr.resources = new List<ProfileItem>();
    }

    public static Profile LoadProfile()
    {
        var encrypting = MainGameManager.Instance.usingEncryption;

        var startTime = DateTime.UtcNow;
        string profilePath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_PROFILE);
        Debug.Log("[GLOBAL.PROFILE] Starting load profile: " + profilePath);        

        if (File.Exists(profilePath + ".json"))
        {
            string json = File.ReadAllText(profilePath + ".json");

            try
            {
                //Дешифруем
                if (encrypting)
                json = SimpleEncrypting.Decode(json);

                Debug.Log("[GLOBAL.PROFILE] Load profile complete");
                Debug.Log("[GLOBAL.PROFILE] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);
            
                return JsonUtility.FromJson<Profile>(json);
            }
            catch
            {
                Debug.LogError("<color=#FF0000>[GLOBAL.PROFILE] File failed to decrypt! Profile is delete</color>");

                File.Delete(profilePath + ".json");
                return LoadProfile();
            }            
        }
        else
        {
            Profile profile = new Profile();
            profile.ApplyDefaultSettings();

            Debug.LogWarning("<color=#FF0000>[GLOBAL.PROFILE] File not found. Apply default profile</color>");
            Debug.Log("[GLOBAL.PROFILE] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);

            return profile;
        }
    }

    public static void SaveProfile(this Profile _pr)
    {
        var encrypting = MainGameManager.Instance.usingEncryption;

        var startTime = DateTime.UtcNow;
        string profilePath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_PROFILE);
        Debug.Log("[GLOBAL.PROFILE] Starting save profile: " + profilePath);
        
        string json = JsonUtility.ToJson((Profile)_pr, true);

        if (!File.Exists(profilePath + ".json"))
            File.Create(profilePath + ".json").Dispose();

        //Шифруем
        if (encrypting)
            json = SimpleEncrypting.Encode(json);

        File.WriteAllText(profilePath + ".json", json);

        Debug.Log("<color=#FFD800>[GLOBAL.PROFILE] Save profile complete</color>");
        Debug.Log("[GLOBAL.PROFILE] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);
    }

    public static void DeleteProfile(this Profile _pr)
    {
        string profilePath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_PROFILE);

        if (File.Exists(profilePath + ".json"))
        {
            File.Delete(profilePath + ".json");

            Debug.LogWarning("<color=#FF0000>[GLOBAL.PROFILE] Profile is delete!</color>");
        }
    }

    public static int GetItem(this Profile _pr, string _id)
    {
        int result = 0;

        var item = _pr.resources.Find(x => x.id == _id);
        if (item != null)
            result = item.amount;

        //Исключительная ситуация для Валюты
        if (_id == ConstantsResourcesID.CREDITS)
            result = _pr.Credits;
        else if (_id == ConstantsResourcesID.TOKENS)
            result = _pr.Tokens;

        return result;
    }

    public static void AddItem(this Profile _pr, string _id, int _amount)
    {
        Debug.Log("[GLOBAL.PROFILE] <color=#FF8800>Add Item:</color> " + _id + " (" + _amount + ")");

        var item = _pr.resources.Find(x => x.id == _id);
        if (item != null)
            item.amount += _amount;
        else
            _pr.resources.Add(new ProfileItem(_id, _amount));

        //Исключительная ситуация для Валюты
        if (_id == ConstantsResourcesID.CREDITS)
            _pr.Credits += _amount;
        else if (_id == ConstantsResourcesID.TOKENS)
            _pr.Tokens += _amount;

        //Проверка на отрицательное значение
        var currValue = _pr.GetItem(_id);
        if (currValue < 0)
            _pr.AddItem(_id, -currValue);
        else        
            EventManager.CallOnResourceUpdate(_id); //Событие на изменение ресурсов
    }
    #endregion

    #region Config
    #endregion

    #region Lists / Dictionaries / Arrays
    public static void AddOrUpdate(this Dictionary<EnumParameters, float> _dictionary, EnumParameters _key, float _value)
    {
        if (_dictionary.ContainsKey(_key))      //Ключ уже есть в словаре - обновляем значение
            _dictionary[_key] += _value;
        else                                    //Ключа в словаре нет, добавляем новый ключ
            _dictionary.Add(_key, _value);
    }
    #endregion
}
