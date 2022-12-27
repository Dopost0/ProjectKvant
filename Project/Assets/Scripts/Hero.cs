using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int hp = 5;
    [SerializeField] private float jumpforce = 10f;


    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isGround = false;

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

    }

    private void Run()
    {
        if (isGround) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed + Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGround = collider.Length > 1;

        if (!isGround) State = States.jump;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (isGround) State = States.idle;
        if (Input.GetButtonDown("Jump") && isGround)
            Jump();
        if (Input.GetButton("Horizontal"))
            Run();
    }
}

public enum States
{
    idle,
    run,
    jump
}