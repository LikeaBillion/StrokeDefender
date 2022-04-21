using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //field for lists of waves
    [SerializeField] List<WaveConfigSO> waveConfigs;
    //fields for the time between waves
    [SerializeField] float timeBetweenWaves = 0f;
    //variable for the currentwave
    WaveConfigSO currentWave;
    
    //instances of other scripts
    LevelManager levelManager;
    ScoreKeeper scoreKeeper;
    Player player;

    //counter for the current wave
    int currentWaveCount =0;

    void Awake() {
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<Player>();
    }

    void Start(){
        StartCoroutine(SpawnEnemyWaves());
    }

    //returns currentwave
    public WaveConfigSO GetCurrentWave(){
        return currentWave;
    }
    //returns number of waves
    public int GetNumberOfWaves(){
        return waveConfigs.Count;
    }
    //returns count of waves currently happened
    public int GetCurrentWaveCount(){
        return currentWaveCount;
    }

    //coroutine for spawning waves
    IEnumerator SpawnEnemyWaves(){
            for(int j =0; j<waveConfigs.Count;j++){
            //currentwave = wave in waveConfig
            currentWave = waveConfigs[j];
            for(int i =0;i <currentWave.GetEnemyCount();i++){
                //instantiate new enemy
                Instantiate(currentWave.GetEnemyPrefab(i),currentWave.GetStartingWaypoint().position,Quaternion.Euler(0,0,180),transform);
                //yield for spawn time
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            //yield for time between waves
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveCount ++;
        }
        //when all enemies have spawned..
        //final score applied
        scoreKeeper.finalScore(levelManager.scene);
        //script called to have the effect of the player flying of screen
        player.FlyOfScreen();
        //level complete set to true
        player.isComplete = true;
        //levelmanager calls the new scene level completed
        levelManager.LevelCompleted();
    }
}
