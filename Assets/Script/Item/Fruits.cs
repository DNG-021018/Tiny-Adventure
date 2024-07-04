using UnityEngine;

public class Fruits : MonoBehaviour
{
    Animator animator;
    [SerializeField] string fruits = "";
    const string collected = "Collected";
    string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeAnimationState(fruits);
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
            ChangeAnimationState(collected);
        }
    }

    // Anim Events
    void DestroyFruits()
    {
        Destroy(this.gameObject);
    }
}
