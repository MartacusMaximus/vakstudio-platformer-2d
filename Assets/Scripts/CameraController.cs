using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 targetPoint = Vector3.zero;

    public PlayerMovement player;

    public float moveSpeed;

    public float lookAheadDistance = 5f, lookAheadSpeed = 3f;

    private float lookOffset;

    private bool isFalling;
    public float maxVertOffset = 5f;


    void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //basic movement

        if(player.isGrounded)
        {
            targetPoint.y = player.transform.position.y;
        }

        if(transform.position.y - player.transform.position.y > maxVertOffset)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            targetPoint.y = player.transform.position.y;
            if (player.isGrounded)
            {
                isFalling = false;
            }
        }

        if(player.rb.linearVelocity.x > 0f)
        {

            lookOffset = Mathf.Lerp(lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        if (player.rb.linearVelocity.x < 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        targetPoint.x = player.transform.position.x + lookOffset;

        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }
}
