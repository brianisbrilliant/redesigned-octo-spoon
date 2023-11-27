using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevinsBullet : MonoBehaviour
{
    public ObjectPoolingTest poolScript;

    void Start()
    {
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        DisableBullet();

    }

  //void OnCollisionEnter(Collision other) { 
      //DisableBullet();
  //}
    void DisableBullet() {
        if (this.gameObject.activeSelf == false) return;

            poolScript.bulletPool.Enqueue(this.GetComponent<Rigidbody>());
            this.gameObject.SetActive(false);
        }
    }