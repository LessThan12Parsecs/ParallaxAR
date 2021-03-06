﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapToPlace : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject placementIndicator;
    private ARRaycastManager arRaycastManagerOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private TMPro.TextMeshProUGUI DebugText;

    private TMPro.TextMeshProUGUI DebugText2;
   
    [SerializeField]
    private Camera targetCamera;
    // public GameObject objectToPlace;
    private SelectionManager selectionManager;
    private int selectedButtonIndex = -1;

    [SerializeField]
    private GameObject[] objectsToPlace;
  
    [SerializeField]
    private GameObject[] placementIndicators;


    private Vector2 touchPosition = default;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits= new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;



    void Start()
    {
        arRaycastManagerOrigin = FindObjectOfType<ARRaycastManager>();
        if (Debug.isDebugBuild)
        {
            DebugText = GameObject.Find("DebugText").GetComponent<TMPro.TextMeshProUGUI>();
            DebugText2 = GameObject.Find("DebugText2").GetComponent<TMPro.TextMeshProUGUI>();
        }
        selectionManager = transform.GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        // {
        //     PlaceObject();
        // }
    }

    public void PlaceObject()
    {
        if (selectedButtonIndex >= 0)
        {
            Instantiate(objectsToPlace[selectedButtonIndex], placementPose.position, placementPose.rotation);
            selectionManager.resetButtonIndex();
            selectionManager.SetAllButtonsInteractable();
            selectedButtonIndex = -1;
            DisablePlacementObjects();
        }
    }

    private void UpdateDebugText(string text)
    {
        DebugText.text = text;
    }
    private void UpdateDebugText2(string text)
    {
        DebugText.text = text;
    }

    // private void UpdatePlacementIndicator()
    // {
    //     if (placementPoseIsValid) 
    //     {
    //         placementIndicator.SetActive(true);
    //         placementIndicator.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
    //     }
    //     else {
    //         placementIndicator.SetActive(false);
    //     }
    // }
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid) 
        {
            placementIndicators[selectedButtonIndex].SetActive(true);
            placementIndicators[selectedButtonIndex].transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
        }        
        else {
            DisablePlacementObjects();
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = targetCamera.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManagerOrigin.Raycast(screenCenter,hits,UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = targetCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x,0,cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    public void SetSelectedButtonIndex(int index){
        selectedButtonIndex = index;
        DisablePlacementObjects();
    }

    private void DisablePlacementObjects()
    {
        foreach (var placementInd in placementIndicators)
        {
            placementInd.SetActive(false);
        }
    }
}
