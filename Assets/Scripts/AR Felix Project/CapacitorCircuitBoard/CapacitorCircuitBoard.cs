using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitorCircuitBoard : MonoBehaviour
{
    bool canAnalyseBoard;       // Ready for user to tap the board.

    // Start is called before the first frame update
    void Start()
    {

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
                                    AnalyseCircuitBoard();
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
                        AnalyseCircuitBoard();
                    }
                }
            }
        }
    }

    public void SetCanStartAnalyseBoard(bool _can)
    {
        canAnalyseBoard = _can;
    }

    void AnalyseCircuitBoard()
    {
        // TODO: analyse baord

    }
}
