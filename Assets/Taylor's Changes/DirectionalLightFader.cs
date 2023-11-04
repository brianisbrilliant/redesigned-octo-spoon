using System.Collections;
using UnityEngine;

public class DirectionalLightFader : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;

    [SerializeField]
    private float fadeDuration = 5.0f; // Set the fade duration in the inspector

    private float initialIntensity;
    private bool isFading = false;

    private void Start()
    {
        if (directionalLight == null)
        {
            Debug.LogError("Directional Light component not assigned!");
            enabled = false;
            return;
        }

        initialIntensity = directionalLight.intensity;
    }

    private void Update()
    {
        if (!isFading && directionalLight.intensity > 0)
        {
            StartCoroutine(FadeDirectionalLight());
        }
    }

    private IEnumerator FadeDirectionalLight()
    {
        isFading = true;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            directionalLight.intensity = Mathf.Lerp(initialIntensity, 0, t);
            yield return null;
        }

        directionalLight.intensity = 0;
        isFading = false;
    }
}
