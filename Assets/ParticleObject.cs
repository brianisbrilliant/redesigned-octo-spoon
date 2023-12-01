using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    private Queue<ParticleObject> pool;
    private float particleTime;

    // Constructor incase we create this class directly
    public ParticleObject(float liveTime, Queue<ParticleObject> particleObjPool){
        pool = particleObjPool;
        particleTime = liveTime;
    }

    public void SetupParticleObj(Queue<ParticleObject> particleObjPool, float liveTime){
        pool = particleObjPool;
        particleTime = liveTime;
        PlayParticle();
    } 

    public void PlayParticle(){
        StartCoroutine(ParticleVisibleTimer());
    }

    public IEnumerator ParticleVisibleTimer(){
        yield return new WaitForSeconds(particleTime);
        DisableParticle();
    }

    private void DisableParticle(){
        if(gameObject.activeSelf == false) return;

        pool.Enqueue(this);
        gameObject.SetActive(false);
    }
}
