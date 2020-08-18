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
    [SerializeField] GameObject voltCasingFrontObj;
    [SerializeField] GameObject voltCasingBackObj;

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
            ViewCapacitorBoard();
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

        Material _volt_case_out_mat = voltCasingFrontObj.GetComponent<MeshRenderer>().material;
        Material _volt_case_in_mat = voltCasingBackObj.GetComponent<MeshRenderer>().material;
        ARSingleton.Instance.ToFadeMode(_volt_case_out_mat);
        ARSingleton.Instance.ToFadeMode(_volt_case_in_mat);
        voltCasingFrontObj.GetComponent<MeshRenderer>().material.DOFade(0, 2);
        voltCasingBackObj.GetComponent<MeshRenderer>().material.DOFade(0, 2);

        //ToFadeMode(_generator_mat);
    }

    void ViewCapacitorBoard()
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


        capacitorBoard.SetCanStartAnalyseBoard(true);
    }
}
