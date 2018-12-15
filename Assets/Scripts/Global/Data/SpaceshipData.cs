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

    public List<SubsystemData> subsystems = new List<SubsystemData>();          //Список подсистем (каждая подсистема содержит перки/скиллы)
    public List<ItemData> weapons = new List<ItemData>();                       //Список установленного вооружения
    public List<ItemData> slots = new List<ItemData>();                         //Список установленного оборудования
    public List<ItemData> items = new List<ItemData>();                         //Список используемых предметов
    #endregion

    #region Public methods
    public SpaceshipData()
    {
        var spaceships = Global.Instance.CONFIGS.spaceships;
        var config = spaceships.Count > 0 ? spaceships[0] : null;

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

        DefaultSubsystems();
    }

    /*public SpaceshipData(string _model)
    {

    }*/
    #endregion

    #region Private methods
    private void ClearSubsystems()
    {
        subsystems.Clear();
    }

    private void DefaultSubsystems()
    {
        ClearSubsystems();

        var configs = Global.Instance.CONFIGS.subsystems;
        foreach (var item in configs)
        {
            var newSubsystem = new SubsystemData((EnumSubsystems)Enum.Parse(typeof(EnumSubsystems), item.id));
            subsystems.Add(newSubsystem);
        }
    }
    #endregion
}
