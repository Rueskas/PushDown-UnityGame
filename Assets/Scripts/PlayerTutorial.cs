using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{ 
    protected float speed = 0.08f;
    protected enum PlayerState { Idle, Left, Right, Stuned };
    protected PlayerState playerState = PlayerState.Idle;
    
    protected Animator animator;
    protected Transform transformPlayer;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rb2D;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transformPlayer = GetComponent<Transform>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if (playerState == PlayerState.Left)
            {
                transformPlayer.position = new Vector3(transformPlayer.position.x - speed, transformPlayer.position.y, 0);
                UpdateAnimation("Run");
            }

            if (playerState == PlayerState.Right)
            {
                transformPlayer.position = new Vector3(transformPlayer.position.x + speed, transformPlayer.position.y, 0);
                 UpdateAnimation("Run");
            }
    }

    public void MoveLeft()
    {
            playerState = PlayerState.Left;
            sprite.flipX = true;
    }

    public void MoveRight()
    {
            playerState = PlayerState.Right;
            sprite.flipX = false;
    }

    public void Jump()
    {
                //Poner la velocidad inicial a 0 por si detecta una plataforma direccional no pueda dar otro salto con el impulso acumulado del anterior
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                rb2D.AddForce(new Vector2(0, 8.2f), ForceMode2D.Impulse);
                UpdateAnimation("Jump");
    }

    public void Stop()
    {
        playerState = PlayerState.Idle;
        if (playerState == PlayerState.Idle)
            {
                UpdateAnimation("Idle");
            }
    }

    public void UpdateAnimation(string animation = "null")
    {
        if (animation != "null")
            animator.Play(animation);
    }

}
