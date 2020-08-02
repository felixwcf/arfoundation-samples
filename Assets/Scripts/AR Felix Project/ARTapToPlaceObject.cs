using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR

using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject DieselGeneratorModel;

    //private ARSessionOrigin arOrigin;
    Pose PlacementPose;
    ARRaycastManager aRRaycastManager;
    bool placementPoseIsValid = false;

    bool hasAlreadyPlaceObject;
    float rotationSpeedFactor = 0.4f;

    GameObject currDispModel;

    private Touch touch;

    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !hasAlreadyPlaceObject)
        {
            hasAlreadyPlaceObject = true;
            PlaceObject();
        }
        else if(hasAlreadyPlaceObject)
        {
            
            if (Input.touchCount > 0)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    touch = Input.GetTouch(0);

                    var rotationY = Quaternion.Euler(0, -touch.deltaPosition.x * rotationSpeedFactor, 0);
                    currDispModel.transform.rotation = rotationY * currDispModel.transform.rotation;
                }
            }
        }
    }

    private void PlaceObject()
    {
        currDispModel = Instantiate(DieselGeneratorModel, PlacementPose.position, PlacementPose.rotation);

        // turn off placement indicator after object has been displayed
        placementIndicator.SetActive(false);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }
    }
}