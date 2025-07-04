using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pranayam_audio : MonoBehaviour

{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
   
    void Start()
    {
        audioSource1.PlayScheduled(AudioSettings.dspTime + 212); //B in
        audioSource2.PlayScheduled(AudioSettings.dspTime + 219);  //B out
        
        audioSource3.PlayScheduled(AudioSettings.dspTime + 225); //Sansin 1
        
    }
 
   

    // Update is called once per frame
    void Update()
    {
        
    }
    }