using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float moveSpeed = 4;
    public float jumpForce;

    private float inputX = 0;
    private float facing = 1;
    private bool isGrounded;

    public Transform groundCheckPos;
    float groundCheckLength = 0.1f;
    public LayerMask groundCheckLayerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        //Facing direction
        if (inputX > 0)
            facing = 1;
        else if (inputX < 0)
            facing = -1;

        if (facing == 1)
            spriteRenderer.flipX = false;
        else if (facing < -0.01f)
            spriteRenderer.flipX = true;

        RaycastHit2D hit = Physics2D.Raycast(groundCheckPos.position, Vector2.down, groundCheckLength, groundCheckLayerMask);
        if(hit.collider != null)
            isGrounded = true;
        else
        isGrounded = false;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //}

        if (animator)
        {
            animator.SetFloat("moveX", Mathf.Abs(rigidBody.linearVelocityX));
            animator.SetBool("isJumping", !isGrounded);
            animator.SetBool("isFalling", rigidBody.linearVelocityY < 0);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = inputX * moveSpeed;
    }
}
