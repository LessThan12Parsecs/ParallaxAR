using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatasetManager : MonoBehaviour
{   
    [SerializeField]
    private int selectedDataset;

    public int SelectedDataset 
    {
        get {return selectedDataset;}
        set {selectedDataset = value;}
    }
    public void SetCurrentDataset(int index) 
    {
        SelectedDataset = index;
    }   
}
