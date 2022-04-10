using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializeField] bool isPlayer;
    //field for the health of the object
   [SerializeField] int health = 50;
   [SerializeField] int score = 100;
   [SerializeField] ParticleSystem hitEffect;

   [SerializeField] bool applyCameraShake;
   CameraShake cameraShake;

   AudioPlayer audioPlayer;
   ScoreKeeper scoreKeeper;

    void Awake() {
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

   public int GetHealth(){
       return health;
   }

   void TakeDamage(int damage){
       //health is the same - damage
       health -= damage;
       if(health <= 0){
           //if no health destroy object
           Die();
       }
   }

   void PlayHitEffect(){
       if(hitEffect != null){
           ParticleSystem instance = Instantiate(hitEffect, transform.position,Quaternion.identity);
           Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
       }
   }

   void Die(){
       if(!isPlayer){
           scoreKeeper.ModifyScore(score);
       }
       Destroy(gameObject);
   }

   void ShakeCamera(){
       if(cameraShake != null && applyCameraShake){
           cameraShake.Play();
       }
   }
}
