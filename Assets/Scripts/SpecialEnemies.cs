using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemies : MonoBehaviour
{
    //field for their sprite renderer
    [SerializeField] SpriteRenderer sr;
    //instance of color
    Color color;  

    //values to change the effect
    float minimum = 0.3f;
    float maximum = 1f;
    float cyclesPerSecond = 2.0f;
    float a;
    bool increasing = true;
      
    void Start() {
        //sets color as sr.color
        color = sr.color;
        a = maximum;
    }



    void Update() {
        //t = current time
        float t = Time.deltaTime;
        //if a is bigger than max start to decrease it
        if (a >= maximum) increasing = false;
        //if a is lower than min start to increase it
        if (a <= minimum) increasing = true;
        //changes alpha with time
        a = increasing ? a += t * cyclesPerSecond * 2 : a -= t * cyclesPerSecond;
        //setvalue of alpha
        color.a = a;
        //sets color to be the same as color
        sr.color = color;
    }
}
