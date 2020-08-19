using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ARTextMesh : MonoBehaviour
{
    [SerializeField] string displayedText;

    TextMeshPro textMesh;

    bool hasStoppedTyping;
    string currText;
    float delayTextWrite = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();

        StartCoroutine(ShowDialogueText(displayedText));
    }

    IEnumerator ShowDialogueText(string _dialText)
    {
        hasStoppedTyping = false;
        for (int i = 0; i <= _dialText.Length; i++)
        {
            if (hasStoppedTyping)
            {
                break;
            }
            currText = _dialText.Substring(0, i);
            textMesh.text = currText;
            yield return new WaitForSeconds(delayTextWrite);
        }
    }
}
