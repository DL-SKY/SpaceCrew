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
        
    }
    
    /*public SpaceshipData(string _model)
    {

    }*/
    #endregion

    #region Private methods
    #endregion
}
