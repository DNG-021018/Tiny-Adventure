using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRockHead : MonoBehaviour
{
    Animator animator;
    string currentState;
    const string IDLE = "Idle";
    const string HIT_RIGHT = "RightHit";
    const string HIT_LEFT = "LeftHit";
    const string HIT_TOP = "TopHit";
    const string HIT_BOT = "BotHit";

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
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 otherPosition = collision.transform.position;
        Debug.Log("otherPosition" + otherPosition);
        Vector2 thisPosition = transform.position;
        Debug.Log("thisPosition" + thisPosition);
        float deltaX = otherPosition.x - thisPosition.x;
        Debug.Log("deltaX" + deltaX);
        float deltaY = otherPosition.y - thisPosition.y;
        Debug.Log("deltaY" + deltaY);

        if (deltaX < 0)
        {
            ChangeAnimationState(HIT_RIGHT);
        }
        else
        {
            ChangeAnimationState(HIT_LEFT);
        }

        if (deltaY < 0)
        {
            Debug.Log("Y lon hon X");
            ChangeAnimationState(HIT_TOP);
        }
        else
        {
            ChangeAnimationState(HIT_BOT);
        }
    }

}
