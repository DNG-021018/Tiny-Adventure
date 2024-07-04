using UnityEngine;

public class StartPoints : MonoBehaviour
{
    Animator animator;
    const string Idle = "Idle Start";
    const string Begin = "Movement Start";
    string currentState;

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
        if (other.gameObject.tag == "Player")
        {
            ChangeAnimationState(Begin);
        }
    }

    public void EndAnimation()
    {
        ChangeAnimationState(Idle);
    }
}
