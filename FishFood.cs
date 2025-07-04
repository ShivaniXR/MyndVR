using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFood : MonoBehaviour
{
    public Rigidbody rigid;
    public Transform fishFoodObject;
    public ParticleSystem particles;

    public float checkRate = 0.35f;
    public float shakeThreshold = 0.1f;

    private Vector3 initialFishFoodPosition;
    private bool isShaking = false;

    private void Start()
    {
        initialFishFoodPosition = fishFoodObject.position;
        StartCoroutine(ShakeCheck());
    }

    private IEnumerator ShakeCheck()
    {
        while (true)
        {
            Vector3 currentPosition = fishFoodObject.position;
            Vector3 velocity = (currentPosition - initialFishFoodPosition) / Time.deltaTime;
            float speed = velocity.magnitude;

            if (speed > shakeThreshold)
            {
                if (!isShaking)
                {
                    isShaking = true;
                    particles.Play();
                }
            }
            else
            {
                if (isShaking)
                {
                    isShaking = false;
                    particles.Stop();
                }
            }

            initialFishFoodPosition = currentPosition;
            yield return new WaitForSeconds(checkRate);
        }
    }
}
