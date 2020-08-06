using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARInteraction : MonoBehaviour
{
    // Test Object
    [SerializeField] GameObject dieselGenerator;            // The 3D model.

    [SerializeField] GameObject interactScrollViewPanel;    // Generator parts in scroll view list.

    [SerializeField] Button[] partsButtons;                 // Generator parts buttons in scroll view.

    [SerializeField] Text[] partsTexts;

    [SerializeField] GameObject[] partIndicators;           // Indicator showed on the diesel generator.

    float rotationSpeedFactor = 0.4f;     
    GameObject mainARObject;            // The 3D model.
    Touch touch;                        

    // Start is called before the first frame update
    void Start()
    {
        // Test script. Comment this one when it's not being used
        if (dieselGenerator)
        {
            mainARObject = dieselGenerator;
            interactScrollViewPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainARObject)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    touch = Input.GetTouch(0);

                    var rotationY = Quaternion.Euler(0, -touch.deltaPosition.x * rotationSpeedFactor, 0);
                    mainARObject.transform.rotation = rotationY * mainARObject.transform.rotation;
                }
            }
        }
    }

    public void UpdateARDetailReady(GameObject _mainARObject)
    {
        mainARObject = _mainARObject;
        interactScrollViewPanel.SetActive(true);
    }

    // Called from button Inspector - OnClick listener
    public void OnPartButtonClick(int index)
    {
        // Reset all the selection color
        foreach(Text text in partsTexts)
        {
            text.color = Color.white;
        }

        partsTexts[index].color = Color.green;
    }
}
