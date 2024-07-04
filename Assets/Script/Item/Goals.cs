using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goals : MonoBehaviour
{
    Animator animator;
    const string Idle = "Idle End";
    const string Begin = "Pressed End";
    string currentState;
    bool isHit = false;

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
            StartCoroutine(FinishLevel());
        }
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSecondsRealtime(1f);
        string sceneName = "Level Scene";
        SceneManager.LoadScene(sceneName);
        FindObjectOfType<GameManager>().isFinishLevel = true;
    }
}
