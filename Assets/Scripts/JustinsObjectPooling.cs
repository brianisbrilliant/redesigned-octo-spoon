using System.Collections;
using UnityEngine;

public class JustinsObjectPooling : MonoBehaviour
{
    public Queue hailstonePool = new Queue(); // Remove type argument

    [SerializeField] Rigidbody hailstonePrefab;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) { SpawnHailstone(); }
    }

    void SpawnHailstone()
    {
        Rigidbody hailstone;
        if (hailstonePool.Count > 0)
        {
            hailstone = (Rigidbody)hailstonePool.Dequeue(); // Cast the result to Rigidbody
            hailstone.gameObject.SetActive(true);
            hailstone.velocity = Vector3.zero;
            StartCoroutine(hailstone.GetComponent<JustinsHailstone>().Wait());
        }
        else
        {
            hailstone = Instantiate(hailstonePrefab).GetComponent<Rigidbody>(); // Cast to Rigidbody
            hailstone.gameObject.GetComponent<JustinsHailstone>().poolScript = this;
        }

        float spawnX = Random.Range(6f, 14f);
        float spawnY = 5f;
        float spawnZ = Random.Range(23f, 30f);

        hailstone.transform.position = new Vector3(spawnX, spawnY, spawnZ);
        hailstone.transform.rotation = Quaternion.identity;

        hailstone.AddForce(Vector3.down * 50, ForceMode.Impulse);
    }
}
