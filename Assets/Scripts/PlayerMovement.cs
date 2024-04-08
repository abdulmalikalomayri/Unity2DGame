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

    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityScaleAtStart;

    bool isAlive = true;

    // take user input from the new input system
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    // Start is called before the first frame update

    SpriteRenderer mySpriteRenderer;
                                                                                
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

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // show the current folder path of the script
        Debug.Log("The current path of the script is: " + Application.dataPath);
         
    }

    // Update is called once per frame
    void Update()
    {
        // if you're not alive, then don't do anything
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
         
    }

    void OnFire(InputValue value) 
    {
        if(!isAlive) { return; }
        Instantiate(bullet, gun.position, gun.rotation);
    }

    void OnMove(InputValue value) 
    {
        if(!isAlive) { return; }
        Debug.Log("onMove method is called!");
        moveInput = value.Get<Vector2>();

        Debug.Log(moveInput);
    }

    void Run() 
    {
        if(!isAlive) { return; }

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

    void Die()
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            
            



            myAnimator.SetTrigger("Die");
            Debug.Log("Ginger is dead!");
            // GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
            myRigidbody.velocity = deathKick;
            mySpriteRenderer.color = Color.red;
            //
            transform.Rotate(0, 0, 90);
            myAnimator.SetTrigger("Dying");
            
             

            // FindObjectOfType<GameSession>().ProcessPlayerDeath();
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
