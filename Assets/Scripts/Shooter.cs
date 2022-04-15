using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projecilePrefab;
    [SerializeField] float projecileSpeed = 10f;
    [SerializeField] float projecileLifetime = 5f;
    [SerializeField] float basefiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector]public bool isFiring;
    [HideInInspector]public bool noFire = false;
    

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    Animator myAnimator;
    
    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        if(!useAI) {myAnimator = GetComponent<Animator>();}

    }

    void Start(){
        if(useAI){
            isFiring = true;
        }
        
    }

    void Update(){
        if(!noFire){
            Fire();
        }
        
    }

    void Fire(){
        if(isFiring && firingCoroutine == null){
            firingCoroutine = StartCoroutine(FireContinuously());
            if(!useAI){
                myAnimator.SetBool("isFiring", true);

            }
        }
        else if(!isFiring && firingCoroutine != null){
            StopCoroutine(firingCoroutine);
            if(!useAI){
                myAnimator.SetBool("isFiring", false);
            ;}
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously(){
        while(true){
            GameObject instance = Instantiate(projecilePrefab,transform.position + new Vector3(0,0.912f,0),Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null){
                rb.velocity = transform.up * projecileSpeed;
            }
            Destroy(instance,projecileLifetime);
            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance, basefiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate,float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    
}
