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
    //field bool for if the implementation needs to loop
    [SerializeField] bool isLooping = false;
    //variable for the currentwave
    WaveConfigSO currentWave;

    void Start(){
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave(){
        return currentWave;
    }

    //coroutine for spawning waves
    IEnumerator SpawnEnemyWaves(){
        do{
            foreach(WaveConfigSO wave in waveConfigs){
            //currentwave = wave in waveConfig
            currentWave = wave;
            for(int i =0;i <currentWave.GetEnemyCount();i++){
                //instantiate new enemy
                Instantiate(currentWave.GetEnemyPrefab(i),currentWave.GetStartingWaypoint().position,Quaternion.identity,transform);
                //yield for spawn time
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            //yield for time between waves
            yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while(isLooping == true);
    }
}
