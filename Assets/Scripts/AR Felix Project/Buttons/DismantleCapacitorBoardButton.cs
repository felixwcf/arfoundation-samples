using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class DismantleCapacitorBoardButton : MonoBehaviour
{
    [SerializeField] GameObject lineIndicator;

    private GameObject capacitorBoard;
    private bool hasDismantled;     // To prevent from firing this multiple time.

    // Start is called before the first frame update
    private void Start()
    {
        capacitorBoard = GameObject.FindGameObjectWithTag("CapacitorCircuitBoard");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        if (capacitorBoard && !hasDismantled)
        {
            StartCoroutine(PerformDismantleBaord());      
        }
    }

    private IEnumerator PerformDismantleBaord()
    {
        hasDismantled = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        lineIndicator.SetActive(false);
        capacitorBoard.transform.DOLocalMoveZ(-1.08f, 2f).SetEase(Ease.InOutQuart);

        yield return new WaitForSeconds(2);

        NotificationCenter.DefaultCenter().PostNotification(this, "dismantleBoardDidTapped");        
    }
}
