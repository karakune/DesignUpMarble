using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public float turnSpeed;
    private Vector3 playerPosition;


    // Use this for initialization
    void Start()
    {
        UpdatePlayerPosition();
        offset = transform.position - playerPosition;
        transform.LookAt(player.transform);
    }

    void Update ()
    {
        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition ()
    {
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerPosition + offset;
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Time.deltaTime * -turnSpeed, 0, 0);
            transform.LookAt(player.transform);
            offset = transform.position - playerPosition;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Time.deltaTime * turnSpeed, 0, 0);
            transform.LookAt(player.transform);
            offset = transform.position - playerPosition;
        }
    }
}
