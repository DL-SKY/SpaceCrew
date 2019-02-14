using System.Collections.Generic;
using UnityEngine;

public class MainEnginesRendererController : MonoBehaviour, IUpdateRenderer
{
    #region Variables
    [Range(0, 1)]
    public float maxTime = 0.1f;
    public List<TrailRenderer> trails;
    #endregion

    #region Public methods
    public void UpdateRenderer(SpaceshipMetadata _meta)
    {
        var speedNormalize = _meta.GetSpeedCurrentNormalize();
        var time = Mathf.Lerp(0.0f, maxTime, speedNormalize);

        //foreach (var trail in trails)
            //trail.time = time;
    }
    #endregion
}
