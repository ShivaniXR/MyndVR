using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBreathing : MonoBehaviour
{
   
    public Transform cameraTransform; // Reference to the camera's transform
    public float moveSpeed = 1.0f; // Speed at which particles move towards the camera
    public float startDelay = 2.0f; // Delay before starting particle movement
    public float resetDelay = 2.0f; // Delay before resetting particle system

    public ParticleSystem[] particleSystems; // Array of particle systems to manage

    private bool[] isMoving; // Array to track particle system movement status

    private void Start()
    {
        isMoving = new bool[particleSystems.Length];

        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
            isMoving[i] = false;
        }

        InvokeRepeating("StartParticleMovement", startDelay, startDelay);
    }

    private void Update()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            if (isMoving[i])
            {
                MoveParticles(i);
            }
        }
    }

    private void StartParticleMovement()
    {
        int randomIndex = Random.Range(0, particleSystems.Length);
        particleSystems[randomIndex].Play();
        isMoving[randomIndex] = true;
        StartCoroutine(ResetParticleSystem(randomIndex, particleSystems[randomIndex].main.duration + resetDelay));
    }

    private void MoveParticles(int index)
    {
        Vector3 directionToCamera = (cameraTransform.position - particleSystems[index].transform.position).normalized;
        particleSystems[index].transform.position += directionToCamera * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(particleSystems[index].transform.position, cameraTransform.position) < 0.1f)
        {
            particleSystems[index].Stop();
            isMoving[index] = false;
        }
    }

    private IEnumerator ResetParticleSystem(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        particleSystems[index].Stop();
        isMoving[index] = false;
    }
}
