using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //field for movespeed of the player
    [SerializeField] float moveSpeed = 5f;

    //fields for padding to guide bounds
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    //min and max bounds
    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 delta;

    //instances of scripts
    Shooter shooter;
    Health health;
    LevelManager levelManager;    
    PlayerControls controls;
    ScoreKeeper scoreKeeper;

    //isComplete to see if the level is complete
    public bool isComplete;
    //paused to see if the level is paused
    public bool paused;
    

    void Awake() {
        shooter = GetComponent<Shooter>();
        levelManager= FindObjectOfType<LevelManager>();
        controls= FindObjectOfType<PlayerControls>();
        scoreKeeper= FindObjectOfType<ScoreKeeper>();

        paused = false;
        isComplete = false;
    }
    void Start(){
        InitBounds();
        
    }

    void Update(){
        if(isComplete){FlyOfScreen();}
    }

    //method to create bounds
    void InitBounds(){
        //gets main camera
        Camera mainCamera = Camera.main;
        //states the bounds are the edges of the cameras view
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    public void Move(Vector2 input)
    {
        //takes rawInput and uses delta time to conform it
        delta = input * moveSpeed * Time.deltaTime;
        //newPos to work out whether its leaving bounds
        Vector2 newPos = new Vector2();

        //setting x and y for newPos - clamping to the value of the bounds + padding 
        newPos.x = Mathf.Clamp(transform.position.x+delta.x,minBounds.x + paddingLeft,maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y+delta.y,minBounds.y + paddingBottom,maxBounds.y - paddingTop);
        //sets position to new pos
        transform.position = newPos;
        
    }

    public void Fire(bool pressedValue){
        //if there is no shooter= set firing pressed value
        if(shooter != null){
            shooter.isFiring = pressedValue;
        }
    }      

    public void Pause(){
        //if not paused, pause the game
        if (!paused) {
             levelManager.PauseGame();
        }
    }

    public void FlyOfScreen(){
        //function to handle player flying offscreen
        Camera mainCamera = Camera.main;
        //removes padding
        paddingTop =0;
        //changes max bounds
        maxBounds =  mainCamera.ViewportToWorldPoint(new Vector2(2,2));
        //flys towards the top of the screen
        transform.position = transform.position + new Vector3(0,1f,0)*Time.deltaTime*10;
    }


}