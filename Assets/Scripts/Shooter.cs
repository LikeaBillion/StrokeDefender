using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //field variables for general shooting behavior
    [Header("General")]
    [SerializeField] GameObject projecilePrefab;
    [SerializeField] float projecileSpeed = 10f;
    [SerializeField] float projecileLifetime = 5f;
    [SerializeField] float basefiringRate = 0.2f;

    //field variables for AI shooting behavior
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    //shooting variables called from elsewhere
    [HideInInspector]public bool isFiring;
    [HideInInspector]public bool noFire = false;
    

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    Animator myAnimator;
    
    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        //if not using ai, then don't call the animator
        if(!useAI) {myAnimator = GetComponent<Animator>();}

    }

    void Start(){
        if(useAI){
            //if using ai, then shoot at intervals
            isFiring = true;
        }
        
    }

    void Update(){
        //if can fire fire
        if(!noFire){
            Fire();
        }
        
    }

    void Fire(){
        //if is firing and there is no current coroutine
        if(isFiring && firingCoroutine == null){
            //start firing couroutine
            firingCoroutine = StartCoroutine(FireContinuously());
            if(!useAI){
                //animator setfiring is now true
                myAnimator.SetBool("isFiring", true);

            }
        }
        //if isn't firing and there is a firing coroutine
        else if(!isFiring && firingCoroutine != null){
            //stop routine
            StopCoroutine(firingCoroutine);
            if(!useAI){
                //animator setfiring is now false
                myAnimator.SetBool("isFiring", false);
            ;}
            //sets the firingCoroutine to be null
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously(){
        //while this can run..
        while(true){
            //create instance of projectile
            GameObject instance = Instantiate(projecilePrefab,transform.position + new Vector3(0,0.912f,0),Quaternion.identity);
            //access rb of projectile
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null){
                //fires at velocity
                rb.velocity = transform.up * projecileSpeed;
            }
            //once its lasted long enough, destroy
            Destroy(instance,projecileLifetime);
            //randomly assign time to next firing based on firing rate and the varience
            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance, basefiringRate + firingRateVariance);
            // sets how long until the next projectile 
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate,float.MaxValue);
            //plays shooting noise
            audioPlayer.PlayShootingClip();
            //waits until time to next projetile
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    
}
