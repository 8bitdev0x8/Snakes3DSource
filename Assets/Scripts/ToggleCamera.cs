using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;
    public GameObject ParentCamera;
    public GameObject FirstPersonCamera;

    public void toggleBtn(){
        if (toggle.isOn == true){
        ParentCamera.SetActive(true);
        FirstPersonCamera.SetActive(false);
        }
        else{
        ParentCamera.SetActive(false);
        FirstPersonCamera.SetActive(true);
        }
    }
    
}
