using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using AREnums;

public class TutorialMessageCenter : MonoBehaviour
{
    static TutorialMessageCenter Instance;

    [SerializeField] Text tutorialTitle;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ShowTitleMessage(TutorialMessageCode.Msg_Machine_code);
    }

    public static void ShowTitleMessage(TutorialMessageCode messageCode)
    {
        switch (messageCode)
        {
            case TutorialMessageCode.Msg_Machine_code:
                Instance.tutorialTitle.text = "Honeywell Diesel Generator RD-850M";
                break;
            case TutorialMessageCode.Msg_UnscrewCapacitorBoard:
                Instance.tutorialTitle.text = "Unleash all four screws at four corners \nof the board.";
                break;
            case TutorialMessageCode.Msg_DoubleClickToDisplayCapacitorBoardInfo:
                Instance.tutorialTitle.text = "Click the capacitor board to show its info.";
                break;
        }
    }
}
