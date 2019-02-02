using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteEnemyController : MonoBehaviour
{
    [SerializeField]
    protected GameObject Enemy;
    [SerializeField]
    protected ParticleSystem particle;
    [SerializeField]
    protected ParticleSystem particleWalk;
    protected GameObject player;
    protected GameObject game;
    protected Animator animator;
    protected float timer = 3f;
    protected float speed = -0.04f; // es positiva
    protected bool walk;
    protected bool init = false;
    protected int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        game = GameObject.Find("Game");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0 && init == false)
            timer -= Time.deltaTime;

        if (timer < 0)
        {
            init = true;
            walk = true;
            timer = 0;
            particle.Stop();
            particleWalk.Play();
        }
        if (walk == true)
        {
            UpdateAnimation("WhiteEnemyIdle");
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
        if (walk == false && init)
        {
            UpdateAnimation("WhiteEnemyStun");
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                particleWalk.Play();
                particle.Stop();
            }
            if (timer < 0 && lives == 0)
            {
                game.GetComponent<Game>().SetScore(50);
                Destroy(Enemy);
            }
        }
    }

    public void UpdateAnimation(string animator = null)
    {
        if (animator != null)
        {
            this.animator.Play(animator);
        }
    }
    public void Stunned()
    {
        lives--;
        particle.Play();
        particleWalk.Stop();
        if (lives < 0)
        {
            Destroy(Enemy);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        print(trigger.tag);
        if (walk == true)
        {
            if (trigger.tag == "Limit" || trigger.tag == "Enemy")
            {
                speed = -speed;
            }
            else if (trigger.tag == "Player")
            {
                player.GetComponent<PlayerController>().Damaged();
                speed = -speed;
            }
            else if (trigger.tag == "PlayerFeet")
            {
                Stunned();
                walk = false;
                timer = 4f;
            }
        }

        if (trigger.tag == "DoorBlue")
        {
            Destroy(Enemy);
        }
    }
}
