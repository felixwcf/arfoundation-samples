using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARInteraction : MonoBehaviour
{
    enum DieselGenerator_Part_Type
    {
        LCD_Panel = 0,
        Main_Volt_Circ_Board,
        Generator_Fans,
        BU_Power_Supp_Unit,
        Capacitor_Circ_Board,
        Logic_Board,
        Lithium_Fuel_Cells
    }

    // Test Object
    [SerializeField] GameObject dieselGenerator;            // The 3D model.

    [SerializeField] GameObject interactScrollView;    // Generator parts in scroll view list.

    [SerializeField] Button menuDropDownButton;                 // Generator parts buttons in scroll view.

    [SerializeField] Button[] partsButtons;                 // Generator parts buttons in scroll view.

    [SerializeField] Text[] partsTexts;

    [SerializeField] GameObject[] partIndicators;           // Indicator showed on the diesel generator.

    float rotationSpeedFactor = 0.4f;     
    GameObject mainARObject;            // The 3D model.
    Touch touch;

    Sprite sprite_dropDown;
    Sprite sprite_dropUp;

    // Start is called before the first frame update
    void Start()
    {
        sprite_dropDown = Resources.Load<Sprite>("drop-down-menu");
        sprite_dropUp = Resources.Load<Sprite>("drop-up-menu");

        // Test script. Comment this one when it's not being used
        if (dieselGenerator)
        {
            mainARObject = dieselGenerator;
            interactScrollView.SetActive(true);
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
        interactScrollView.SetActive(true);
    }

    // Called from button Inspector - OnClick listener
    public void OnPartButtonClick(int index)
    {
        // Reset all the selection color
        foreach(Text text in partsTexts)
        {
            text.color = Color.white;
        }

        // Show selected text in green color
        partsTexts[index].color = Color.green;

        // Reset all part indicator
        foreach (GameObject indicator in partIndicators)
        {
            indicator.SetActive(false);
        }

        // Play transistion animation to show the particular part
        StartCoroutine(ShowPartWithAnimation(index));
    }

    bool isMenuDropDown = true;
    public void OnDropDownButtonClick()
    {
        isMenuDropDown = !isMenuDropDown;

        menuDropDownButton.image.sprite = isMenuDropDown ? sprite_dropUp : sprite_dropDown;

        interactScrollView.SetActive(isMenuDropDown);
    }

    IEnumerator ShowPartWithAnimation(int index)
    {
        switch ((DieselGenerator_Part_Type)index)
        {
            case DieselGenerator_Part_Type.LCD_Panel:
                break;
            case DieselGenerator_Part_Type.Main_Volt_Circ_Board:
                break;
            case DieselGenerator_Part_Type.Generator_Fans:
                break;
            case DieselGenerator_Part_Type.BU_Power_Supp_Unit:
                break;
            case DieselGenerator_Part_Type.Capacitor_Circ_Board:
                break;
            case DieselGenerator_Part_Type.Logic_Board:
                break;
            case DieselGenerator_Part_Type.Lithium_Fuel_Cells:
                break;
        }

        yield return new WaitForSeconds(1);

        // Show particular part indicator
        partIndicators[index].SetActive(true);
    }

}
