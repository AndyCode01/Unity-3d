using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput _playerInput;
    public HandItem handItem;

    public float defaultSpeed = 5f;
    public float jumpHeight = 3f;


    float gravity = -9.81f * 2;
    float groundDistance = 0.4f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public Animator player_animator;
    private string currentState;
    bool isGrounded;


    Vector3 velocity;
    Vector3 horizontalVelocity;
    
    
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }
 
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Movement();
        Jump();
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
            if( _playerInput.actions["Sprint"].IsPressed() && handItem.HandleHandItem(defaultSpeed)==0) speed = (float)(defaultSpeed * 2);
            if(handItem.HandleHandItem(defaultSpeed)!=0) speed = defaultSpeed - handItem.HandleHandItem(defaultSpeed);
        }
        controller.Move(horizontalVelocity * speed * Time.deltaTime);
    }



    void Jump()
    {
        //check if the player is on the ground so he can jump
        if (_playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded && !handItem.FlagHaveItem())
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
                if(_playerInput.actions["Sprint"].IsPressed() && !handItem.FlagHaveItem())
                {
                    ChangeAnimationState("Run Left");
                }
                else
                {
                    ChangeAnimationState("Walk Left");
                }        
            } else if (x > 0.1f) {
                if(_playerInput.actions["Sprint"].IsPressed() && !handItem.FlagHaveItem())
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
            if(_playerInput.actions["Sprint"].IsPressed() && !handItem.FlagHaveItem())
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
