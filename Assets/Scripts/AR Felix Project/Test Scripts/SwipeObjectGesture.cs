using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeObjectGesture : MonoBehaviour
{
    [SerializeField]
    GameObject targettedObject;

    float rotationSpeedFactor = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<Touch> touches = InputHelper.GetTouches();


        if (touches.Count > 0)
        {
            foreach (Touch touch in touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        break;
                    case TouchPhase.Moved:
                        var rotationY = Quaternion.Euler(0, -touch.deltaPosition.x * rotationSpeedFactor, 0);
                        targettedObject.transform.rotation = rotationY * targettedObject.transform.rotation;
                        break;
                }
            }
        }
    }
}
