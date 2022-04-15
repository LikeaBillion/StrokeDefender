using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private InputActions controls;
    Player player;

    void Awake() {
        controls = new InputActions();
        player = FindObjectOfType<Player>();
    }

    public void OnEnable() {
        controls.Enable();
    }

    public void OnDisable() {
        controls.Disable();
    }

    void Start()
    {
        
    }

    void Update() {
        Move();
        Fire(); 
        Pause();
    }

    void Move(){
        Vector2 move =controls.Player.Move.ReadValue<Vector2>();
        player.Move(move);
    }

    void Fire(){
        bool fire =controls.Player.Fire.IsPressed();    
        player.Fire(fire);
    }

    void Pause(){
        float pause =controls.Player.Pause.ReadValue<float>(); 
        if(pause ==1){
            player.Pause();
        }
    }
}
