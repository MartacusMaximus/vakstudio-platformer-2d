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

    public PolygonCollider2D cameraBounds;
    private float camHalfWidth;
    private float camHalfHeight;


    void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

    }

    private Vector3 ClampToBounds(Vector3 pos)
    {
        Bounds b = cameraBounds.bounds;

        float minX = b.min.x + camHalfWidth;
        float maxX = b.max.x - camHalfWidth;
        float minY = b.min.y + camHalfHeight;
        float maxY = b.max.y - camHalfHeight;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        return pos;
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

        Vector3 newPos = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.position = ClampToBounds(newPos);

    }
}
