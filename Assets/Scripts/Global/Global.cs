using DllSky.Managers;
using DllSky.Patterns;
using DllSky.Utility;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Global : Singleton<Global>
{
    #region Variables
    public bool isComplete = false;
    public Configs CONFIGS;
    public GameSettings SETTINGS;
    public Profile PROFILE;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void Initialize()
    {
        Debug.Log("<color=#FFD800>[GLOBAL] Start GLOBAL initialize</color>");
        InitConfigs();
        isComplete = true;
        Debug.Log("<color=#FFD800>[GLOBAL] Complete GLOBAL initialize</color>");
    }
    #endregion

    #region Private methods
    private void InitConfigs()
    {
        //Загрузка файла конфига
        var startTime = DateTime.UtcNow;        
        string json = ResourcesManager.Load<TextAsset>(ConstantsResourcesPath.CONFIGS, ConstantsResourcesPath.FILE_CONFIG).text;
        Debug.Log("[GLOBAL.CONFIG] Start load Config.json");
        CONFIGS = JsonUtility.FromJson<Configs>(json);
        //CONFIGS.Sorting();
        Debug.Log("[GLOBAL.CONFIG] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);

        //Загрузка настроек
        //LOG - в методе
        SETTINGS = ExtensionGlobal.LoadSettings();

        //Вызов события смены языка локализации
        Debug.Log("[CONFIG] Calling the update event of the localization dictionary");
        EventManager.CallOnChangeLanguage();

        //Загрузка профиля Игрока
        //LOG - в методе
        PROFILE = ExtensionGlobal.LoadProfile();
    }
    #endregion

    #region Coroutines
    #endregion

    #region Context menu
    [ContextMenu("Check CONFIG")]
    public void CheckConfig()
    {
        var startTime = DateTime.UtcNow;        

        //Загрузка файла конфига
        Debug.Log("[CONFIG] Start load Config.json");
        string json = ResourcesManager.Load<TextAsset>(ConstantsResourcesPath.CONFIGS, ConstantsResourcesPath.FILE_CONFIG).text;
        Configs config = JsonUtility.FromJson<Configs>(json);
        //config.Sorting();

        //Проверка "Localization"
        foreach (var item in config.localization)
        {
            if (string.IsNullOrEmpty(item.rus) || string.IsNullOrEmpty(item.eng))
                Debug.LogError("[CONFIG.Localization] Null or empty: " + item.id);
        }

        

        Debug.Log("[CONFIG] Check complete");
        Debug.Log("[CONFIG] TOTAL TIME (ms): " + (DateTime.UtcNow - startTime).TotalMilliseconds);
    }

    [ContextMenu("Save SETTINGS")]
    public void SaveSettings()
    {
        SETTINGS.SaveSettings();
    }

    [ContextMenu("Delete SETTINGS")]
    public void DeleteSettings()
    {
        SETTINGS.DeleteSettings();
    }

    [ContextMenu("Save PROFILE")]
    public void SaveProfile()
    {
        PROFILE.SaveProfile();
    }

    [ContextMenu("Delete PROFILE")]
    public void DeleteProfile()
    {
        PROFILE.DeleteProfile();
    }
    #endregion
}

// ================= SETTINGS ================= \\
[Serializable]
public class GameSettings
{
    public string version;
    public string language;    
    public float volumeSound;
    public float volumeMusic;
    public bool muteSound;
    public bool muteMusic;

    public bool vibration;

    public bool console;
    public bool debug;
}

// ================= PROFILE ================= \\
[Serializable]
public class Profile
{
    public string currentShip;

    public List<SpaceshipData> spaceships = new List<SpaceshipData>();
    public List<ProfileItem> items = new List<ProfileItem>();
}

[Serializable]
public class ProfileItem
{
    public string id;
    public int amount;

    public ProfileItem(string _id, int _amount)
    {
        id = _id;
        amount = _amount;
    }
}

// ================= CONFIGS ================= \\
// Сам класс конфига + ниже классы 
[Serializable]
public class Configs
{
    //Таблицы из конфига (лучше сохранить последовательность)
    public List<SettingsConfig> settings = new List<SettingsConfig>();
    public List<LocalizationConfig> localization = new List<LocalizationConfig>();
    public List<ColorsConfig> colors = new List<ColorsConfig>();

    public List<SpaceshipsConfig> spaceships = new List<SpaceshipsConfig>();

    public List<SubsystemsConfig> subsystems = new List<SubsystemsConfig>();
    public List<ParametersConfig> parameters = new List<ParametersConfig>();

    public List<ResourcesConfig> resources = new List<ResourcesConfig>();
    public List<ItemsConfig> items = new List<ItemsConfig>();


    //public List<SpaceshipConfig> spaceships = new List<SpaceshipConfig>();
    //public List<EquipmentConfig> equipments = new List<EquipmentConfig>();
}

[Serializable]
public class SettingsConfig
{
    public string id;
    public string value;
}

[Serializable]
public class LocalizationConfig
{
    public string id;

    public string rus;
    public string eng;
}

[Serializable]
public class ColorsConfig
{
    public string id;
    public string color;
}

[Serializable]
public class SpaceshipsConfig
{
    public string id;

    public string blueprint;

    public int[] mks;

    public int[] weapons;
    public int[] slots;
    public int[] items;

    public float[] armor;
    public float[] cargo;
    public float[] shield;
    public float[] shieldRecovery;
    public float[] speed;
    public float[] maneuver;
    public float[] targets;
    public float[] energy;

    public string[] skills;
}

[Serializable]
public class SubsystemsConfig
{
    public string id;
}

[Serializable]
public class ParametersConfig
{
    public string id;
    public string subsystem;
}

[Serializable]
public class ResourcesConfig
{
    public string id;

    public string type;

    public string rarity;
    public float chance;

    public string image;
    public string icon;
}

[Serializable]
public class ItemsConfig
{
    public string id;

    public string blueprint;

    public string type;
    public string subType;

    public float energy;

    public string[] selfParameters;
    public string[] shipParameters;
    public string[] skills;
}

[Serializable]
public class BlueprintsConfig
{
    public string id;

    public string[] recipe;
}

/*
[Serializable]
public class SpaceshipConfig
{
    public string id;
    public string description;
    public float hitPoints;
    public float shieldPoints;
    public string[] equipments;
    public int slots;
}

[Serializable]
public class EquipmentConfig
{
    public string id;
    public string description;
    public string ammoPrefab;
    public string slotImage;
    public int level;
    public int[] uses;
    public float[] cooldown;
}
*/
