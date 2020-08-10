using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CapacitorCircuitBoardInteraction : MonoBehaviour
{
    ARInteraction arInteraction;

    [SerializeField] GameObject dieselGenerator;
    [SerializeField] GameObject voltageCasingObj;
    [SerializeField] Button btnStopInteraction;

    bool hasOpenedCasing;
    bool hasViewedCapacitorBoard;
    bool canBoardKeepLookingAtCam;

    // Start is called before the first frame update
    void Start()
    {
        // look at camera...
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);

        arInteraction = GameObject.FindGameObjectWithTag("ARInteraction").GetComponent<ARInteraction>();
    }

    void Update()
    {
        if(canBoardKeepLookingAtCam)
        {
            Vector3 relativePos = Camera.main.transform.position - dieselGenerator.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(-relativePos, Vector3.up); // Vector3.up
            dieselGenerator.transform.rotation = Quaternion.Slerp(dieselGenerator.transform.rotation, rotation, Time.deltaTime * 1);
        }
    }

    public void StopPartInteractionButtonDidClick()
    {
        hasOpenedCasing = false;

        // TODO: reset to original state with animation
    }

    // Global Event Listener 
    private void OnMouseUp()
    {
        if (!hasOpenedCasing)
        {
            hasOpenedCasing = true;
            StartCoroutine(OpenCasing());
        }
        else if(!hasViewedCapacitorBoard)
        {
            hasViewedCapacitorBoard = true;
            StartCoroutine(ViewCapacitorBoard());
        }
    }

    IEnumerator OpenCasing()
    {
        if (dieselGenerator.transform.position != Vector3.zero)
        {
            dieselGenerator.transform.DOLocalRotate(new Vector3(0, 0, 0), .6f);
            yield return new WaitForSeconds(.6f);
        }

        voltageCasingObj.transform.DOLocalRotate(new Vector3(0, 98, 0), 1);
        dieselGenerator.transform.DOLocalRotate(new Vector3(0, 0, 0), .6f);
    }

    IEnumerator ViewCapacitorBoard()
    {
        arInteraction.SetCanRotateMainObject(false);

        dieselGenerator.transform.SetParent(Camera.main.transform);

        if (dieselGenerator.transform.localPosition != Vector3.zero)
        {
            dieselGenerator.transform.DOLocalRotate(new Vector3(0, 0, 0), .6f);
            yield return new WaitForSeconds(.6f);
        }

        //dieselGenerator.transform.DOLocalMove(new Vector3(0, 1f, -2), .6f, false);
        canBoardKeepLookingAtCam = true;

    }
}
