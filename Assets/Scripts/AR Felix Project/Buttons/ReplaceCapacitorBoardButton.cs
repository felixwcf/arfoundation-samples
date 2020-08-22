using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class ReplaceCapacitorBoardButton : MonoBehaviour
{
    [SerializeField] Vector3 moveOutPos;
    [SerializeField] Vector3 originalPos;

    private GameObject capacitorBoard;

    // Start is called before the first frame update
    private void Start()
    {
        capacitorBoard = GameObject.FindGameObjectWithTag("CapacitorCircuitBoard");
    }

    private void OnMouseUpAsButton()
    {
        StartCoroutine(PerformReplaceBoard());
    }

    private IEnumerator PerformReplaceBoard()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        capacitorBoard.transform.DOLocalMove(moveOutPos, 1.6f).SetEase(Ease.OutQuad);
        capacitorBoard.GetComponent<MeshRenderer>().material.DOFade(0, 0.6f);

        yield return new WaitForSeconds(1f);

        capacitorBoard.transform.DOLocalMove(originalPos, 1.6f).SetEase(Ease.InQuad);
        capacitorBoard.GetComponent<MeshRenderer>().material.DOFade(1, 0.6f);
    }
}
