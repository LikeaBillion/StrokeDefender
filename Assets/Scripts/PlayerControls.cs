using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //sets variable for new input actions class instance
    private InputActions controls;
    Player player;

    void Awake() {
        controls = new InputActions();
        player = FindObjectOfType<Player>();
    }

    //enables controls
    public void OnEnable() {
        controls.Enable();
    }
    //disables controls
    public void OnDisable() {
        controls.Disable();
    }

    void Update() {
        Move();
        Fire(); 
        Pause();
    }

    void Move(){
        //reads the move key- then calls a method for the player to move based on that
        Vector2 move =controls.Player.Move.ReadValue<Vector2>();
        player.Move(move);
    }

    void Fire(){
        //reads the fire key, then passed value to the player based on that
        bool fire =controls.Player.Fire.IsPressed();    
        player.Fire(fire);
    }

    void Pause(){
        //reads paused key, passed value to player based on that
        float pause =controls.Player.Pause.ReadValue<float>(); 
        if(pause ==1){
            player.Pause();
        }
    }
}
