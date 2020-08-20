using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ARInteraction : MonoBehaviour
{
    [SerializeField] Text debugText;

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

    public static bool canRotateMainObject;

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

        SetCanRotateMainObject(true);

        // Test script. Comment this one when it's not being used
        if (dieselGenerator.activeSelf)
        {
            UpdateARDetailReady(dieselGenerator);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Swipe and rotate object. Shouldn't be able to do it on UX perspective
        //if (mainARObject && canRotateMainObject)
        //{
        //    if (Input.touchCount > 0)
        //    {
        //        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //        {
        //            touch = Input.GetTouch(0);

        //            var rotationY = Quaternion.Euler(0, -touch.deltaPosition.x * rotationSpeedFactor, 0);
        //            mainARObject.transform.rotation = rotationY * mainARObject.transform.rotation;
        //        }
        //    }
        //}
    }

    public void UpdateARDetailReady(GameObject _mainARObject)
    {
        mainARObject = _mainARObject;

        // Assigning indicators children inside the generator into here 
        GameObject indicatorObjects = null;
        foreach (Transform child in mainARObject.transform)
        {
            if (child.gameObject.name.Equals(ARTapToPlaceObject.MODEL_PARTS_INDICATORS_PARENT_OBJECT_NAME))
            {
                indicatorObjects = child.gameObject;
            }
        }

        debugText.text = "partindicators count:" + indicatorObjects.transform.childCount;
        partIndicators = new GameObject[indicatorObjects.transform.childCount];
        for (int i=0; i<partIndicators.Length;i++)
        {
            debugText.text = "-->" + indicatorObjects.transform.GetChild(i).gameObject.name;
            partIndicators[i] = indicatorObjects.transform.GetChild(i).gameObject;
        }

        debugText.text = "partindicators count 2:" + partIndicators[0].name;

        interactScrollView.SetActive(true);
        menuDropDownButton.gameObject.SetActive(true);
    }

    // Called from button Inspector - OnClick listener
    int selectedIndicator;
    public void OnPartButtonClick(int index)
    {
        // Reset all the selection color
        foreach(Text text in partsTexts)
        {
            text.color = Color.white;
        }

        // Show selected text in green color
        partsTexts[index].color = Color.green;
        selectedIndicator = index;

        // Reset all part indicator
        foreach (GameObject indicator in partIndicators)
        {
            indicator.SetActive(false);
        }

        // Play transistion animation to show the particular part
        StartCoroutine(ShowPartWithAnimation(index));
    }

    public void SetCanShowPartsDropDownList(bool canShow)
    {
        interactScrollView.SetActive(canShow);
        menuDropDownButton.gameObject.SetActive(canShow);

        SetShowSelectedIndicator(canShow);
    }

    public void SetCanRotateMainObject(bool canRotate)
    {
        canRotateMainObject = canRotate;
    }

    public void SetShowSelectedIndicator(bool show)
    {
        partIndicators[selectedIndicator].SetActive(show);       
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
                mainARObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                break;
            case DieselGenerator_Part_Type.Main_Volt_Circ_Board:
                mainARObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                break;
            case DieselGenerator_Part_Type.Generator_Fans:
                mainARObject.transform.DOLocalRotate(new Vector3(0, -180, 0), 1);
                break;
            case DieselGenerator_Part_Type.BU_Power_Supp_Unit:
                mainARObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
                break;
            case DieselGenerator_Part_Type.Capacitor_Circ_Board:
                mainARObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                break;
            case DieselGenerator_Part_Type.Logic_Board:
                mainARObject.transform.DOLocalRotate(new Vector3(0, -180, 0), 1);
                break;
            case DieselGenerator_Part_Type.Lithium_Fuel_Cells:
                mainARObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                break;
        }

        yield return new WaitForSeconds(1);

        // Show particular part indicator
        partIndicators[index].SetActive(true);
    }

}
