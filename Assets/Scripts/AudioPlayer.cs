using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    //shooting clip area- including all the settings wanting to be edited in the inspector
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 1f;

    //damage clip area- including all the settings wanting to be edited in the inspector
    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f,1f)] float damageVolume = 1f;

    //health clip area- including all the settings wanting to be edited in the inspector
    [Header("Health")]
    [SerializeField] AudioClip healthClip;
    [SerializeField] [Range(0f,1f)] float healthVolume = 1f;


    //bellow code is to make the audioplayer a singleton class

    //sets this audioplayer insance
    static AudioPlayer instance;

    //getter for current instance
    public AudioPlayer GetInstance(){
        return instance;
    }
    
    void Awake() {
        ManageSingleton();    
    }

    //if there's an instance- don't create a new one and delete the one trying to be made
    void ManageSingleton(){
        if(instance !=null){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else{
            //if there isn't an instance, then create one
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //plays shooting clip
    public void PlayShootingClip(){
        PlayClip(shootingClip,shootingVolume);
    }

    //plays damage clip
    public void PlayDamageSound(){
        PlayClip(damageClip,damageVolume);
    }

    //plays heal clip
    public void PlayHealSound(){
        PlayClip(healthClip,healthVolume);
    }
    
    //function to play any clip passed in
    void PlayClip(AudioClip clip, float volume){
        //if there is a clip to play
        if(clip != null){
            //sets camera pos, needs this for the audio to appear right in 2d
            Vector3 cameraPos = Camera.main.transform.position;
            //plays clip at point of the camera
            AudioSource.PlayClipAtPoint(clip,cameraPos,volume);
        }
    }
}

    
