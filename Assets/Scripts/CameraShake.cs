using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //two adjustable fields for the camera shake
    [SerializeField] float shakeDuration = 0.1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    //vector3 for the starting postion- so we know where to return it to
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play(){
        StartCoroutine(Shake());
    }

    //coroutine for shaking the camera
    IEnumerator Shake(){
        float elapsedTime = 0;
        //while time elapsed isn't greater than shake duration
        while(elapsedTime < shakeDuration){
            //transform the camera to be somewhere different randomly
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            //adds delta time to time elapsed 
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //reset transform when done
        transform.position = initialPosition;
    }
}
