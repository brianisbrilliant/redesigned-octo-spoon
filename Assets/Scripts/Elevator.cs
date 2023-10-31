using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] 
    private Vector3 startPosition;
    [SerializeField] 
    private Vector3 endPosition;
    [SerializeField]
    private float intervalInSeconds = 5;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    private bool looping = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting the Elevator.");
        // startPosition = new Vector3(0, 0, 0);
        // endPosition = new Vector3(0, 5, 0);
        StartCoroutine(Move());
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R) && !looping) {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move() {
        float counter = 0;
        while(counter < intervalInSeconds) {
            counter += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(counter / intervalInSeconds));
            yield return new WaitForEndOfFrame();
        }
        if(looping) {
            StartCoroutine(Move());
        }
    }
}
