using System.Collections;
using UnityEngine;

public class ParticleActivator : MonoBehaviour
{
    public GameObject particlePrefab; // The prefab of the particle system
    public GameObject lightPrefab;    // The prefab of the light
    public float activationInterval = 7.0f; // Change the interval to 7 seconds
    public float activationRadius = 10.0f; // Radius for random position
    public float fixedYPosition = 1.0f; // The fixed Y position for the particles
    public float flickerDuration = 0.5f; // Duration for the light flickering
    public float lightOffDelay = 2.0f; // Delay before turning off the lights

    private void OnDrawGizmos()
    {
        // Visualize the activation radius with a wireframe sphere in the Unity editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }

    private void Start()
    {
        // Start the activation coroutine
        StartCoroutine(ActivateParticles());
    }

    private IEnumerator ActivateParticles()
    {
        while (true)
        {
            // Wait for 7 seconds
            yield return new WaitForSeconds(activationInterval);

            // Generate a random X and Z position within the activationRadius
            Vector2 randomPosition = Random.insideUnitCircle * activationRadius;
            Vector3 spawnPosition = new Vector3(randomPosition.x, fixedYPosition, randomPosition.y) + transform.position;

            // Instantiate the lights at the same position
            GameObject light1 = Instantiate(lightPrefab, spawnPosition, Quaternion.identity);
            GameObject light2 = Instantiate(lightPrefab, spawnPosition, Quaternion.identity);

            // Set the lights to be on
            SetLightState(light1.GetComponent<Light>(), true);
            SetLightState(light2.GetComponent<Light>(), true);

            // Instantiate the particle system at the random position
            GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);

            // Wait for the specified delay before turning off the lights
            yield return new WaitForSeconds(lightOffDelay);

            // Turn off the lights
            SetLightState(light1.GetComponent<Light>(), false);
            SetLightState(light2.GetComponent<Light>(), false);

            // Make sure to destroy the particle system and lights after some time (e.g., particle duration)
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                float particleDuration = ps.main.duration + ps.main.startLifetime.constantMax;
                Destroy(particle, particleDuration);
                Destroy(light1, particleDuration);
                Destroy(light2, particleDuration);
            }
        }
    }

    private void SetLightState(Light light, bool state)
    {
        light.enabled = state;
    }
}
