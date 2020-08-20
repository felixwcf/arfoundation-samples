using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using AREnums;
using cakeslice;

//NotificationCenter.DefaultCenter().AddObserver(this, PersistKeys.ON_GAMEPLAY_PAUSE);

public class CapacitorCircuitBoard : MonoBehaviour
{
    enum AnalyseBoardStepType
    {
        ShowScrewsIndicators,

    }

    ARMainModel arMainModel;
    [SerializeField] Outline growOutline;

    [SerializeField] GameObject[] screwIndicators;
    [SerializeField] GameObject[] screwTypeIndicator;
    [SerializeField] GameObject[] screwLineIndicator;
    GameObject screwDriverScrollView;

    bool canAnalyseBoard;       // Ready for user to tap the board.
    bool canUnscrewTheBoard;    // When correct screwdriver is selected.

    // Start is called before the first frame update
    void Start()
    {
        arMainModel = GameObject.FindGameObjectWithTag("ARMainModel").GetComponent<ARMainModel>();


        // TODO: select screwdriver and tap on screw indicator to unscrew the board
        NotificationCenter.DefaultCenter().AddObserver(this, "OnCorrectScrewDriverSelected");

        growOutline.enabled = false;
    }

    private void OnDisable()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "OnCorrectScrewDriverSelected");
    }

    private void OnDestroy()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "OnCorrectScrewDriverSelected");
    }

    void Update()
    {
        List<Touch> touches = InputHelper.GetTouches();

        // For Editor Keyboard
        if (touches.Count > 0 && canAnalyseBoard)
        {
            foreach (Touch touch in touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        Ray raycast = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit[] hits = Physics.RaycastAll(raycast, 20f);
                        if (hits.Length > 0)
                        {
                            for(int i=0;i<hits.Length;i++)
                            {
                                if (hits[i].collider.CompareTag("CapacitorCircuitBoard"))
                                {
                                    StartCoroutine(AnalyseCircuitBoard(AnalyseBoardStepType.ShowScrewsIndicators));
                                }
                            }
                        }
                        break;
                }
            }
        }

        // For Mobile Touch
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) && canAnalyseBoard)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit[] hits = Physics.RaycastAll(raycast, 20f);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.CompareTag("CapacitorCircuitBoard"))
                    {
                        StartCoroutine(AnalyseCircuitBoard(AnalyseBoardStepType.ShowScrewsIndicators));
                    }
                }
            }
        }
    }

    public void SetCanStartAnalyseBoard(bool _can)
    {
        canAnalyseBoard = _can;
    }

    IEnumerator AnalyseCircuitBoard(AnalyseBoardStepType stepType)
    {
        Debug.Log("ALoha~!");

        // Show Tutorial Message
        TutorialMessageCenter.ShowTitleMessage(TutorialMessageCode.Msg_UnscrewCapacitorBoard);

        // Dismiss selection list UI
        ARInteraction arInteraction = GameObject.FindGameObjectWithTag("ARInteraction").GetComponent<ARInteraction>();
        arInteraction.SetCanShowPartsDropDownList(false);

        if (!screwDriverScrollView) // Make sure only one instance is instantiated.
        {
            screwDriverScrollView = Instantiate(Resources.Load("ScrewdriversScrollView")) as GameObject;
            screwDriverScrollView.name = "screwDriverScrollView";
            screwDriverScrollView.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
        }

        // Show Screws Indicator
        switch (stepType)
        {
            case AnalyseBoardStepType.ShowScrewsIndicators:

                for (int i = 0; i< screwIndicators.Length;i++)
                {
                    screwIndicators[i].SetActive(true);
                    screwIndicators[i].GetComponent<MeshRenderer>().material.DOFade(1, 0.1f);
                    screwLineIndicator[i].SetActive(true);
                    screwTypeIndicator[i].SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                }

                //transform.DOLocalMove(new Vector3(-0.45f, 0.606f, -1.039f), 1, false);
                break;
        }

        // Play Unscrew Tutorial Video
        ARVideoPlayer.Instance.PlayTutorialVideo(VideoCode.UnscrewCapacitorBoard);

        yield return new WaitForSeconds(0.1f);

    }

    // ---- Listeners / Observers ---------------------------------------------
    // Do not remove this functions.
    void OnCorrectScrewDriverSelected(Notification notification)
    {
        if (notification.data != null)
        {
            Debug.Log("Selected screwdriver:" + notification.data);

            if ((int)notification.data == 5)
            {
                canUnscrewTheBoard = true;
            }
            else
            {
                canUnscrewTheBoard = false;
            }
        }
    }
}
