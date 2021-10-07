using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    public GameObject InstructionPanel;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch;
    public float tapRange;
    void Update()
    {
        Swipe();
    }

    public void Swipe(){

        if(Input.GetKeyDown(KeyCode.Space))
        {
            InstructionPanel.SetActive(false);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                InstructionPanel.SetActive(false);
            }
        }
    }

}
