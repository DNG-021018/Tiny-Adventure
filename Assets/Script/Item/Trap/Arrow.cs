using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Animator animator;
    const string IDLE = "Idle";
    const string HIT = "Hit";
    string currentState;
    bool isHit = false;
    Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeAnimationState(IDLE);
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
            isHit = true;
            ChangeAnimationState(HIT);
            Invoke(nameof(Enable), 5.0f);
        }
    }

    // Anim Events
    void Disable()
    {
        if (isHit == true)
        {
            this.gameObject.SetActive(false);
            isHit = false;
        }
    }

    void Enable()
    {
        this.gameObject.SetActive(true);
        ChangeAnimationState(IDLE);
    }
}
