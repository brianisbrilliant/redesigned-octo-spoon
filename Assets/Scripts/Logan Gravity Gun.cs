using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityGun : MonoBehaviour, IItem
{
    public GameObject cubePrefab;
    public float gravityStrength = 10f;
    public float floatingForce = 5f;

    private Rigidbody rb;
    private List<GameObject> spawnedCubes = new List<GameObject>();
    private bool isFloating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the item!");
        }
    }

    void Update()
    {
        // Check for right mouse button down
        if (Input.GetMouseButtonDown(1))
        {
            isFloating = true;

            // Influence the existing cubes spawned by the primary action
            foreach (GameObject cube in spawnedCubes)
            {
                if (cube != null)
                {
                    // Change its color
                    InfluenceCube(cube.transform);

                    // Apply a gravitational force to pull the cube
                    PullCube(cube.transform);
                }
            }
        }

        // Check for right mouse button release
        if (Input.GetMouseButtonUp(1))
        {
            isFloating = false;
        }
    }

    void FixedUpdate()
    {
        // Apply floating force while right mouse button is held down
        if (isFloating)
        {
            foreach (GameObject cube in spawnedCubes)
            {
                if (cube != null)
                {
                    Rigidbody cubeRb = cube.GetComponent<Rigidbody>();
                    if (cubeRb != null)
                    {
                        // Get the mouse position in the world space
                        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

                        // Adjust the floating force direction based on the mouse position
                        Vector3 forceDirection = mousePosition - cube.transform.position;
                        cubeRb.AddForce(forceDirection.normalized * floatingForce, ForceMode.Force);
                    }
                }
            }
        }
    }

    public void Pickup(Transform hand)
    {
        // Attach the item to the hand
        transform.parent = hand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Disable the rigidbody while being held
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public void Drop()
    {
        // Detach the item from the hand
        transform.parent = null;

        // Enable the rigidbody when dropped
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    public void PrimaryAction()
    {
        // Spawn a cube, change its color, and destroy it after 10 seconds
        SpawnCube(cubePrefab, 10f);
        Debug.Log("Cube spawned!");

        // You can perform additional actions with the spawned cube if needed
    }

    public void SecondaryAction()
    {
        // Influence the existing cubes spawned by the primary action
        foreach (GameObject cube in spawnedCubes)
        {
            if (cube != null)
            {
                // Change its color
                InfluenceCube(cube.transform);

                // Apply a gravitational force to pull the cube
                PullCube(cube.transform);
            }
        }
    }

    private void InfluenceCube(Transform cubeTransform)
    {
        // Change the color randomly
        Renderer renderer = cubeTransform.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    private void PullCube(Transform cubeTransform)
    {
        // Add a gravitational force to pull the cube
        Rigidbody cubeRb = cubeTransform.GetComponent<Rigidbody>();
        if (cubeRb != null)
        {
            Vector3 pullDirection = transform.position - cubeTransform.position;
            cubeRb.AddForce(pullDirection.normalized * gravityStrength, ForceMode.Force);
        }
    }

    private void SpawnCube(GameObject prefab, float lifetime)
    {
        // Spawn the object forward from the gun
        Transform gunTransform = transform;
        Vector3 spawnPosition = gunTransform.position + gunTransform.forward * 2f;
        Quaternion spawnRotation = gunTransform.rotation;

        // Instantiate the object
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, spawnRotation);

        // Add Rigidbody if it's a cube
        Rigidbody cubeRb = spawnedObject.GetComponent<Rigidbody>();
        if (cubeRb == null)
        {
            cubeRb = spawnedObject.AddComponent<Rigidbody>();
        }

        // Add the spawned cube to the list
        spawnedCubes.Add(spawnedObject);

        // Destroy it after the specified lifetime
        Destroy(spawnedObject, lifetime);
    }
}
