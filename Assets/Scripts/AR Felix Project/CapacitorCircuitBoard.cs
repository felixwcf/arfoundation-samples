using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Collider))] //A collider is needed to receive clicksx2
public class CapacitorCircuitBoard : MonoBehaviour
{
    [SerializeField] GameObject dieselGenerator;
    [SerializeField] GameObject voltageCasingObj;
    [SerializeField] Button btnStopInteraction;

    bool hasOpenedCasing;

    // Start is called before the first frame update
    void Start()
    {
        // look at camera...
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }

    public void StopPartInteractionButtonDidClick()
    {
        hasOpenedCasing = false;

        // TODO: reset to original state with animation
    }

    // Global Event Listener 
    private void OnMouseUp()
    {
        if(hasOpenedCasing) return;

        hasOpenedCasing = true;

        StartCoroutine(OpenCasing());
    }

    IEnumerator OpenCasing()
    {
        if (dieselGenerator.transform.position != Vector3.zero)
        {
            dieselGenerator.transform.DOLocalRotate(new Vector3(0, 0, 0), .6f);
            yield return new WaitForSeconds(1);
        }

        voltageCasingObj.transform.DOLocalRotate(new Vector3(0, 98, 0), 1);
        dieselGenerator.transform.DOLocalRotate(new Vector3(0, 0, 0), .6f);
    }
}
