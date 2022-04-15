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
    LevelManager levelManager;
    ScoreKeeper scoreKeeper;
    int currentWaveCount =0;

    void Awake() {
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start(){
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave(){
        return currentWave;
    }

    public int GetNumberOfWaves(){
        return waveConfigs.Count;
    }

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
        scoreKeeper.finalScore(levelManager.scene);
        levelManager.LevelCompleted();
    }
}
