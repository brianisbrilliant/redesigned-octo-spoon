using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurpBullet : MonoBehaviour
{
    // public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    //    animator = GetComponent<Animator>();
    }

public void OnCollisionEnter(Collision collision)
{
    GameObject prefab = Resources.Load ("Burpbullet") as GameObject;
    GameObject Burpbullet = Instantiate (prefab) as GameObject;
    Burpbullet.transform.position = transform.position;
    Destroy (Burpbullet, 2);
    ReturnToPool();
}
private void ReturnToPool()
{
    LanesObjectPooler.Instance.ReturnToPool(gameObject);
}
}
