using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AREnums;

public class ScrewInteraction : MonoBehaviour
{
    [SerializeField] ScrewType screwType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        Debug.Log("tap the screw...");
    }
}
