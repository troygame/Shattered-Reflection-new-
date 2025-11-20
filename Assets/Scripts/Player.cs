// Player.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    [SerializeField] private Transform groundCheck;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float fallGravityMultiplier = 1.5f;

    [Header("Ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.15f;

    // Exposed to states:
    public float MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool IsGrounded { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    // Concrete states:
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }

    private float _baseGravityScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StateMachine = new PlayerStateMachine();

        // Create all state instances (each gets Player + StateMachine)
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        FallState = new PlayerFallState(this, StateMachine);
    }

    private void Start()
    {
        _baseGravityScale = rb.gravityScale;

        // Start in Idle
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
    
        // --- INPUT (old input system for simplicity) ---
        MoveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            JumpPressed = true;
        }

        // --- GROUND CHECK ---
        if (groundCheck != null)
        {
            IsGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }
        // --- STATE UPDATES ---
        StateMachine.CurrentState.HandleInput();
        StateMachine.CurrentState.LogicUpdate();

        // Consume jump input after state logic
        JumpPressed = false;
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    // ----------- Helper methods that states can call -----------

    public void ApplyHorizontalMovement()
    {
        rb.linearVelocity = new Vector2(MoveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void DoJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void ApplyGravityMultiplier(bool useFallMultiplier)
    {
        rb.gravityScale = useFallMultiplier
            ? _baseGravityScale * fallGravityMultiplier
            : _baseGravityScale;
    }

    public void FaceMovementDirection()
    {
        if (Mathf.Abs(MoveInput) > 0.01f)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Sign(MoveInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
