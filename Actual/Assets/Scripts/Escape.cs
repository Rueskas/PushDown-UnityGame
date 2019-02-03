using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    [SerializeField]
    protected GameObject Player;
    [SerializeField]
    protected GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Enemy")
        {
            Player.GetComponent<PlayerController>().Escaped();
        }
    }
}
