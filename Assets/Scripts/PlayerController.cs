using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Compensate for the camera angle
        //   Get the part of camera forward that is on the XZ plane as a normalzied vector
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        //   Get the part of camera right that is on the XZ plane as a normalzied vector
        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        if (canMove)
        {
            // Move the character in relation to the camera
            Vector3 movementX = camRight * Input.GetAxis("Horizontal");
            Vector3 movementZ = camForward * Input.GetAxis("Vertical");
            Vector3 movement = movementX + movementZ;
            rb.AddForce(movement * speed);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name.Contains("Plane"))
        {
            canMove = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name.Contains("Plane"))
        {
            canMove = false;
        }
    }
}
