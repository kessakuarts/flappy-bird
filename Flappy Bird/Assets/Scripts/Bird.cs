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

    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        instance = this;
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
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
