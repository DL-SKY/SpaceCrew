using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    #region Variables
    public string model;
    public string material;

    [Header("Main Renderer")]
    public MeshFilter mainFilter;
    public MeshRenderer mainRenderer;

    [Header("Camera")]
    public CameraSpaceship cameraPlace;

    private SpaceshipData data;
    #endregion

    #region Unity methods
    private void Start()
    {
        InitializeSpaceship();
    }
    #endregion

    #region Public methods
    public void InitializeSpaceship()
    {
        data = Global.Instance.PROFILE.spaceships.Find(x => x.model == model);
        if (data == null)
        {
            //Debug.LogWarning("<color=#FF0000>[SpaceshipController] _data is null!</color>");
            //model = "ERROR";
            //return;

            data = new SpaceshipData()
            {
                model = "mk6",
                material = "Default",
            };
        }

        model = data.model;
        material = data.material;

        //LoadedMainMesh();
        LoadedMainMaterial();
    }
    #endregion

    #region Private methods
    /*private void LoadedMainMesh()
    {
        Mesh mesh = Resources.Load(ConstantsResourcesPath.MODELS_SPACESHIPS + model, typeof(Mesh)) as Mesh;
        mainFilter.mesh = mesh;
    }*/

    private void LoadedMainMaterial()
    {
        Material mat = Resources.Load(ConstantsResourcesPath.MATERIALS_SPACESHIPS + model + "/" + material, typeof(Material)) as Material;
        mainRenderer.material = mat;
    }
    #endregion

    #region Coroutines
    #endregion
}
