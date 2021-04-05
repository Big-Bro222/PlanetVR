using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using DG.Tweening;
public class PrefabCreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject newPrefab;
    public bool grabble;
    GameObject shield;
    [SerializeField] GameObject PlanetPrefab;


    private float radius;
    private Vector3 axis;
    private Vector3 planetStartPoint;
    

    public bool instantiatePhase;
    void Start()
    {
        grabble = true;
        shield = UIController.Instance.sheild;
        //instantiatePhase = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGrabBegin()
    {
        if (grabble)
        {
            if (UIController.Instance.currentState == UIController.UIstate.General)
            {
                newPrefab = Instantiate(PlanetPrefab,transform.parent);
                newPrefab.transform.position = transform.position;
                newPrefab.transform.localScale = transform.localScale;
                newPrefab.GetComponent<OVRGrabbable>().enabled = false;
                newPrefab.GetComponent<PrefabCreator>().PlanetPrefab = PlanetPrefab;

                


                transform.parent = UIController.Instance.OrbitFolder.transform;
                transform.localScale *= 1.5f;
                transform.GetComponent<TrailRenderer>().enabled = false;
                UIController.Instance.EnterUIState(UIController.UIstate.Selecting);
                UIController.Instance.currentFocusPlanet = gameObject;
            }
        }



    }

    private void OnMouseDown()
    {
        OnGrabBegin();
        Invoke("OnGrabEnd", 4f);
    }

    //private void OnMouseUp()
    //{
    //    if (instantiatePhase)
    //    {
    //        OnGrabEnd();
    //        instantiatePhase = false;
    //    }
    //    else
    //    {

    //    }
    //}

    public void OnGrabEnd()
    {

        if (grabble)
        {
                newPrefab.GetComponent<OVRGrabbable>().enabled = true;

            GeneratePlanetMovementPara();

                DOTween.Sequence()
                    .Append(transform.DOMove(UIController.Instance.ReviewPoint.transform.position, 2f).SetEase(Ease.InOutCubic))
                    .Append(transform.DOMove(planetStartPoint, 3f).SetEase(Ease.InOutCubic))
                    .Join(transform.DOScale(1.0f, 3f).SetEase(Ease.InOutCubic))
                    .OnComplete(()=> {
                        UIController.Instance.EnterUIState(UIController.UIstate.General);
                        UIController.Instance.currentFocusPlanet = null;
                        gameObject.GetComponent<CubeDebugger>().enabled = true;
                        grabble = false;
                        transform.GetComponent<TrailRenderer>().enabled = true;
                        transform.GetComponent<PlanetMovement>().Ismoveable = true;
                    });




        }

    }
    private void GeneratePlanetMovementPara()
    {
        GameObject orbitCenter = UIController.Instance.OrbitCenter;
        radius = Random.Range(0.2f, 1.0f);
        axis = new Vector3(0, Random.Range(0.5f, 1.0f), Random.Range(-0.5f, 0.5f));
        transform.GetComponent<PlanetMovement>().Radius = radius;
        transform.GetComponent<PlanetMovement>().RotationWorldAxis = orbitCenter.transform.TransformDirection(axis);
        Vector3 localDirection = Vector3.Cross(axis, orbitCenter.transform.right);
        planetStartPoint = orbitCenter.transform.position + radius * transform.TransformDirection(localDirection).normalized;
        transform.GetComponent<PlanetMovement>().StartPoint = planetStartPoint;

    }

}
