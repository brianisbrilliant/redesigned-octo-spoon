using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseAnimationController : MonoBehaviour
{
    public AnimationClip forwardAnimation;

    private Animator animator => GetComponent<Animator>();

    private void Start() {
        PlayForward();
    }

    private void PlayForward(){
        animator.SetFloat("Speed", 1);
        animator.Play(forwardAnimation.name);
        Invoke(nameof(PlayBackward), 5f);
    }

    private void PlayBackward(){
        animator.SetFloat("Speed", -1);
        animator.Play(forwardAnimation.name);
        Invoke(nameof(PlayForward), 5f);
    }
}
