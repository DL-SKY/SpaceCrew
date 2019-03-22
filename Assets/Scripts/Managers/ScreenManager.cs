using DllSky.Managers;
using DllSky.Patterns;
using DllSky.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : Singleton<ScreenManager>
{
    #region Variables
    public Transform parentScreens;
    public Transform parentDialogs;

    [SerializeField]
    private List<ScreenController> screens = new List<ScreenController>();
    [SerializeField]
    private List<DialogController> dialogs = new List<DialogController>();
    #endregion

    #region Unity methods
    private void Awake()
    {
        if (!parentScreens)
            parentScreens = GameObject.FindGameObjectWithTag(ConstantsTag.TAG_SCREENS_CANVAS).transform;
        if (!parentDialogs)
            parentDialogs = GameObject.FindGameObjectWithTag(ConstantsTag.TAG_DIALOGS_CANVAS).transform;
    }

    private void Start()
    {
        screens.Clear();
        dialogs.Clear();
    }

    private void Update()
    {
        //Кнопка "Назад"
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("<color=#FFD800>[INFO]</color> [ScreenManager] " + KeyCode.Escape);

            EventManager.CallOnClickEsc();

            if (dialogs.Count > 0)
                dialogs[dialogs.Count - 1].Close(false);
        }
    }
    #endregion

    #region Public methods
    public void ShowScreen(string _name, object _data = null)
    {
        //Проверка с текущим экраном
        if (screens.Count > 0 && screens[screens.Count - 1].screenName == _name)
        {
            Debug.LogWarning("[ScreenManager] Trying to re-open the screen: " + _name);
            SplashScreenManager.Instance.HideSplashScreenImmediately();
            return;
        }

        //Проверка со список открытых экранов
        foreach (var item in screens)
        {
            if (item.screenName == _name)
            {
                Debug.LogWarning("[ScreenManager] The screen \"" + _name + "\" is in the history of open screens.");
                //...
                return;
            }
        }

        var screen = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.SCREENS, _name), parentScreens).GetComponent<ScreenController>();
        screen.transform.SetAsLastSibling();
        screen.Initialize(_data);
        screen.screenName = _name;

        //Деактивируем предыдущие экраны
        foreach (var item in screens)
        {
            var itemGO = item.gameObject;

            if (itemGO.activeSelf)
                itemGO.SetActive(false);
        }

        screens.Add(screen);
        Debug.Log("<color=#FFD800>[ScreenManager] Screen loaded: " + _name + "</color>");

        /*
        var screen = ResourcesManager.Instance.InstantiatePrefab<ScreenController>(transform, ResourcesManagerPaths.UI_SCREENS, _type.ToString());
        screens.Add(screen);
        screen.Type = _type;
        screen.Initialize(_data);

        LocalizationManager.ClearCache();

        return screen;
        */
    }

    public void CloseScreen(ScreenController _screen)
    {
        screens.Remove(_screen);

        screens[screens.Count - 1].transform.SetAsLastSibling();
        screens[screens.Count - 1].gameObject.SetActive(true);
    }

    public T ShowDialog<T>(string _name) where T : DialogController
    {
        var dialog = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.DIALOGS, _name), parentDialogs).GetComponent<T>();
        dialog.transform.SetAsLastSibling();
        dialog.dialogName = _name;

        dialogs.Add(dialog);
        Debug.Log("<color=#FFD800>[ScreenManager] Dialog loaded: " + _name + "</color>");

        return dialog;
    }

    public DialogController ShowDialog(string _name)
    {
        return ShowDialog<DialogController>(_name);
    }

    public void CloseDialog(DialogController _dialog)
    {
        dialogs.Remove(_dialog);
        Debug.Log("<color=#FFD800>[ScreenManager]</color> Dialog closed: " + _dialog.dialogName);
        //Destroy(_dialog.gameObject);
        /*int index = dialogs.FindIndex(x => x.id == _dialog.id);

        Debug.Log(_dialog.id);
        Debug.Log(index);

        if (index > 0)
            dialogs.RemoveAt(index);*/
    }









    public void /*DialogController*/ ShowErrorDialog(string _text, string _techInfo = null)
    {
        /*if (screens.Count > 0)
            _text += "\n" + LocalizationManager.Get(LocalizationKeys.ERROR_INFO_SCREEN, screens[screens.Count - 1].Type.ToString());

        if (_techInfo == null)
            _techInfo = Environment.StackTrace;

        var dialog = ShowDialog<ErrorDialogController>(DialogNameConstants.ERROR, AlwaysOverlayCanvas);
        dialog.Initialize(_text, _techInfo);
        return dialog;*/
    }
    /*
    /// <summary>
    /// Creates dialog either in current screen or in plain canvas if no current screen.
    /// </summary>
    public T ShowDialog<T>(string _name) where T : DialogController
    {
        if (screens.Count > 0)
            return ShowDialog<T>(_name, screens[screens.Count - 1]);
        else
            return ShowDialog<T>(_name, this);
    }

    public T ShowDialog<T>(string _name, MonoBehaviour _parent) where T : DialogController
    {
        return ResourcesManager.Instance.InstantiatePrefab<T>(_parent.transform, ResourcesManagerPaths.UI_DIALOGS, _name);
    }

    public T ShowDialog<T>(string _name, Transform _transform) where T : DialogController
    {
        return ResourcesManager.Instance.InstantiatePrefab<T>(_transform, ResourcesManagerPaths.UI_DIALOGS, _name);
    }
    */
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    /*public IEnumerator ShowScreen(string _name)
    {
    
    }*/
    #endregion
}
