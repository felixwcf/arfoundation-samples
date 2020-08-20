using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewDriverButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Calling from screwdriver selection list scrollview 
    public void ScrewDriverDidSelected(int index)
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "OnCorrectScrewDriverSelected", index);
    }
}
