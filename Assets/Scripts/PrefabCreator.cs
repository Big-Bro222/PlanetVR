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
    Sequence s1;
    Sequence s2;
    GameObject shield;
    [SerializeField] GameObject PlanetPrefab;

    public bool instantiatePhase;
    void Start()
    {
        grabble = true;
        s1 = DOTween.Sequence();
        s2 = DOTween.Sequence();
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
                UIController.Instance.EnterUIState(UIController.UIstate.Selecting);
                UIController.Instance.currentFocusPlanet = gameObject;
            }
        }



    }

    //private void OnMouseDown()
    //{
    //    OnGrabBegin();
    //    Invoke("OnGrabEnd", 4f);
    //}

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
                


                DOTween.Sequence()
                    .Append(transform.DOMove(UIController.Instance.ReviewPoint.transform.position, 2f).SetEase(Ease.InOutCubic))
                    .Append(transform.DOMove(UIController.Instance.OrbitPoint.transform.position, 3f).SetEase(Ease.InOutCubic))
                    .Join(transform.DOScale(1.0f, 3f).SetEase(Ease.InOutCubic))
                    .OnComplete(()=> {
                        UIController.Instance.EnterUIState(UIController.UIstate.General);
                        UIController.Instance.currentFocusPlanet = null;
                        gameObject.GetComponent<CubeDebugger>().enabled = true;
                        grabble = false;
                    });




        }

    }

}
