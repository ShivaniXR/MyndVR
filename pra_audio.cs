using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pra_audio : MonoBehaviour

{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    //public AudioListener AudioListener;
    //public float delay=155;
    
    void Start()
    {
        audioSource1.PlayScheduled(AudioSettings.dspTime);
        double clipLength = audioSource1.clip.samples / audioSource1.clip.frequency;
        audioSource2.PlayScheduled(AudioSettings.dspTime + 155);
        //AudioListener.pause = true;
    }
 
   

    // Update is called once per frame
    void Update()
    {
        
    }
    }