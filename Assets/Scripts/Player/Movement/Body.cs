using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public PlayerInput _playerInput;

    public float defaultSpeed = 5f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator player_animator;
    private string currentState;
    bool isGrounded;
    bool isHaveAtHandItem=false;

    Vector3 velocity;
    Vector3 horizontalVelocity;
    
    
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
 
    // Update is called once per frame
    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Movement();
        Jump();
    }

    public void HandleHandItem()
    {
        isHaveAtHandItem= !isHaveAtHandItem;
    } 

    void Movement()
    {   
        float speed = defaultSpeed;
        if(isGrounded)
        {
            Vector2 movementInput = _playerInput.actions["Move"].ReadValue<Vector2>();
            Animations(movementInput.x,movementInput.y);
            Vector3 moveDirection = transform.TransformDirection(new Vector3(movementInput.x, 0, movementInput.y));
            horizontalVelocity = Vector3.ProjectOnPlane(moveDirection, Vector3.up);
            if( _playerInput.actions["Sprint"].IsPressed() && !isHaveAtHandItem){
                speed = (float)(defaultSpeed * 2);
            } 
            if(isHaveAtHandItem)
            {
                speed = defaultSpeed - 3f;
            }
        }
        controller.Move(horizontalVelocity * speed * Time.deltaTime);
    }

    void Jump()
    {
        //check if the player is on the ground so he can jump
        if (_playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded && !isHaveAtHandItem)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;
        player_animator.Play(newState);
        currentState = newState;
    }

    void Animations(float x,float z)
    {
        bool isMovingDiagonal = Mathf.Abs(x) > 0.1f && Mathf.Abs(z) > 0.1f;

        if (!isMovingDiagonal)
        {
            if (x < -0.1f) {
                if(_playerInput.actions["Sprint"].IsPressed() && !isHaveAtHandItem)
                {
                    ChangeAnimationState("Run Left");
                }
                else
                {
                    ChangeAnimationState("Walk Left");
                }        
            } else if (x > 0.1f) {
                if(_playerInput.actions["Sprint"].IsPressed() && !isHaveAtHandItem)
                {
                    ChangeAnimationState("Run Right");
                }
                else
                {
                    ChangeAnimationState("Walk Right");
                }  
            }
        }

        if (z < -0.1f) {
            ChangeAnimationState("Walk backward");
        } else if (z > 0.1f) {
            if(_playerInput.actions["Sprint"].IsPressed() && !isHaveAtHandItem)
            {
                ChangeAnimationState("Run Forward");
            }
            else
            {
                ChangeAnimationState("Walk Forward");
            }  
        }

        if(x == 0 && z == 0){
            ChangeAnimationState("Idle");
        }
    }

}
