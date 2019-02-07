using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected float speed = 0.06f;
    protected int lives = 3;
    protected float timer = 3f;
    protected bool grounded;
    protected bool playing = false;
    protected bool escape = false;
    protected bool pushDown = true;
    protected Vector3 startPosition;
    protected enum PlayerState { Idle, Left, Right, Stuned };
    protected PlayerState playerState = PlayerState.Idle;

    protected Animator animator;
    protected Transform transformPlayer;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rb2D;
    protected Collider2D coll;
    protected AudioSource clips;
    [SerializeField]
    protected GameObject game;
    [SerializeField]
    protected AudioClip jump;

    public int GetLives()
    {
        return lives;
    }

    public void RestLive()
    {
        this.lives--;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transformPlayer = GetComponent<Transform>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        clips = GetComponent<AudioSource>();

        startPosition = transformPlayer.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playing = game.GetComponent<Game>().GetGameState() == Game.GameState.Playing;
        if (playing)
        {
            if (playerState == PlayerState.Left)
            {
                transformPlayer.position = new Vector3(transformPlayer.position.x - speed, transformPlayer.position.y, 0);

                if (grounded && rb2D.velocity.y <= 0.2f && rb2D.velocity.y > -0.2f)
                    UpdateAnimation("Run");
            }

            if (playerState == PlayerState.Right)
            {
                transformPlayer.position = new Vector3(transformPlayer.position.x + speed, transformPlayer.position.y, 0);

                if (grounded && rb2D.velocity.y <= 0.2f && rb2D.velocity.y > -0.2f)
                    UpdateAnimation("Run");
            }
            if (grounded == false && rb2D.velocity.y < 0 && playerState != PlayerState.Stuned)
            {
                UpdateAnimation("Fall");
            }
            if (playerState == PlayerState.Stuned && escape == false)
            {
                if (lives >= 0)
                {
                    UpdateAnimation("Stun");
                    timer -= Time.deltaTime;
                    if (timer < 0)
                    {
                        playerState = PlayerState.Idle;
                        Stop();
                        timer = 3f;
                    }
                }
                else
                {
                    UpdateAnimation("Death");
                    game.GetComponent<Game>().SetGameState(Game.GameState.Finished);
                }
            }
            if (playerState == PlayerState.Stuned && lives <= 0 && escape == true)
            {
                UpdateAnimation("Stun");
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    playerState = PlayerState.Idle;
                    Stop();
                    timer = 3f;
                    escape = false;
                }
            }
            else if (playerState == PlayerState.Stuned && lives > 0 && escape == true)
            {
                escape = false;
                playerState = PlayerState.Idle;
            }
        }
    }

    public void MoveLeft()
    {
        if (playerState != PlayerState.Stuned && playing)
        {
            playerState = PlayerState.Left;
            sprite.flipX = true;
        }
    }

    public void MoveRight()
    {
        if (playerState != PlayerState.Stuned && playing)
        {
            playerState = PlayerState.Right;
            sprite.flipX = false;
        }
    }

    public void Jump()
    {
        if (playerState != PlayerState.Stuned && playing)
        {
            if (grounded)
            {
                //Poner la velocidad inicial a 0 por si detecta una plataforma direccional no pueda dar otro salto con el impulso acumulado del anterior
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                rb2D.AddForce(new Vector2(0, 8.2f), ForceMode2D.Impulse);
                UpdateAnimation("Jump");
                clips.clip = jump;
                clips.Play();
            }
        }

    }

    public void PushDown()
    {
        if (playerState != PlayerState.Stuned && playing && pushDown)
        {
            if (grounded == true)
                coll.enabled = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
            rb2D.AddForce(new Vector2(0, -8.2f), ForceMode2D.Impulse);
            UpdateAnimation("Fall");
        }
    }

    public void Stop()
    {
        if (playerState != PlayerState.Stuned && playing)
        {
            playerState = PlayerState.Idle;
            if (grounded)
            {
                UpdateAnimation("Idle");
            }
        }
    }

    public void UpdateAnimation(string animation = "null")
    {
        if (animation != "null")
            animator.Play(animation);
    }

    public void Damaged()
    {
        if (playerState != PlayerState.Stuned && playing)
        {
            RestLive();
            playerState = PlayerState.Stuned;
        }
    }
    public void Escaped()
    {
        if (playerState != PlayerState.Stuned && playing)
        {
            escape = true;
            playerState = PlayerState.Stuned;
            if (lives > 0)
                RestLive();

        }
    }

    void OnCollisionStay2D()
    {
        grounded = true;
        animator.SetBool("Grounded", true);
    }

    void OnCollisionExit2D()
    {
        grounded = false;
        animator.SetBool("Grounded", false);
        pushDown = true;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Ground")
        {
            coll.enabled = true;
        }
        if (trigger.tag == "Sea" && playerState != PlayerState.Stuned)
        {
            Damaged();
            transformPlayer.transform.position = startPosition;
        }
    }
    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.tag == "Generator" || trigger.tag == "MainPlatform" || trigger.tag == "Enemy")
        {
            pushDown = false;
        }
    }
}
