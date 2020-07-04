using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectToPlace;

    //ARSessionOrigin arOrigin;
    ARRaycastManager arRaycastManager;
    Pose placementPose;
    bool isPlacementPoseValid;
    bool hasObjectPlaced;

    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if(isPlacementPoseValid && !hasObjectPlaced && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        hasObjectPlaced = true;

        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if(isPlacementPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(.5f, .5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        isPlacementPoseValid = hits.Count > 0;
        if(isPlacementPoseValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward; // We don't have to know how far the camera is pointing towards an object,
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized; // instead we just need to know the camera bearing (rotation).
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
        
    }
}
