using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy: MonoBehaviour
{
    public float speed;
    public Vector3[] position;

    private int currentTarget;

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
}
