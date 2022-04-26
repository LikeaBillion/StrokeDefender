using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializeField] bool isPlayer;
   [SerializeField] bool isSpecial;
    //field for the health of the object
   [SerializeField] int health = 50;
   [SerializeField] int score = 100;
   //imports for hiteffect particles
   [SerializeField] ParticleSystem hitEffect;
   //imports for camera shake
   [SerializeField] bool applyCameraShake;
   //imports for other scripts
   CameraShake cameraShake;
   AudioPlayer audioPlayer;
   ScoreKeeper scoreKeeper;
   LevelManager levelManager;
  
   


    void Awake() {
        levelManager = FindObjectOfType<LevelManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        

    }

   void OnTriggerEnter2D(Collider2D other) {
       //tries to get a damage dealer from the collided entity
       DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        //if there was one
       if(damageDealer != null){
           //take damage from the damage they deal
           TakeDamage(damageDealer.GetDamage());
           //calls the hiteffect
           PlayHitEffect();
           //plays noise to take damage
           audioPlayer.PlayDamageSound();
           //calls the shakecamera
           ShakeCamera();
           //damage dealer hit called to destroy object
           damageDealer.Hit();
       }
   }

   //getter for health
   public int GetHealth(){
       return health;
   }

   public void TakeDamage(int damage){
       //health is the same - damage
       health -= damage;
        if(damage < 0 && health<=6){
            audioPlayer.PlayHealSound();
        }
       if(health <= 0){
           //if no health destroy object
           Die();
       }
   }

    //heals the player fully
   public void Heal(){
       health = 5;
   }

    //method that greats the particles on hitting an enemy
   void PlayHitEffect(){
       if(hitEffect != null){
           ParticleSystem instance = Instantiate(hitEffect, transform.position,Quaternion.identity);
           Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
       }
   }
    //called when either the player or ai dies
   void Die(){
       //when is not player adds score
       if(!isPlayer){
           //if is special enemy, then calls the question method to begin asking the question
           if(isSpecial){
                levelManager.Question();
                //if special, they are worth more score
                score = score*5;
            }
            scoreKeeper.ModifyScore(score);
       }
       //when is player dies
       else{
           scoreKeeper.FinalScore(levelManager.scene);
           levelManager.GameOver();
       }
       //game object destroyed
       Destroy(gameObject);
   }

    //calls camera shake when damage is taken
   void ShakeCamera(){
       if(cameraShake != null && applyCameraShake){
           cameraShake.Play();
       }
   }

}
