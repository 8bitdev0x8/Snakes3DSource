using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider grid;

    void Start(){
        RandomPosition();
    }

    private void RandomPosition(){
        GetComponent<Renderer>().material.color= Random.ColorHSV(0f,1f,1f,1f,0.5f,1f);
        
        Bounds bounds = this.grid.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y), Mathf.Round(z));
    }
        
    private void OnTriggerEnter(Collider other){
        if (other.tag == "Head"){
            RandomPosition();
        }
    }
}
