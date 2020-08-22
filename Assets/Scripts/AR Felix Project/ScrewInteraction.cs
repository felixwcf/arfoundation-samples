using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AREnums;

public class ScrewInteraction : MonoBehaviour
{
    [SerializeField] ScrewType screwType;

    // hardcoded, total of screws of the boad are hardcoded
    [SerializeField] int screwIndex;

    private Material unscrewIndicatorMat;
    private bool canLooseTheScrew;
    private bool hasAlreadyLoosen;

    // Start is called before the first frame update
    void Start()
    {
        unscrewIndicatorMat = Resources.Load("Screw type hex Material", typeof(Material)) as Material;
        
        NotificationCenter.DefaultCenter().AddObserver(this, "OnCorrectScrewDriverSelected");
    }

    private void OnDestroy()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "OnCorrectScrewDriverSelected");
    }

    // Update is called once per frame
    void Update()
    {
//        List<Touch> touches = InputHelper.GetTouches();

//        // For Editor Keyboard

//#if UNITY_EDITOR
//        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit Hit;

//        if (Input.GetMouseButtonDown(0))
//        {
//            if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("InteractableScrew"))
//            {
//                if (canLooseTheScrew)
//                {
//                    LooseScrew();
//                }
//            }
//        }
//#endif

//        // For Mobile Touch
//        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) && canLooseTheScrew)
//        {
//            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//            RaycastHit[] hits = Physics.RaycastAll(raycast, 20f);
//            if (hits.Length > 0)
//            {
//                for (int i = 0; i < hits.Length; i++)
//                {
//                    if (hits[i].collider.CompareTag("InteractableScrew"))
//                    {
//                        LooseScrew();
//                    }
//                }
//            }
//        }
    }

    private void OnMouseUp()
    {
        if(canLooseTheScrew)
        {
            LooseScrew();
        }
    }

    void LooseScrew()
    {
        if (hasAlreadyLoosen) return;

        Debug.Log("wtf? screw index:" + screwIndex);

        hasAlreadyLoosen = true;
        gameObject.GetComponent<MeshRenderer>().material = unscrewIndicatorMat;
        NotificationCenter.DefaultCenter().PostNotification(this, "looseScrewDidTapped", screwIndex);
    }

    // ---- Listener ------------------------------------------------------
    void OnCorrectScrewDriverSelected(Notification notification)
    {
        if (notification.data != null)
        {
            if ((int)notification.data == 5)
            {
                Debug.Log("correct screwdriver");
                canLooseTheScrew = true;
            }
            else
            {
                canLooseTheScrew = false;
            }
        }
    }
}