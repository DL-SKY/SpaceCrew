public static class ConstantsGameSettings
{
    //public static float CELL_SIZE = 1.0f;           //Сторона вокселя игрового пространства (одна игровая клетка)
    //public static float BEYOND_BORDERS = 10.0f;     //Воксели за пределами Игрового поля, по которым можно перемещаться
    //public static float RAY_COEF = 1.1f;            //Длина луча для проверки столкновения
    //public static float TIME_ANIMATION = 1.0f;      //Скорость анимаций перемещения
    //public static int BASE_EQUIPMENT_SLOTS = 3;     //Базовое кол-во слотов оснащения в корабле

    //public const float SPACETRACK_COUNT = 5.1f;            //[!]Нечетное!
    //public const float SPACETRACK_WIDTH = 2.5f;

    //public const int GAMEPLAY_DIFFICULTY_DEF = 1;          //1
    //public const float GAMEPLAY_SPEED_DEF = 1.0f;          //2.5    
    //public const float GAMEPLAY_GENERATION_TIMER = 2.5f;   //1.0
    //public static Vector2 GAMEPLAY_GENERATION_ROTATION = new Vector2(-25.0f, 25.0f);

    public const float MANEUVER_MOD_SPEED = 0.5f;       //1.0f / 2.0f;
    public const float MANEUVER_MOD_ROTATE = 0.25f;     //1.0f / 4.0f;
}

public static class ConstantsTag
{
    public const string TAG_SCREENS_CANVAS = "ScreensCanvas";
    public const string TAG_DIALOGS_CANVAS = "DialogsCanvas";

    public const string TAG_SPACESHIP = "Spaceship";
    public const string TAG_MAIN_COLLIDER = "MainCollider";
    public const string TAG_SHIELD_COLLIDER = "ShieldCollider";


    //public const string TAG_VOID = "Void";

    //public const string TAG_PLAYER = "Player";
    /*public const string TAG_MISSILE_PLAYER = "MissilePlayer";
    public const string TAG_SHOT_PLAYER = "ShotPlayer";

    public const string TAG_SPACE_OBJECT = "SpaceObject";
    public const string TAG_SPACESHIP = "Spaceship";
    public const string TAG_MISSILE = "Missile";
    public const string TAG_SHOT = "Shot";

    public const string TAG_EQUIPMENT = "Equipment";*/
}

public static class ConstantsResourcesPath
{
    //Path
    public const string RESOURCES = "Assets/Resources/";
    public const string CONFIGS = "Configs/";
    public const string PREFABS = "Prefabs/";
    
    //Prefabs
    public const string SPLASHSCREEN = "Prefabs/UI/SplashScreens/";
    public const string SCREENS = "Prefabs/UI/Screens/";
    public const string DIALOGS = "Prefabs/UI/Dialogs/";
    public const string ELEMENTS_UI = "Prefabs/UI/Elements/";

    public const string PREF_OBJECTS = "Prefabs/Objects/";

    //Sprites
    public const string SPRITES = "Sprites/";

    //3D meshes
    public const string MODELS = "Models/";
    public const string MODELS_SPACESHIPS = "Models/Spaceships/";

    //Materials
    public const string MATERIALS = "Materials/";
    public const string MATERIALS_SPACESHIPS = "Materials/Spaceships/";

    //File name
    public const string FILE_CONFIG = "Config";
    public const string FILE_SETTINGS = "Settings";
    public const string FILE_PROFILE = "Profile";
}

public static class ConstantsPrefabName
{
    //Markers
    public static string MARKER_POINT = "MarkerPoint";

    //Radar
    public const string RADAR_SPACESHIP = "RadarSpaceship";
    public const string RADAR_POINT = "RadarPoint";
}

public static class ConstantsScene
{
    public const string MAIN_SCENE = "Main";
    public const string MAIN_MENU = "MainMenu";

    public const string GAME_SCENE = "Game";
}

public static class ConstantsScreen
{
    public const string MAIN_MENU = "MainMenuScreen";

    public const string DEADSPACE = "DeadspaceScreen";

    public const string GAME_SCREEN = "GameScreen";
}

public static class ConstantsDialog
{
    public const string SETTINGS = "SettingsDialog";
    public const string LOGS = "LogsDialog";
}

public static class ConstantsColor
{
    public const string DARK_AMBER = "dark_amber";
    public const string TIZIANO = "tiziano";
    public const string DEAD_INDIGO = "dead_indigo";
    public const string AZURE_GRAY = "azure_gray";
    public const string AZURE_KRAOLA = "azure_kraola";
    public const string LIGHT_KRAOLA = "light_kraola";
}

public static class ConstantsResourcesType
{

}

public static class ConstantsResourcesSubtype
{

}

public static class ConstantsSkill
{

}

public static class ConstantsSpaceshipSettings
{
    //Скорость (нормализованная 0.0-1.0)
    public const float SPEED_STOP = 0.0f;
    public const float SPEED_DOCK = 0.1f;               //Стыковочная скорость
    //public const float SPEED_TURN = 0.3f;               //Поворот
    public const float SPEED_HALF = 0.5f;
    public const float SPEED_COMMON = 0.6f;             //Активное вооружение    
    public const float SPEED_FULL = 1.0f;
    //public const float SPEED_BURN = 1.25f;

    //...
}

public static class ConstantsLanguage
{
    public const string RUSSIAN = "rus";
    public const string ENGLISH = "eng";
}

public static class ConstantsEmail
{
    public const string ADDRESS = "alex.dllsky@gmail.com";
    public const string SUBJECT_LOGS = "Game Logs";
}

/*public static class ConstantsSubsystems
{
    public const string MAIN = "main";
    public const string SHIELD = "shield";
    public const string NAVIGATION = "navigation";
    public const string WEAPON = "weapon";
    public const string ENERGY = "energy";
}

public static class ConstantsParameters
{
    public const string ARMOR = "armor";
    public const string CARGO = "cargo";

    public const string SHIELD = "shield";
    public const string SHIELD_RECOVERY = "shieldRecovery";

    public const string SPEED = "speed";
    public const string SPEED_RECOVERY = "maneuver";

    public const string ACCURACY = "accuracy";
    public const string CRITICAL = "critical";

    public const string ENERGY = "energy";
    public const string ENERGY_RECOVERY = "energyRecovery";
}*/

#region ENUM
public enum EnumTargetType
{
    None,
    ToPoint,
    ToFollow,
    ToOrbit,
    ToObject,
    ToEnemy,
}

public enum EnumSubsystems
{
    main,
    shield,
    navigation,
    weapon,
    energy,
}

public enum EnumParameters
{
    //Параметры подсистем корабля
    armor,
    cargo,
    shield,
    shieldRecovery,
    speed,
    maneuver,
    targets,
    energy,

    //Параметры оснащения
    shieldDamage,
    armorDamage,
    optimalDistance,
    accuracy,
    critical,
}

public enum EnumPointType
{
    point,
    enemy,
}

//Тип объекта
/*public enum EnumSpaceObject
{
    Void,
    SpaceObject,

    Player,
    MissilePlayer,
    ShotPlayer,

    Spaceship,
    Missile,
    Shot,

    Equipment,
    Item,
}

//Тип оснащения
public enum EnumEquipmentType
{
    TestMissileLauncher,
}*/
#endregion



