using UnityEngine;

public class BoxItems : MonoBehaviour
{
    // [SerializeField] int hitLimit = 0;
    // int hitCount = 0;
    Animator animator;
    bool isHit = false;
    const string Box_Idle = "Idle";
    const string Box_Hit = "Hit";
    string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeAnimationState(Box_Idle);
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isHit)
        {
            ChangeAnimationState(Box_Hit);
            isHit = true;
            // hitCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHit = false;
            ChangeAnimationState(Box_Idle);
        }
    }

    // void CheckHitLimit()
    // {
    //     if (hitCount == hitLimit)
    //     {
    //         Debug.Log("Break");
    //     }
    // }
}
