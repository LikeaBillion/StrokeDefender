using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //field for movespeed of the player
    [SerializeField] float moveSpeed = 5f;

    //movement rawInput
    Vector2 rawInput;
    public bool paused;

    //fields for padding to guide bounds
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    //min and max bounds
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;
    Health health;
    LevelManager levelManager;    

    void Awake() {
        shooter = GetComponent<Shooter>();
        levelManager= FindObjectOfType<LevelManager>();
        paused = false;
    }
    void Start(){
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    //method to create bounds
    void InitBounds(){
        //gets main camera
        Camera mainCamera = Camera.main;
        //states the bounds are the edges of the cameras view
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    void Move()
    {
        //takes rawInput and uses delta time to conform it
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        //newPos to work out whether its leaving bounds
        Vector2 newPos = new Vector2();

        //setting x and y for newPos - clamping to the value of the bounds + padding 
        newPos.x = Mathf.Clamp(transform.position.x+delta.x,minBounds.x + paddingLeft,maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y+delta.y,minBounds.y + paddingBottom,maxBounds.y - paddingTop);
        //sets position to new pos
        transform.position = newPos;
    }

    void OnMove(InputValue value){
        //gets raw vector2 on keypress
        if(paused){return;}
        rawInput = value.Get<Vector2>();
        
    }

    void OnFire(InputValue value){
        if(shooter != null){
            shooter.isFiring = value.isPressed;
        }
    }

    void OnPause(){
        if (!paused) {
                levelManager.PauseGame();
        }
    }
}