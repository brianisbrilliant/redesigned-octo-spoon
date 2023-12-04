using System.Collections;
using UnityEngine;

public class JustinsHailstone : MonoBehaviour
{
    public JustinsObjectPooling poolScript;

    void Start()
    {
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        DisableHailstone();
    }

    void DisableHailstone()
    {
        if (!gameObject.activeSelf) return;

        poolScript.hailstonePool.Enqueue(GetComponent<Rigidbody>());
        gameObject.SetActive(false);
    }
}
