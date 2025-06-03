using UnityEngine;

public class AnimatorSetup : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false; // Usiamo Rigidbody
    }
}
