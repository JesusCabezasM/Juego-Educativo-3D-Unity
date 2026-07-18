using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

    [Header("Movement Settings")]
    public float Velocity = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    
    [Header("References")]
    public Animator anim;
    public Camera cam;
    public CharacterController controller;

    [Header("State")]
    public bool isGrounded;
    private Vector3 playerVelocity;

    public float InputX, InputZ, Speed;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public float allowPlayerRotation = 0.1f;

    [Header("Animation Smoothing")]
    [Range(0, 1f)] public float StartAnimTime = 0.3f;
    [Range(0, 1f)] public float StopAnimTime = 0.15f;

    void Start () {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
    }
    
    void Update () {
        // Solo intentamos mover si el controlador está activo
        if (!controller.enabled) return;

        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;

        MovePlayer();
    }

    void MovePlayer() {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        var forward = cam.transform.forward;
        var right = cam.transform.right;
        forward.y = 0f; right.y = 0f;
        forward.Normalize(); right.Normalize();
        
        Vector3 moveDir = (forward * InputZ + right * InputX);

        if (Speed > allowPlayerRotation) {
            anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
            
            if (!blockRotationPlayer) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), desiredRotationSpeed);
            }
            
            Vector3 finalMove = (moveDir * Velocity) + new Vector3(0, playerVelocity.y, 0);
            controller.Move(finalMove * Time.deltaTime);
        } 
        else {
            anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}