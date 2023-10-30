using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    // What variables do we want to know about?
    // integers are whole numbers, positive or negative
    int counter = 0;
    // floating point numbers allow for decimal points, accurate to 7-significant figures.
    float charge = 0;

    string enemyName = "Darth Vader";

    // press the left mouse button to open or close the door.
    bool doorIsOpen = false;        // always name your booleans as a statement.

    // public variables can be edited in the inspector.
    public GameObject door;

    Vector3 spawnPoint = new Vector3(0f, 2f, 0f);     // where do we spawn the box?


    // let's spawn a ball five times a second.
    [SerializeField]
    Transform ballSpawn;

    float ballSpawnTimer = 0;
    float ballSpawnInterval = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        // make sure the code is running
        // this.gameObject.name = enemyName;
        Debug.Log("The Variables script is running on the " + this.gameObject.name + " gameobject.");

        // if the door is not assigned, look for it in the scene, and assign the first
        // put it all on one line if you want to!
        if(door == null) { door = GameObject.Find("Door"); }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            counter++;
            Debug.Log("The space bar has been pressed " + counter + " times.");
        }   

        if(Input.GetKey(KeyCode.C))
        {
            charge += Time.deltaTime;
            // Debug.Log("Charge is " + charge + ".");      // comment out code with Control + /
        }
        if(Input.GetKeyUp(KeyCode.C)) {
            Debug.Log("Charge is " + charge + ".");
            charge = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            if(doorIsOpen) {
                door.SetActive(false);
            }
            else {
                door.SetActive(true);
            }
            // true = NOTtrue; or false = NOTfalse.
            doorIsOpen = !doorIsOpen;
        }

        if(Input.GetKey(KeyCode.B)) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // .... what do we call this cube that we created?
            cube.transform.position = spawnPoint;
            cube.AddComponent<Rigidbody>();
        }

        // ball spawn timer
        ballSpawnTimer += Time.deltaTime;
        if(ballSpawnTimer > ballSpawnInterval) {
            SpawnBall();
            // reset the timer
            ballSpawnTimer = 0;
        }
    }




    // new function
    // return type of void
    // named SpawnBall()
    void SpawnBall() {
        // spawn a ball
        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // setting the ball's position
        ball.transform.position = ballSpawn.position;
        // randomize ball position a little bit
        ball.transform.position = (Random.insideUnitSphere * .2f) + ballSpawn.position;
        // setting the ball scale
        ball.transform.localScale = Vector3.one * 0.2f;
        // add rigidbody
        ball.AddComponent(typeof(Rigidbody));
        ball.AddComponent(typeof(Light));
    }
}
