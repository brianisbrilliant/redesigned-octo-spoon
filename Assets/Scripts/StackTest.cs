using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTest : MonoBehaviour
{
    // first in, last out.
    Stack numbers = new Stack();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) {
            // this adds an element to the end of the stack.
            numbers.Push(Random.value);
        }

        if(Input.GetKeyDown(KeyCode.D)) {
            // this displays all of the numbers in the stack.
            Debug.Log("<color=yellow>Displaying numbers</color>");
            foreach(float num in numbers) {
                if(num > .75f) {
                    Debug.Log("<color=green>" + num + "</color>");
                } else {
                    Debug.Log(num);
                }
                
            }
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            // this removes the last element from the stack
            if(numbers.Count > 0) {
                numbers.Pop();
            }
            
        }
    }
}
