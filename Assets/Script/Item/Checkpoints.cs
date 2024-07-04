using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    Animator animator;
    const string Idle = "Idle Checkpoint";
    const string Begin = "Start Checkpoint";
    const string End = "End Checkpoint";
    string currentState;
    bool isHit = false;
    bool isAlreadyCheck = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeAnimationState(Idle);
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState)
            return;
        animator.Play(newState);
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isHit)
        {
            ChangeAnimationState(Begin);
            isHit = true;
            if (isAlreadyCheck == false)
            {
                Vector2 newPoint = this.transform.position;
                FindObjectOfType<PlayerController>().currentPos = newPoint;
                isAlreadyCheck = true;
            }
        }
    }

    // Anim Events
    void EndAnimation()
    {
        ChangeAnimationState(End);
    }
}
