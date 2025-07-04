using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using Meta.WitAi.Events;

//using UnityEngine.InputSystem;
using TMPro;


public class VoiceScript : MonoBehaviour
{
     public AppVoiceExperience voiceExperience;
     public TextMeshProUGUI fullTranscription;
     private bool appVoiceActive;
    // Start is called before the first frame update
    private void Awake()
    {
       // voiceExperience.events.onFullTranscription.AddListener((transcription) =>
      //  {
        // fullTranscription.text = transcription;
      //  });
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.Button.One))
        { 
            voiceExperience.Activate();
        }
    }
}
