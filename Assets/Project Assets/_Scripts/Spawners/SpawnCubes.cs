// Brian Foster Interactive Scripting Fall 2023
// this code will spawn cubes at random locatons around the map


// challenge #2 
// create an array of names
// assign a random name to each new cube.
// bonus (append a new number for each duplicate name. e.g. tom2, betty4) (i won't even try it)


// homework: use an array of colors to choose the random color of your cubes. (Bonus: every name has the same color.)
// homework: change collectCubes() to a coroutine that uses WaitUntilEndOfFrame();


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{

    // creating an array of string variables named "names" and supplying three names.
    public string[] names = {"Harry", "Hermione", "Ron"};

    // names[0] = Harry
    // names[1] = Hermione
    // names[2] = Ron
    // names[3] = ????      // random.range(int,int) is maxExclusive

    [SerializeField]
    private Color[] colors;


    [SerializeField]
    bool debug = false;

    // public (or editable) variables at the top
    [SerializeField]
    private GameObject prefabCube;

    [SerializeField]
    private int totalCubes = 25;

    // can you change the timer interval between spawns?
    [SerializeField]
    [Range(0.1f, 2f)]       // this is an attribute, it 
    private float spawnCubeInterval = 1f;

    [SerializeField]
    private float spawnPositionRange = 40;


    // private variables at the bottom
    private bool canStartSpawnLoop = true;


    // Start is called before the first frame update
    void Start(){
        Debug.Log("Press Shift+0 to enable debug mode.");
        if(debug) Debug.Log("<color=cyan>Press G to spawn cubes.</color>");
        if(debug) Debug.Log("<color=magenta>Press B to collect cubes.</color>");
        if(debug) Debug.Log("The first name in the array of names is " + names[0]);
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.LeftShift)) {
            if(Input.GetKeyDown(KeyCode.Alpha0)) {
                debug = !debug;     // toggle the boolean
                Debug.Log("Debug mode is now " + debug);
            }
        }

        if(Input.GetKeyDown(KeyCode.G)) {
            if(canStartSpawnLoop == true) {
                StartCoroutine(SpawnLoop());
            }
            else {
                if(debug) Debug.Log("<color=red>You cannot start a new loop</color> until the old one is finished.");
            }
            
        }
        
        if(Input.GetKeyDown(KeyCode.B)) {
            StartCoroutine(CollectCubes());
        }
    }

    // spawn a single cube with a random color, in a random position, with a rigidbody.
    GameObject SpawnCube() {
        if(debug) Debug.Log("<color=green>Starting SpawnCube() function.</color>");

        if(debug) Debug.Log("creating cube from prefab 'prefabCube'");
        GameObject cube = Instantiate(prefabCube);
        // move the cube to a random x position of -40, 40
        // move the cube to a y position of 2
        // move the cube to a random z position of -40, 40
        // can you change (in the inspector) the size of the random location?

        // changing the name to a random name
        int index = Random.Range(0,names.Length);
        cube.name = names[index];
        

        Vector3 newPos = new Vector3(
                                Random.Range(-spawnPositionRange,spawnPositionRange), 
                                Random.Range(1.5f,2.5f), 
                                Random.Range(-spawnPositionRange, spawnPositionRange)
                            );

        if(debug) Debug.Log("setting cube position to " + newPos);
        cube.transform.position = newPos;

        // log error (turn on error pause)
        // if(debug) Debug.LogError("Pausing here to look at the position of the cube.");
        
        // can you change the color of each cube?
        Color newColor = Random.ColorHSV();
        if(debug) Debug.Log("setting color to " + newColor);
        // the old way
        // cube.GetComponent<Renderer>().material.color = newColor;

        // the new way, use the colors from our array of colors.
        // names[1] always gets colors[1].
        cube.GetComponent<Renderer>().material.color = colors[index];

        // can you add physics to the cubes? Either in code, or in the prefab?
        if(debug) Debug.Log("adding Rigidbody component.");
        // cube.AddComponent(typeof(Rigidbody));


        if(debug) Debug.Log("<color=red>End of SpawnCube() function.</color>");
        return cube;
    }


    // this function and it's loop all happen on one frame.
    IEnumerator CollectCubes() {
        // find all cubes in scene // add them to an array and add them to an array
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        // move all of the to the same location (0,2,0)
        int i = 0;
        while(i < cubes.Length) {
            cubes[i].transform.position = new Vector3(0,2,0);
            i += 1;
            yield return new WaitForEndOfFrame();
        }
    }

    // continue to spawn cubes until enough have been spawned
    IEnumerator SpawnLoop() {
        // don't allow the player to spawn more cubes until this coroutine is finished.
        canStartSpawnLoop = false;

        // can you reset the SpawnLoop code and run it again, from a key press?
        int counter = 0;
        // can you change how many cubes are created, from the inspector?
        while (counter < totalCubes) {
            // counter = counter + 1;
            counter += 1;       // adds 1 to counter.
            SpawnCube();
            // can you change the timer interval between spawns?
            yield return new WaitForSeconds(spawnCubeInterval);
        }
        
        // don't allow the player to spawn more cubes until this coroutine is finished.
        canStartSpawnLoop = true;
    }
}


