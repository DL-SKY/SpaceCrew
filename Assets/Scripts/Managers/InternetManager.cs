using DllSky.Patterns;
using System;
using System.Collections;
using UnityEngine;

public class InternetManager : Singleton<InternetManager>
{
    #region Events   
    public event Action<bool, float> OnPingComplete;
    #endregion

    #region Vars
    public bool autoStart = true;               //Флаг автозапуска Ping при старте объекта
    public bool autoPing = true;                //Флаг автоповторения Ping
    public float timerAutoPing = 30.0f;         //(sec) интервал между автоповторами Ping - сек.
    public float maxPingWaitingTime = 10.0f;    //(sec) макс. время ожидание ответа - сек.
    public string address = "8.8.8.8";          //(default - Google DNS) Адрес для проверки Ping
    #endregion

    #region Get/Set
    [SerializeField]                            //Для удобства контроля результата в Редакторе
    private bool lastPingResult = false;        //Результат последнего запуска Ping
    public bool LastPingResult
    {
        get { return lastPingResult; }
    }

    [SerializeField]                            //Для удобства контроля результата в Редакторе
    private float lastPingTime = -1.0f;         //(msec) Время ответа последнего Ping - мсек.
    public float LastPingTime
    {
        get { return lastPingTime; }
    }
    #endregion

    #region Unity methods
    private void Start()
    {
        if (autoStart)
        {
            Ping();
        }
    }
    #endregion

    #region Public methods
    public void Ping()
    {
        StopAllCoroutines();
        StartCoroutine(RequestPing());
    }
    #endregion

    #region Coroutines
    private IEnumerator RequestPing()
    {
        if (string.IsNullOrEmpty(address))
            address = "8.8.8.8";

        float pingTime = 0.0f;
        Ping ping = new Ping(address);        

        while (!ping.isDone)
        {
            yield return null;
            pingTime += Time.deltaTime;
            if (pingTime >= maxPingWaitingTime)
                break;
        }

        lastPingResult = ping.isDone;
        lastPingTime = ping.time;

        if (OnPingComplete != null)
            OnPingComplete.Invoke(LastPingResult, LastPingTime);

        ping.DestroyPing();

        //Петля
        if (autoPing)
        {
            yield return new WaitForSeconds(timerAutoPing);
            Ping();
        }
    }
    #endregion
}
