using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected GameObject gamePad;

    protected Vector2 MINCAMERA = new Vector2(-12.17744f, 0f);
    protected Vector2 MAXCAMERA = new Vector2(17.6f, 13.69704f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = player.transform.position.x;
        float posy = player.transform.position.y;

        transform.position = new Vector3(
            Mathf.Clamp(posX, MINCAMERA.x, MAXCAMERA.x),
                Mathf.Clamp(posy, MINCAMERA.y, MAXCAMERA.y),
                    transform.position.z);

        gamePad.transform.position = new Vector3(
            Mathf.Clamp(posX, MINCAMERA.x, MAXCAMERA.x),
                Mathf.Clamp(posy, MINCAMERA.y, MAXCAMERA.y),
                    gamePad.transform.position.z);
    }
}
