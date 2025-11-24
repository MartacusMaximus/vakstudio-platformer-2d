using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public InputAction move;
    public InputAction jump;

    public Rigidbody2D rb;
    public bool isGrounded;
    public Animator anim;

    void OnEnable()
    {
        move.Enable();
        jump.Enable();
    }

    void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        anim.SetBool("Walking", Mathf.Abs(moveInput) > 0.1f);

        // Flip sprite when walking
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(-moveInput), 1, 1);
        anim.SetBool("Falling", !isGrounded);


        if (jump.WasPressedThisFrame() && isGrounded)
        {
            anim.SetTrigger("Jump");
        }
    }
    public void OnJumpAnimationEvent()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
