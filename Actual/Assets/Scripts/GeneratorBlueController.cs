﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBlueController : MonoBehaviour
{
    [SerializeField]
    protected GameObject enemyPrefab;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected BoxCollider2D box;
    [SerializeField]
    protected GameObject game;
    protected bool active = false;
    protected float timer = -1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active == false && timer < 0)
        {
            RepeatSummon();
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            active = false;
        }
        if ( game.GetComponent<Game>().GetGameState() == Game.GameState.Finished)
        {
            CancellSummon();
        }
    }

    void SummonEnemy()
    {
        active = true;
        Instantiate(enemyPrefab, new Vector2(-1.7f, 13f), Quaternion.identity);
    }

    void RepeatSummon()
    {
        box.enabled = true;
        animator.Play("ActiveBlue");
        InvokeRepeating("SummonEnemy", 0f, 13f);
        timer = 0;
    }

    void CancellSummon()
    {
        box.enabled = false;
        CancelInvoke("SummonEnemy");
        timer = 7f;
        animator.Play("DisabledBlue");
        game.GetComponent<Game>().SetScore(10);
    }
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "PlayerFeet" && box.isActiveAndEnabled == true)
        {
            CancellSummon();
        }
    }
}
