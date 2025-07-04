using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGathering : MonoBehaviour
{
    public Transform waterPlane; // Assign the water plane in the Inspector
    public float gatheringRadius = 5.0f; // Adjust the radius for fish gathering
    public float gatheringSpeed = 2.0f; // Adjust the speed at which the fish gather

    private bool isGathering = false;
    private Vector3 gatherPoint;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("FishFoodParticles")) //  tag the fish food particles
        {
            Vector3 collisionPoint = other.transform.position;

            // Set the gathering point and start gathering
            gatherPoint = collisionPoint;
            isGathering = true;
        }
    }

    private void Update()
    {
        if (isGathering)
        {
            GatherFish();
        }
    }

    private void GatherFish()
    {
        // Move the fish objects towards the gatherPoint
        foreach (Transform fish in transform)
        {
            Vector3 direction = gatherPoint - fish.position;
            float step = gatheringSpeed * Time.deltaTime;

            if (direction.magnitude > 0.1f) // Adjust threshold for stopping
            {
                fish.position = Vector3.MoveTowards(fish.position, gatherPoint, step);
            }
            else
            {
                isGathering = false;
            }
        }
    }
}

