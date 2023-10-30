using System.Collections;                                                // put your notes here!
                                                // put your questions here!
using UnityEngine;                              // put your expletives here!

public class Methods : MonoBehaviour
{   
    [SerializeField]        // let us mess with this variable in the Inspector.
    GameObject go;

    [Header("Color Change Experiment")]
    [SerializeField]
    // camelCase capitalizes every word after the first one.
    float colorChangeInterval = 1f;

    [SerializeField]
    GameObject colorChangeObject;


    // Start is called before the first frame update
    void Start()
    {
        SayHello();
        Debug.Log("7 + 8 = " + AddTwoNumbers(7,8));
        int answer = AddTwoNumbers(400, 600);
        Debug.Log("400 + 600 = " + answer);

        StartCoroutine(ChangeColor(colorChangeObject, colorChangeInterval));
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("I exist!");

        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Wait(go));
        }
    }

    // let's talk about functions
    // functions have a return type, a name, parameters, and body of code.
    // functions are all about input and output.
    // return type is void, this returns nothing
    void SayHello() {
        Debug.Log("Hello");
    }

    // create a function that adds two numbers
    // and returns the sum.

    // return type is int
    // name is AddTwoNumbers
    // two parameters (inputs) named num1 and num2. (they are integers)
    int AddTwoNumbers(int num1, int num2) {
        // create an int named 'sum' that is equal to the two numbers.
        int sum = num1 + num2;
        // finally, return the sum.
        return sum;
    }


    // this coroutine will turn off a given gameobject using SetActive(false)
    // wait two seconds
    // and then turn it back on.

    // coroutines are called after a certain amount of time.
    IEnumerator Wait(GameObject go) {
        // stuff we do before we wait
        go.SetActive(false);
        yield return new WaitForSeconds(2f);
        // stuff we do, after we have waited.
        go.SetActive(true);
    }

    // this coroutine will change the color of a gameobject every half second.
    IEnumerator ChangeColor(GameObject givenObject, float interval = 0.5f) {
        // loop with a while() loop
        while(true) {
            // wait our seconds
            yield return new WaitForSeconds(interval);
            // then change color.
            // hypothetically, givenObject is the cube.
            givenObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }

    }
    
    
}
