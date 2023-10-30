using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPlatform : MonoBehaviour
{
    LineRenderer lr;            // a reference to the line renderer
    Transform platformBase, start, end;       // a reference to the start and end gameobjects.

    [SerializeField] float intervalInSeconds = 2;
    [SerializeField] AnimationCurve curve;
    [SerializeField] bool looping = true;
    [SerializeField] bool updateBeam = false;

    // Start is called before the first frame update
    void Start()
    {
        platformBase = this.transform.GetChild(0);  // assign platform base
        start = this.transform.GetChild(1);         // assign start
        end = this.transform.GetChild(2);           // assign end

        lr = this.gameObject.GetComponent<LineRenderer>();      // assign lr
        if (lr)
        {                                                // if lr exists, assign the positions.
            lr.SetPosition(0, start.position);
            lr.SetPosition(1, end.position);
        }

        StartCoroutine(Move());         // start the move coroutine.
    }

    // Update is called once per frame
    void Update()
    {
        // every frame that we move, we change the beam's end location to be the bottom of the platform.
        if (updateBeam)
        {
            lr.SetPosition(1, platformBase.position);
        }
    }

    IEnumerator Move()
    {
        float counter = 0;

        // move from start to end
        while (counter < intervalInSeconds)
        {
            counter += Time.deltaTime / intervalInSeconds;
            platformBase.position = Vector3.Lerp(start.position, end.position, curve.Evaluate(counter));
            yield return new WaitForEndOfFrame();
        }

        // wait 2 seconds
        yield return new WaitForSeconds(2);

        // move from end to start.
        while (counter > 0)
        {
            counter -= Time.deltaTime / intervalInSeconds;
            platformBase.position = Vector3.Lerp(start.position, end.position, curve.Evaluate(counter));
            yield return new WaitForEndOfFrame();
        }

        // if you want to loop, loop again.
        if (looping)
        {
            StartCoroutine(Move());
        }
    }
}