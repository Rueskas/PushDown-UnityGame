using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreenController : MonoBehaviour
{
    [SerializeField]
    protected GameObject Enemy;
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected GameObject game;
    [SerializeField]
    protected ParticleSystem particle;
    protected Animator animator;
    protected float timer = 3f;
    protected float speed = 0.085f; // es positiva
    protected bool walk;
    protected bool init = false;

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
        }
        if (walk == true)
        {
            UpdateAnimation("EnemyGreenWalk");
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
        if (walk == false && init)
        {
            UpdateAnimation("EnemyGreenIdle");
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                game.GetComponent<Game>().SetScore(10);
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

    void OnTriggerEnter2D(Collider2D trigger)
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
            walk = false;
            particle.Play();
            timer = 3f;
        }
        else if (trigger.tag == "DoorBlue")
        {
            Destroy(Enemy);
        }
    }
}
