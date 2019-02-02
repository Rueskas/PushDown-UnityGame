using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlueController : MonoBehaviour
{
    [SerializeField]
    protected GameObject Enemy;
    protected GameObject player;
    [SerializeField]
    protected ParticleSystem particle;
    [SerializeField]
    protected GameObject game;
    protected Animator animator;
    protected float timer = 3f;
    protected float speed = 0.06f; // es positiva
    protected bool walk;
    protected bool init = false;
    public void SetWalk( bool walk)
    {
        this.walk = walk;
    }
    public void SetInit(bool init)
    {
        this.init = init;
    }
    public void SetTimer(float timer)
    {
        this.timer = timer;
    }
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
        if( walk == true )
        {
            UpdateAnimation("EnemyBlueWalk");
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
        if (walk == false && init)
        {
            UpdateAnimation("EnemyBlueIdle");
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Destroy(Enemy);
                game.GetComponent<Game>().SetScore(5);
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
        if (trigger.tag == "Limit" || trigger.tag == "Enemy" )
        {
            speed = -speed;
        }
        else if ( trigger.tag == "Player" )
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
