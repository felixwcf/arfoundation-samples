using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using AREnums;
using cakeslice;

public class CapacitorCircuitBoard : MonoBehaviour
{
    enum AnalyseBoardStepType
    {
        ShowScrewsIndicators,

    }

    ARMainModel arMainModel;
    [SerializeField] Outline growOutline;

    [SerializeField] GameObject[] screwIndicators;

    bool canAnalyseBoard;       // Ready for user to tap the board.

    // Start is called before the first frame update
    void Start()
    {
        arMainModel = GameObject.FindGameObjectWithTag("ARMainModel").GetComponent<ARMainModel>();
        growOutline.enabled = false;
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

        // Show Screws Indicator
        switch (stepType)
        {
            case AnalyseBoardStepType.ShowScrewsIndicators:

                for (int i = 0; i< screwIndicators.Length;i++)
                {
                    screwIndicators[i].SetActive(true);
                }

                //transform.DOLocalMove(new Vector3(-0.45f, 0.606f, -1.039f), 1, false);
                break;
        }

        // Play Unscrew Tutorial Video
        ARVideoPlayer.Instance.PlayTutorialVideo(VideoCode.UnscrewCapacitorBoard);

        yield return new WaitForSeconds(0.4f);

    }
}
