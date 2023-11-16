using UnityEngine;

public class ShultsAutoColorChangeScript : MonoBehaviour
{
    // Time between color changes in seconds
    public float timeBetweenColorChanges = .5f;

    // Timer to keep track of time elapsed
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Call the method to change color immediately
        ChangeColorRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to change color
        if (timer >= timeBetweenColorChanges)
        {
            // Call the method to change color randomly
            ChangeColorRandomly();

            // Reset the timer
            timer = 0f;
        }
    }

    // Method to change the color randomly
    void ChangeColorRandomly()
    {
        // Get the Renderer component of the GameObject
        Renderer rend = GetComponent<Renderer>();

        // Generate random values Color
        float randomR = Random.value;
        float randomG = Random.value;
        float randomB = Random.value;

        //  I Assign the random color to the material color
        rend.material.color = new Color(randomR, randomG, randomB);
    }
}
