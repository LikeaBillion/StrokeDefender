using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //field for damage the damage dealer does
    [SerializeField] int damage = 10;

    //getter for damage
    public int GetDamage(){
        return damage;
    }

    //when hit destroy itself
    public void Hit(){
        Destroy(gameObject);
    }
}
