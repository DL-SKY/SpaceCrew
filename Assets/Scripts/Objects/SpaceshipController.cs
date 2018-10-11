using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    #region Variables
    public string model;
    public string material;

    [Header("Targets")]
    public Transform targetMove;

    [Header("Main Renderer")]
    public MeshFilter mainFilter;
    public MeshRenderer mainRenderer;

    [Header("Camera")]
    public SpaceshipCameraPlace cameraPlace;

    private SpaceshipData data;
    private Rigidbody rb;
    #endregion

    #region Unity methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

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
            Global.Instance.PROFILE.spaceships.Add(data);
        }

        material = data.material;

        //LoadedMainMesh();
        LoadedMainMaterial();
    }

    public void SetTargetMove(Transform _target)
    {
        StopAllCoroutines();

        targetMove = _target;
        StartCoroutine(ToMove());
    }

    public void ClearTargetMove()
    {
        targetMove = null;
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
    private IEnumerator ToMove()
    {
        //TODO
        var speedMove = 5.0f;

        while (targetMove)
        {
            yield return ToRotate(targetMove);
            //transform.LookAt(targetMove);

            if (transform.position == targetMove.position)
            {
                targetMove = null;
            }
            else
            {
                //transform.position.
                //transform.Translate(Vector3.forward * speedMove * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, targetMove.position, speedMove * Time.deltaTime);
            }

            //Debug.Log("dist: " + Vector3.Distance(transform.position, targetMove.position));

            yield return null;
        }
    }

    private IEnumerator ToRotate(Transform _target)
    {
        //TODO: speed rotate
        var speedRotation = 1.0f;

        var targetRot = _target.position - transform.position;
        var quatToTarget = Quaternion.LookRotation(targetRot);
    
        while ((_target.position - transform.position) != Vector3.zero && quatToTarget != transform.rotation)
        {
            
            var step = speedRotation * Time.deltaTime;
            var newRot = Vector3.RotateTowards(transform.forward, targetRot, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newRot);

            yield return null;

            targetRot = _target.position - transform.position;
            quatToTarget = Quaternion.LookRotation(targetRot);
        }

        Debug.Log("Rotation DONE");
    }
    #endregion
}
