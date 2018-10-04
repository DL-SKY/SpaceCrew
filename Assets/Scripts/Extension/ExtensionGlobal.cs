using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            Debug.Log("[GLOBAL.SETTINGS] File not found. Apply default settings");
            Debug.Log("[GLOBAL.SETTINGS] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);

            return settings;
        }
    }

    public static void SaveSettings(this GameSettings _gs)
    {
        var startTime = DateTime.UtcNow;
        string settingsPath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_SETTINGS);
        Debug.Log("[GLOBAL.SETTINGS] Starting save settings: " + settingsPath);
        
        string json = JsonUtility.ToJson((GameSettings)_gs, true);

        if (!File.Exists(settingsPath + ".json"))
            File.Create(settingsPath + ".json").Dispose();

        File.WriteAllText(settingsPath + ".json", json);

        Debug.Log("[GLOBAL.SETTINGS] Save settings complete");
        Debug.Log("[GLOBAL.SETTINGS] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);
    }

    public static void ApplyDefaultSettings(this GameSettings _gs)
    {
        var settingsConfig = Global.Instance.CONFIGS.settings;

        _gs.version = settingsConfig.Find(x => x.id == "version").value;
        _gs.language = _gs.GetCurrentSystemLanguage();//settingsConfig.Find(x => x.id == "language").value;
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
    public static Profile LoadProfile()
    {
        var startTime = DateTime.UtcNow;
        string profilePath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_PROFILE);
        Debug.Log("[GLOBAL.PROFILE] Starting load profile: " + profilePath);        

        if (File.Exists(profilePath + ".json"))
        {
            string json = File.ReadAllText(profilePath + ".json");

            Debug.Log("[GLOBAL.PROFILE] Load profile complete");
            Debug.Log("[GLOBAL.PROFILE] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);

            return JsonUtility.FromJson<Profile>(json);
        }
        else
        {
            Profile profile = new Profile();
            //profile.ApplyDefaultSettings();

            Debug.Log("[GLOBAL.PROFILE] File not found. Apply default profile");
            Debug.Log("[GLOBAL.PROFILE] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);

            return profile;
        }
    }

    public static void SaveProfile(this Profile _pr)
    {
        var startTime = DateTime.UtcNow;
        string profilePath = Path.Combine(Application.persistentDataPath, ConstantsResourcesPath.FILE_PROFILE);
        Debug.Log("[GLOBAL.PROFILE] Starting save profile: " + profilePath);
        
        string json = JsonUtility.ToJson((Profile)_pr, true);

        if (!File.Exists(profilePath + ".json"))
            File.Create(profilePath + ".json").Dispose();

        File.WriteAllText(profilePath + ".json", json);

        Debug.Log("[GLOBAL.PROFILE] Save profile complete");
        Debug.Log("[GLOBAL.PROFILE] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);
    }

    public static int GetItem(this Profile _pr, string _id)
    {
        int result = 0;

        var item = _pr.items.Find(x => x.id == _id);
        if (item != null)
            result = item.amount;

        return result;
    }

    public static void AddItem(this Profile _pr, string _id, int _amount)
    {
        Debug.Log("[GLOBAL.PROFILE] <color=#FFD800>Add Item:</color> " + _id + " (" + _amount + ")");

        var item = _pr.items.Find(x => x.id == _id);
        if (item != null)
            item.amount += _amount;
        else
            _pr.items.Add(new ProfileItem(_id, _amount));
    }
    #endregion

    #region Config
    #endregion
}
