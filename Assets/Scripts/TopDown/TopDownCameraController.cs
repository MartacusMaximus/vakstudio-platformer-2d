using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public TopDownMovement moveScript;

    [Header("Look Ahead")]
    public float lookAheadDistance = 2f;
    public float lookAheadSpeed = 4f;

    private Vector3 targetPos;
    private Vector3 lookOffset;

    [Header("Camera Bounds")]
    public PolygonCollider2D cameraBounds;
    private Bounds worldBounds;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (cameraBounds != null)
        {
            worldBounds = cameraBounds.bounds;
        }
    }

    void LateUpdate()
    {
        // READ CURRENT MOVEMENT INPUT FROM TopDownMovement
        Vector2 moveInput = moveScript.moveInput;   // <-- NEW WAY

        // LOOK AHEAD OFFSET
        Vector3 desiredOffset = new Vector3(moveInput.x, moveInput.y, 0f) * lookAheadDistance;
        lookOffset = Vector3.Lerp(lookOffset, desiredOffset, lookAheadSpeed * Time.deltaTime);

        // TARGET POSITION
        targetPos = player.position + lookOffset;
        targetPos.z = transform.position.z;

        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        newPos = ClampCameraToBounds(newPos);

        transform.position = newPos;
    }

    Vector3 ClampCameraToBounds(Vector3 camPos)
    {
        if (cameraBounds == null) return camPos;

        float camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        camPos.x = Mathf.Clamp(camPos.x,
            worldBounds.min.x + camWidth,
            worldBounds.max.x - camWidth);

        camPos.y = Mathf.Clamp(camPos.y,
            worldBounds.min.y + camHeight,
            worldBounds.max.y - camHeight);

        return camPos;
    }
}
