using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float JUMP_AMOUNT = 100f;

    private static Bird instance;
    public static Bird GetInstance()
    {
        return instance;
    }

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;

    private Rigidbody2D m_Rigidbody;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }
    private State state;

    private void Awake()
    {
        instance = this;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Rigidbody.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    state = State.Playing;
                    m_Rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    Jump();

                    if (OnStartedPlaying != null) 
                        OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
            case State.Dead:
                break;
        }
    }

    private void Jump()
    {
        m_Rigidbody.velocity = Vector2.up * JUMP_AMOUNT;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_Rigidbody.bodyType = RigidbodyType2D.Static;
        OnDied(this, EventArgs.Empty);
    }
}
