using DllSky.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpaceshipData
{
    #region Variables
    public string id;                                                           //Уникальный идентификатор
    public string model;                                                        //Наименование модели
    public string material;                                                     //Используемый материал
    public int mk;                                                              //Mark/Модель №/Уровень

    //public List<string> subsystems = new List<string>();                        //Список подсистем (каждая подсистема содержит перки/скиллы)
    public List<string> weapons = new List<string>();                           //Список установленного вооружения
    public List<string> slots = new List<string>();                             //Список установленного оборудования
    public List<string> items = new List<string>();                             //Список используемых предметов

    [NonSerialized]
    private SpaceshipsConfig config;
    #endregion

    #region Public methods
    public SpaceshipData()
    {
        var spaceships = Global.Instance.CONFIGS.spaceships;
        config = spaceships.Count > 0 ? spaceships[0] : null;

        if (config == null)
        {
            Debug.LogError("[SpaceshipData] Config is NULL. Create default values.");

            id = "";
            model = "mk6";
            material = "Default";
            mk = 1;

            return;
        }

        model = config.model;
        id = UtilityBase.GetMD5(model + DateTime.UtcNow.ToString());
        material = "Default";
        mk = 1;
    }

    /*public SpaceshipData(string _model)
    {

    }*/

    public int GetMkIndex()
    {
        var mkIndex = mk - 1;
        if (mkIndex < 0)
            mkIndex = 0;

        return mkIndex;
    }

    public SpaceshipsConfig GetConfig()
    {
        if (config == null)
            config = Global.Instance.CONFIGS.spaceships.Find(x => x.model == model);

        if (config == null)
            Debug.LogWarning("<color=#FF0000>[SpaceshipData] \"config\" is null!</color>");

        return config;
    }
    #endregion

    #region Private methods
    
    #endregion
}
