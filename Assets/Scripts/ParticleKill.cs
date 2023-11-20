using UnityEngine;

public class ParticleKill : MonoBehaviour
{
    void Start(){
        TryGetComponent(out ParticleSystem particle);
        Destroy(gameObject, particle.main.duration * 10);        
    }
}
