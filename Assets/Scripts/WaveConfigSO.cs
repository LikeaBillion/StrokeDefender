using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    //list of gameobjects for enemies
    [SerializeField] List<GameObject> enemyPrefabs;
    //field for pathfinding object
    [SerializeField] Transform pathPrefab;
    //field for enemy movespeed
    [SerializeField] float moveSpeed = 5;
    //field for time between spawns
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    //field for variance in spawns
    [SerializeField] float spawnTimeVarience;
    //field for minimum spawn time
    [SerializeField] float minimumSpawnTime = 0.2f;
    
    //getter for enemycount
    public int GetEnemyCount(){
        return enemyPrefabs.Count;
    }

    //getter for enemy[index] in prefab list
    public GameObject GetEnemyPrefab(int index){
        return enemyPrefabs[index];
    }

    //function to return initial child of pathPrefab- first waypoint
    public Transform GetStartingWaypoint(){
        return pathPrefab.GetChild(0);
    }

    //function to return list of all waypoints under pathPrefab
    public List<Transform> GetWaypoints(){
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab){
            waypoints.Add(child);
        }
        return waypoints;
    }

    //getter for movespeed
    public float GetMoveSpeed(){
        return moveSpeed;
    }

    //generates a random spawn time for the enemies
    public float GetRandomSpawnTime(){
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVarience, timeBetweenEnemySpawns+spawnTimeVarience);
        return Mathf.Clamp (spawnTime,minimumSpawnTime, float.MaxValue);
    }

}
