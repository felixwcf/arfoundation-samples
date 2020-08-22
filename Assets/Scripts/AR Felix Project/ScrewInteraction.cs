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
        // --- For Editor use ---
        List<Touch> touches = InputHelper.GetTouches();

        if (touches.Count > 0)
        {
            foreach (Touch touch in touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if(canLooseTheScrew)
                        {
                            LooseScrew();
                        }
                        break;
                    case TouchPhase.Ended:
                        break;
                }
            }
        }
    }

    private void OnMouseDown() // For mobile use
    {
        if(canLooseTheScrew) LooseScrew();
    }

    void LooseScrew()
    {
        if (hasAlreadyLoosen) return;

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