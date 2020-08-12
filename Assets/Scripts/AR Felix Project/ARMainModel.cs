using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMainModel : MonoBehaviour
{
    [SerializeField] GameObject dieselGenerator;
    [SerializeField] GameObject voltageCasingObj;
    [SerializeField] GameObject boardBGObj;

    bool canLookAtCam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canLookAtCam)
        {
            Vector3 relativePos = Camera.main.transform.position - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(-relativePos, Vector3.up); // Vector3.up
            transform.rotation = rotation;
        }
    }

    public void SetLookAtCamera(bool _look)
    {
        canLookAtCam = _look;
    }

    // Temporary fade away
    public void dimissObject()
    {

    }
}
