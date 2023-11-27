using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartBulletExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

public void OnCollisionEnter(Collision collision)
{
    GameObject prefab = Resources.Load ("Fartbullet") as GameObject;
    GameObject Fartbullet = Instantiate (prefab) as GameObject;
    Fartbullet.transform.position = transform.position;
    Destroy (Fartbullet, 8);
   // Destroy (gameObject);
   ReturnToPool();
}
private void ReturnToPool()
{
    LanesObjectPooler.Instance.ReturnToPool(gameObject);
}
}
