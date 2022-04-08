using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{   
    //instance of enemyspawner
    EnemySpawner enemySpawner;
    //instance of waveconfigso
    WaveConfigSO waveConfig;
    //creates list of waypoints to be filled further down
    List<Transform> waypoints;
    //index to find what waypoint we are upto using
    int waypointIndex = 0;

    void Awake(){
        //finds the enemyspawner and assigns to the variable
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start(){
        //sets current wave as the wave being used
        waveConfig = enemySpawner.GetCurrentWave();
        //creates a list of the waypoints based of the waveConfig
        waypoints = waveConfig.GetWaypoints();
        //sets starting location
        transform.position = waypoints[waypointIndex].position;
    }

    void Update(){
        FollowPath();
    }

    private void FollowPath() {
        //if there are waypoints left
        if(waypointIndex < waypoints.Count){
            //gets the next position to be used
            Vector3 targetPosition = waypoints[waypointIndex].position;
            //fors the delta, how much its going to move
            float delta = waveConfig.GetMoveSpeed()*Time.deltaTime;
            //transforms using the current pos, destination and speed
            transform.position = Vector2.MoveTowards(transform.position,targetPosition, delta);
            if(transform.position == targetPosition){
                waypointIndex++;
            }
        }
        else{
            //once done the game object isn't in frame destroy it
            Destroy(gameObject);
        }
    }
}
