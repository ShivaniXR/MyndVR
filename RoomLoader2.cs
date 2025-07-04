using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader2 : MonoBehaviour
{
     public Transform globe; 
     public float detectionThreshold = 0.1f; // Adjust the threshold for detecting changes
    
     private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
         initialPosition = globe.position;
    }

  private void Update()
    {
        // Calculate the difference in position
        Vector3 positionDifference = globe.position - initialPosition;
        
        // Check if the position difference is greater than the threshold
        if (positionDifference.magnitude >= detectionThreshold)
        {
            LoadNewScene();
        }
    }

    private void LoadNewScene()
    {
        // Load your new scene here
        SceneManager.LoadScene("Kalpana");
    }
}
