using System;
using UnityEngine;
using UnityEngine.UI;

public class FroggyMovement : MonoBehaviour
{
    [SerializeField] private MovementButton _leftMovementButton;
    [SerializeField] private MovementButton _rightMovementButton;
    [SerializeField] private Button _jumpButton;

    private Rigidbody2D rigb; //make variable to avoid calling GetComponent again and again
    private BoxCollider2D boxcoll;
    private SpriteRenderer spriter;
    private Animator anim;

    [SerializeField] private LayerMask jumpingGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5.5f;
    [SerializeField] private float jumpForce = 11f;

    private enum MovementState { idle, running, jumping, falling } //finite set of animation states, convenient since animations are mutually exclusive

    [SerializeField] private AudioSource jumpSound;

    private void OnEnable()
    {
        _leftMovementButton.OnPressHandling += OnLeftMovementButtonPressing;
        _rightMovementButton.OnPressHandling += OnRightMovementButtonPressing;
        _jumpButton.onClick.AddListener(Jump);
    }

    private void OnDisable()
    {
        _leftMovementButton.OnPressHandling -= OnLeftMovementButtonPressing;
        _rightMovementButton.OnPressHandling -= OnRightMovementButtonPressing;
        _jumpButton.onClick.RemoveListener(Jump);
    }

    private void Start()
    {
        rigb = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //from Input Manager of Unity; float for adaptability for joysticks
        Move(); /*instead of 0, keep position of y the same
                                                                 '* dirX' saves time since dirX gives pos/neg value, efficient*/

        if (Input.GetButtonDown("Jump") && IsGrounded()) //from Input Manager of Unity
        {
            Jump();
        }

        //UpdateAnimationState();
    }

    private void OnLeftMovementButtonPressing()
    {
        dirX = -1;
        Move();
    }

    private void OnRightMovementButtonPressing()
    {
        dirX = 1;
        Move();
    }

    private void Move()
    {
        rigb.velocity = new Vector2(dirX * moveSpeed, rigb.velocity.y);
        UpdateAnimationState();
    }

    private void UpdateAnimationState() //method to change inbetween animations, depends on: 'state' value
    {
        MovementState state;

        if (dirX > 0f) //check for disposition on X-axis, that is run right/run left, else idle
        {
            state = MovementState.running;
            spriter.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            spriter.flipX = true; //change direction of animation when running to left
        }
        else
        {
            state = MovementState.idle;
        }

        if (rigb.velocity.y > .1f) //check for disposition on Y-axis, that is jump/fall
        {
            state = MovementState.jumping;
        }
        else if (rigb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded() //method to avoid infinite jumps into air
    {
        return Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0f, Vector2.down, .1f, jumpingGround); /*bounds.center & bounds.size - copy objects box, 0f - no rotation,
                                                                                                            Vector2.down, .1f - move the new box down by .1f*/
    }

    public void Jump()
    {
        rigb.velocity = new Vector2(rigb.velocity.x, jumpForce); //Vector2 since 2D game
        jumpSound.Play();
    }
}
