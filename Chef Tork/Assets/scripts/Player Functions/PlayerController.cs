using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float jumpStrength = 5f;
    public bool canControl = true;

    TouchingDirections touchingDirections;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        walkSpeed = data.walkSpeed;
        runSpeed = data.runSpeed;
        jumpStrength = data.jumpStrength;
        canControl = data.canControl;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }

    public float currentMoveSpeed
    {
        get
        {
            if (IsMoving)
            {
                if (IsRunning)
                { 
                    return runSpeed; 
                }
                else 
                { 
                    return walkSpeed; 
                }

            }
            else
            {
                return 0; //idle speed is 0
            }
        }
    }

    [SerializeField] // makes variable visible in unity
    private bool _isMoving = false;
    [SerializeField]
    private bool _isRunning = false;
    public bool IsMoving { 
        get 
        {
            return _isMoving;
        }
        private set
        {
            if (GameIsPaused == false)
            {
                _isMoving = value;
                animator.SetBool(AnimationStrings.isMoving, value);
            }
            else { return; } 
        }
    }

    private bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool CanControl
    {
        get
        {
            return canControl;
        }
        set
        {
            canControl = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return animator.GetBool(AnimationStrings.death);
        }
        set
        {
            animator.SetBool(AnimationStrings.death, value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight { get { return _isFacingRight; }
        private set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1); //flip the image of the player
            }
            _isFacingRight = value;
        }
    }
    Rigidbody2D rb;
    Animator animator;
  

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>(); 
       animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();

    }

    private Rigidbody2D rb2d;
    private Animator[] animators;


    // Start is called before the first frame update
    void Start()
    {

    }

    bool GameIsPaused = false; 

    public GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }
    void Resume()
    {
        GameIsPaused = false;

    }

    void Pause()
    {
        GameIsPaused = true;
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
    }

   public void OnMove(InputAction.CallbackContext context)
    {
        if (!CanControl)
            return;

        moveInput = context.ReadValue<Vector2>(); //this makes x and y movements

       // IsMoving = true; //make moving status always true upon first button press

        IsMoving = moveInput != Vector2.zero; //Is moving is true as long as its not equal to zero

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (GameIsPaused == false)
        {
            if (moveInput.x > 0 && !IsFacingRight)
            {
                //face right
                IsFacingRight = true;
            }
            else if (moveInput.x < 0 && IsFacingRight)
            {
                //face left
                IsFacingRight = false;
            }
        }
        else { return; }
    }

    public void onRun(InputAction.CallbackContext context) //'context' regard the pressing of a keyboard button here
    {
         if (!CanControl)
            return;
      
            if (context.started) //button is pressed down
            {
                IsRunning = true;
            }
            else if (context.canceled) //button is released
            {
                IsRunning = false;
            }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!CanControl)
            return;

        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
    }

    public void Die()
    {
        IsDead = true;
        CanControl = false;
    }
}

