using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        anim.SetBool("Walking", Mathf.Abs(move) > 0.1f);

        // Flip sprite when walking
        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(-move), 1, 1);

        anim.SetBool("Falling", !isGrounded);


        if (Input.GetButtonDown("Jump") && isGrounded)
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
