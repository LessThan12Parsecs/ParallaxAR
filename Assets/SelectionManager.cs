using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    [SerializeField]
    private Button[] buttons;
    private int buttonIndex = -1; //TODO set to -1 after placing object in TapToPlace
    private TapToPlace tapManager;


    private void Start () 
    {
        tapManager = transform.GetComponent<TapToPlace>();
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
}
