using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // This is the namespace for the new input system

public class PlayerMove : MonoBehaviour
{
    // create an input to edit the run speed in the inspector
    [SerializeField] float runSpeed = 10f;

    [SerializeField] float jumpSpeed = 10f;

    [SerializeField] float climbSpeed = 10f;

    float gravityScaleAtStart;

    // take user input from the new input system
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    // Start is called before the first frame update
                                                                                
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;
    
    // catch the reference of the animator
    // This only define variable with type Animator 
    public Animator myAnimator;

    void Start()
    {
        // We use Matf.Epsilon instead of 0 because of floating point precision issues. and 0 not equal to 0.0001f
        Debug.Log(Mathf.Sign(-5f));
        myRigidbody = GetComponent<Rigidbody2D>();

        // This will take the component in the GameObject so we can modify the animator for player
        // Question: how did he take Ginger without telling the name of the gameobject?
        // Answer: because this script is attached to the Ginger GameObject
        myAnimator = GetComponent<Animator>();
        Debug.Log("myAnimator name is: " + myAnimator.name);

        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        Debug.Log("myCapsuleCollider name is: " + myCapsuleCollider.name);

        myBoxCollider = GetComponent<BoxCollider2D>();
        Debug.Log("myBoxCollider name is: " +myBoxCollider.name);

        gravityScaleAtStart = myRigidbody.gravityScale;

        // show the current folder path of the script
        Debug.Log("The current path of the script is: " + Application.dataPath);
         
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();

    }

    void OnMove(InputValue value) 
    {

        Debug.Log("onMove method is called!");
        moveInput = value.Get<Vector2>();

        Debug.Log(moveInput);
    }

    void Run() 
    {
        
        // Movement of the player 
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity ;

        // flip the Ginger sprite based on the direction
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; 
        if(playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("isRunning", true);
        }

        if(!playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("isRunning", false);
        }
        
    }

    void FlipSprite() 
    {
        // make boolean variable to make ginger stay on the direction 
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; 

        if(playerHasHorizontalSpeed)
        {
            
            // this will flip the sprite based on the direction user input either left or right 
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

    }

    void OnJump(InputValue value)
    {
        // get out of this method if the player is not touching the ground = to disable infinite jump/flying
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return;
        }

        if(value.isPressed)
        {
            Debug.Log("Jump is pressed!");
            myRigidbody.velocity = new Vector2(0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
       
        // If Ginger not touching the ladder, then exit the method
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
 

        Debug.Log("Climbing the ladder!");

        Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = playerVelocity;
        myRigidbody.gravityScale = 0f;

        

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        // if(playerHasVerticalSpeed)
        // {
        //     myAnimator.SetBool("isClimbing", true);
        // }

    }
}
