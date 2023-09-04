// Brian Foster Interactive Scripting Fall 2023
// this code will spawn cubes at random locatons around the map


// can you change the color of each cube?
// can you change how many cubes are created, from the inspector?
// can you change the timer interval between spawns?
// can you change (in the inspector) the size of the random location?
// can you add physics to the cubes? Either in code, or in the prefab?
// can you reset the SpawnLoop code and run it again, from a key press?



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField]
    GameObject prefabCube;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn a cube
    GameObject SpawnCube() {
        GameObject cube = Instantiate(prefabCube);
        // move the cube to a random x position of -40, 40
        // move the cube to a y position of 2
        // move the cube to a random z position of -40, 40
        cube.transform.position = new Vector3(Random.Range(-40, 40), 2, Random.Range(-40, 40));

        return cube;
    }

    // the loop function
    IEnumerator SpawnLoop() {
        int counter = 0;
        while (counter < 25) {
            // counter = counter + 1;
            counter += 1;       // adds 1 to counter.
            SpawnCube();
            yield return new WaitForSeconds(1f);
        }
    }
}


