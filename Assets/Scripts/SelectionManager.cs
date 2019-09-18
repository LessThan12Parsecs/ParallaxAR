using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SelectionManager : MonoBehaviour
{

    [SerializeField]
    private Button[] buttons;
    private int buttonIndex = -1; //TODO set to -1 after placing object in TapToPlace
    private TapToPlace tapManager;

     // Start is called before the first frame update
    // public GameObject placementIndicator;
    private ARRaycastManager arRaycastManagerOrigin;
   
    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    private Material selectedMaterial;
    // public GameObject objectToPlace;
    private Vector2 touchPosition = default;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits= new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;

 
    private void Start () 
    {
        tapManager = transform.GetComponent<TapToPlace>();
        arRaycastManagerOrigin = FindObjectOfType<ARRaycastManager>();
    }

    public void SetAllButtonsInteractable()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void OnButtonClicked(Button clickedButton)
    {
        buttonIndex = System.Array.IndexOf(buttons, clickedButton);
        if (buttonIndex == -1)
            return;
        SetAllButtonsInteractable();

        clickedButton.interactable = false;
        tapManager.SetSelectedButtonIndex(buttonIndex);
    }

    public int GetCurrentButton()
    {
        return buttonIndex;
    }

    public void resetButtonIndex(){
        buttonIndex = -1;
    }

    private void Update(){

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = targetCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    if (lastSelectedObject != null)
                    {
                        PlacementObject [] allOtherObjects = FindObjectsOfType<PlacementObject>();
                        lastSelectedObject.GetComponentInChildren<Renderer>().material = selectedMaterial;
                        foreach (PlacementObject placementObject in allOtherObjects)
                        {
                            placementObject.Selected = placementObject == lastSelectedObject;
                        }
                    }
                }      
            }
            if (touch.phase == TouchPhase.Ended)
            {
                lastSelectedObject.Selected = false;
            }
            
        }
        if (arRaycastManagerOrigin.Raycast(touchPosition,hits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            if (lastSelectedObject.Selected)
            {
                lastSelectedObject.transform.position = hitPose.position;
                lastSelectedObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}
