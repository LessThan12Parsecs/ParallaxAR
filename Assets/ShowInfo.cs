using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject label;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MainCamera"){
            label.SetActive(true);
        }    
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "MainCamera"){
            label.SetActive(false);
        }
    }
}
