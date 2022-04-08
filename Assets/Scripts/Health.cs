using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //field for the health of the object
   [SerializeField] int health = 50;

   void OnTriggerEnter2D(Collider2D other) {
       //tries to get a damage dealer from the collided entity
       DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        //if there was one
       if(damageDealer != null){
           //take damage from the damage they deal
           TakeDamage(damageDealer.GetDamage());
           //damage dealer hit called to destroy object
           damageDealer.Hit();
       }
   }

   void TakeDamage(int damage){
       //health is the same - damage
       health -= damage;
       if(health <= 0){
           //if no health destroy object
           Destroy(gameObject);
       }
   }
}
