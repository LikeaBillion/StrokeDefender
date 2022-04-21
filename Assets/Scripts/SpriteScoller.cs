using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScoller : MonoBehaviour
{
    //speed of movement
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    
    void Update()
    {
        //moves background sprites for parallax scrolling, relative to time passed 
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
