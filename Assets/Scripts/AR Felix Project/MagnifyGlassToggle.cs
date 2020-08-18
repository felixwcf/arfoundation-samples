using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnifyGlassToggle : MonoBehaviour
{
    [SerializeField] MagnifyGlass magnifyGlass;
    [SerializeField] Text buttonText;

    GameObject magnifyCameraObject;
    bool hasMagnifyCam;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = "Magnify Glass: " + (isOn ? "ON" : "OFF");
    }

    private void Update()
    {
        if(!hasMagnifyCam)
        {
            if (GameObject.Find("MagnifyCamera"))
            {
                hasMagnifyCam = true;
                magnifyCameraObject = GameObject.Find("MagnifyCamera");
                magnifyCameraObject.SetActive(false);
            }
        }
    }

    public void toggleButtonDidTapped()
    {
        isOn = !isOn;
        magnifyCameraObject.SetActive(isOn);
        buttonText.text = "Magnify Glass: " + (isOn?"ON" : "OFF");
    }
}
