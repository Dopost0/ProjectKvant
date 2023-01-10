using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy: MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;

    public float speed;
    public Vector3[] position;

    private int currentTarget;

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    public void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, position[currentTarget], speed);

        if(transform.position == position[currentTarget])
        {
            if(currentTarget < position.Length - 1)
            {
                currentTarget++;
            }else
            {
                currentTarget = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void EnemyWolk()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed + Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }
}

public enum States1
{
    EnemyIdle,
    EnemyWolk
}
