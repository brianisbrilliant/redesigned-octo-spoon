using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScrubber : MonoBehaviour
{
    [SerializeField] private string animationName;

    private Animator anim;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.speed = 0;
    }

    void Update() {
        anim.Play(animationName, -1, time);
    }

    public void ScrubAnimation(float value) {
        time = value;
    }
}
