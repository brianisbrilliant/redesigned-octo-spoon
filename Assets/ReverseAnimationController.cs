using UnityEngine;

public class ReverseAnimationController : MonoBehaviour
{
    public AnimationClip forwardAnimation;
    public float timeToWait = 3f;

    private Animator animator => GetComponent<Animator>();

    private void Start() {
        PlayForward();
    }

    private void PlayForward(){
        animator.SetFloat("Speed", 1);
        animator.Play(forwardAnimation.name);
        Invoke(nameof(PlayBackward), timeToWait);
    }

    private void PlayBackward(){
        animator.SetFloat("Speed", -1);
        animator.Play(forwardAnimation.name);
        Invoke(nameof(PlayForward), timeToWait);
    }
}
