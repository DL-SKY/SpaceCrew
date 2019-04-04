using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectsLaser : MonoBehaviour, IWeaponEffects
{
    #region Variables
    public float speed = 100.0f;

    [Space()]
    public LineRenderer line;
    public GameObject particles;
    #endregion

    #region Unity methods
    private void Awake()
    {
        HideLaser();
    }
    #endregion

    #region Public methods
    public void HideVFX()
    {
        HideLaser();
    }
    #endregion

    #region Private methods
    private void HideLaser()
    {
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);

        particles.SetActive(false);
    }
    #endregion

    #region Coroutines
    public IEnumerator PrepareVFX(float _time)
    {
        yield return new WaitForSeconds(_time * 0.5f);

        particles.SetActive(true);

        yield return new WaitForSeconds(_time * 0.5f);
    }

    public IEnumerator AttackVFX(Transform _start, Transform _end, float _time)
    {
        var T = 0.0f;

        line.SetPosition(0, _start.position);
        line.SetPosition(1, _start.position);

        while (T < _time)
        {
            line.SetPosition(0, _start.position);
            var end = Vector3.MoveTowards(line.GetPosition(1), _end.position, speed * Time.deltaTime);
            line.SetPosition(1, end);

            yield return null;

            T += Time.deltaTime;
        }

        HideLaser();
    }    
    #endregion
}
