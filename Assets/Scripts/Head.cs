using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    public Transform bodyPrefab;
    public int size = 3;
    public float headspeed;
    private List<Transform> body = new List<Transform>();
    private int currentScore;
    public Text scoreText;
    bool isMovingUp = true,isMovingDown,isMovingLeft,isMovingRight;

    public Text outputText;
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;
    public float swipeRange;
    public float tapRange;

    public GameObject restartPanel;
    
    void Start(){
        reset();
        currentScore = 0;
    }

    void Update()
    {
        Time.fixedDeltaTime = headspeed;
        //uncomment swipe for touch controls.
        //Swipe();

         //WASD Controls
         
        if (Input.GetKeyDown(KeyCode.W) && isMovingDown == false){
            direction = Vector3.right;
            isMovingLeft = false;
            isMovingRight = false;
            isMovingUp = true;
            isMovingDown = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.S) && isMovingUp == false){
            direction = Vector3.left;
            isMovingLeft = false;
            isMovingRight = false;
            isMovingDown = true;
            isMovingUp = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && isMovingRight == false){
            isMovingUp = false;
            isMovingDown = false;
            isMovingRight = true;
            isMovingLeft = true;
            direction = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.D) && isMovingLeft == false){
            isMovingUp = false;
            isMovingDown = false;
            isMovingLeft = true;
            isMovingRight = true;
            direction = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.Space)){
            transform.position = new Vector3(23f,1f,26f);
        }   

        //*/

        if (currentScore == 0){
            headspeed = 0.08f;
        }
        

    }

    public void Swipe(){

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if(!stopTouch)
            {
                if (Distance.x < -swipeRange && isMovingRight == false)
                {
                    outputText.text = "Left";
                    isMovingUp = false;
                    isMovingDown = false;
                    isMovingRight = true;
                    isMovingLeft = true;
                    direction = Vector3.forward;
                    stopTouch = true; 
                }
                else if (Distance.x > swipeRange && isMovingLeft == false)
                {
                    outputText.text = "Right";
                    direction = Vector3.back;
                    isMovingUp = false;
                    isMovingDown = false;
                    isMovingLeft = true;
                    isMovingRight = true;
                    stopTouch = true; 
                }
                else if (Distance.y > swipeRange && isMovingDown == false)
                {
                    outputText.text = "Up";
                    direction = Vector3.right;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingUp = true;
                    isMovingDown = true;
                    stopTouch = true; 
                }
                else if (Distance.y < -swipeRange && isMovingUp == false)
                {
                    outputText.text = "Down";
                    direction = Vector3.left;
                    isMovingLeft = false;
                    isMovingRight = false;
                    isMovingDown = true;
                    isMovingUp = true;
                    stopTouch = true; 
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                outputText.text = "Tap";
            }
        }
    }
    void FixedUpdate()
    {
        for (int i = body.Count - 1 ; i > 0; i--)
        {
            body[i].position = body [i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            Mathf.Round(this.transform.position.z) + direction.z 
        );
    }

    private void HandleScore ()
    {
        scoreText.text = "Score: " + currentScore;
        if(currentScore==30)
        {
            Debug.Log("won");
            restartPanel.SetActive(true);
        }
    }

    void grow(){
        Transform segment = Instantiate(this.bodyPrefab);
        segment.position = body[body.Count - 1].position;
        body.Add(segment);
    }

    void reset(){
        for (int i = 1; i < body.Count; i++){
            Destroy(body[i].gameObject);
        }

        body.Clear();
        body.Add(this.transform);

        for(int i = 1; i < size; i++){
            body.Add(Instantiate(this.bodyPrefab));
        }

        this.transform.position = new Vector3(23f,1f,26f);

    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Food"){
            grow();
            currentScore ++;
            HandleScore ();
            
        }
        else if (other.tag == "Obstacle"){
            reset();
            currentScore = 0;
            HandleScore ();
        }
        
    }

}
