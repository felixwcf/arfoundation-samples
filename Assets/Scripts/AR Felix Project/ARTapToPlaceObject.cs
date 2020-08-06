using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
//using UnityEngine.Experimental.XR

using UnityEngine.XR.ARSubsystems;

using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public static readonly string MODEL_OBJECT_NAME = "DieselGenerator_Modified Variant";
    public static readonly string MODEL_PARTS_INDICATORS_PARENT_OBJECT_NAME = "Generator Selection Indicators";

    public GameObject placementIndicator;
    public GameObject DieselGeneratorModel;

    public Text txtTest;

    //private ARSessionOrigin arOrigin;
    Pose PlacementPose;
    ARRaycastManager aRRaycastManager;
    bool placementPoseIsValid = false;

    bool hasAlreadyPlaceObject;

    public static GameObject currDispModel;

    public ARInteraction arInteraction;

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
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        hasAlreadyPlaceObject = true;

        currDispModel = Instantiate(DieselGeneratorModel, PlacementPose.position, PlacementPose.rotation);
        currDispModel.name = MODEL_OBJECT_NAME;
        arInteraction.UpdateARDetailReady(currDispModel);

        // turn off placement indicator after object has been displayed
        placementIndicator.SetActive(false);

        //StartCoroutine(DestroySelf());
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid && !hasAlreadyPlaceObject)
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

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}