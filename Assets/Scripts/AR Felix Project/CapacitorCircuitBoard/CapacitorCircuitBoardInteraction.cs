using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CapacitorCircuitBoardInteraction : MonoBehaviour
{
    ARInteraction arInteraction;
    CapacitorCircuitBoard capacitorBoard;
    GameObject indicatorObj;

    [SerializeField] GameObject dieselGenerator;
    [SerializeField] GameObject voltageCasingObj;
    [SerializeField] GameObject boardBGObj;

    bool hasOpenedCasing;
    bool hasViewedCapacitorBoard;

    // Start is called before the first frame update
    void Start()
    {
        // look at camera...
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);

        arInteraction = GameObject.FindGameObjectWithTag("ARInteraction").GetComponent<ARInteraction>();
        capacitorBoard = GameObject.FindGameObjectWithTag("CapacitorCircuitBoard").GetComponent<CapacitorCircuitBoard>();
    }

    void Update()
    {
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
            StartCoroutine(OpenCasing(true));
        }
        else if(!hasViewedCapacitorBoard)
        {
            hasViewedCapacitorBoard = true;
            StartCoroutine(ViewCapacitorBoard());
        }
    }

    IEnumerator OpenCasing(bool _open)
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
        //arInteraction.SetCanRotateMainObject(false);

        //dieselGenerator.transform.SetParent(Camera.main.transform);

        //if (dieselGenerator.transform.localPosition != Vector3.zero)
        //{
        //    dieselGenerator.transform.DOLocalRotate(new Vector3(0, 0, 0), .6f);
        //    yield return new WaitForSeconds(.6f);
        //}

        //ARMainModel mainModel = GameObject.FindGameObjectWithTag("ARMainModel").GetComponent<ARMainModel>();
        //mainModel.SetLookAtCamera(true);

        yield return new WaitForSeconds(1);

        capacitorBoard.SetCanStartAnalyseBoard(true);

    }
}
